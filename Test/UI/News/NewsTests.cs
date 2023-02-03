using System;
using System.Collections.Generic;
using System.Linq;
using API.ApiUsers;
using API.Helpers;
using API.Mailtrap;
using Atata;
using Core.Helpers;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Atata.Extensions;
using UI.Business.BaseApp.Home;
using UI.Business.BaseApp.UserContribution;
using UI.Models;
using static Core.Constants.TestCategories;

namespace Tests.UI.News;

[TestClass]
public class NewsTests : BaseUiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46708), TestCategory(CaptchaEnabled)]
    public void UnableNewsAddByBotTest()
    {
        #region Setup

        var userModel = new UserUiModel();
        var company = Admin.AdminCompany.GetUserCompanies(Admin.Id).First();
        var newsModel = new NewsUiModel { CompanyName = company.CompanyName, CompanyUrl = company.WebSiteUrl };

        #endregion

        //  Check add news page open
        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        var newsPage = brandsPage.SwitchToNewsTab();
        var addNewsPage = newsPage.StartCompanyNewsAdd();

        addNewsPage.FirstNewsUrlInput.Wait(Until.Visible);

        //  Check bot unable to add news
        addNewsPage.FillNewsData(newsModel);
        addNewsPage.FillUserData(userModel);
        addNewsPage.SubmitButton.ClickAndGo();

        TestActions.Add(() => Admin.AdminUser.Delete(Admin.AdminUser.GetList(userModel.Email).First().Id));

        Assert.ThrowsException<Exception>(() => Admin.AdminUser.GetList(userModel.Email).First().Id, "System should not pass Bots");
        Assert.ThrowsException<Exception>(() => Admin.AdminNews.GetList(newsModel.NewsUrl[0]).First().Id, "System should not let Bot add Company");
    }

    [TestMethod]
    [StoryId(46708), TestCategory(EmailNotification)]
    public void NewsAddConfirmVerifyDeleteEmailTest()
    {
        #region Setup

        const string addedCompanyNewsPageTitle = "CONFIRM NEWS ADDING";
        const string addedCompanyNewsPageDescription = "To complete the process, please confirm your email address.";
        const string confirmedCompanyNewsPageTitle = "THANK YOU FOR SUBMITTING A NEWS ITEM";
        const string confirmedCompanyNewsPageDescription = "Our moderators will review your submission asap";

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        Admin.AdminCompany.ChangeConfirmationStatus(companyModel.Id);
        Admin.AdminCompany.ChangeVerificationStatus(companyModel.Id);

        var newsModel = new NewsUiModel { CompanyName = companyModel.CompanyName, CompanyUrl = companyModel.WebSiteUrl };

        var user = new TenantApiUser(MailtrapUtil.CreateMailboxCredentialsStorage());
        TestActions.Add(() => MailtrapUtil.DeleteInbox(user.Credentials.InboxId));

        #endregion

        //  Check add news page open
        var addNewsPage = GoWithRetry.To<AddNewsPage>(url: Url.ToAddNews);

        addNewsPage.FirstNewsUrlInput.Wait(Until.Visible);

        //  Check add news
        addNewsPage.FillNewsData(newsModel);
        addNewsPage.FillUserData(user.Credentials);
        var successInfoPage = addNewsPage.SubmitButton.ClickAndGo();
        successInfoPage.Description.Wait(Until.Visible);

        user.Credentials.Id = Admin.AdminUser.GetList(user.Credentials.Email).First().Id;
        TestActions.Add(() => Admin.AdminUser.Delete(user.Id));

        newsModel.Id = Admin.AdminNews.GetUserNews(user.Id).First().Id;
        TestActions.Add(() => Admin.AdminNews.Delete(newsModel.Id));

        successInfoPage.Title.Should.WithRetry.ContainIgnoringCase(addedCompanyNewsPageTitle);
        successInfoPage.Description.Should.ContainIgnoringCase(addedCompanyNewsPageDescription);

        //  Check news created email sent
        var subject = EmailSubject.ConfirmAddedNews;
        var body = EmailBody.ConfirmAddedNews;

        var message = user.Mailbox.GetHtmlMessage(subject, body);

        //  Check news activation
        successInfoPage = GoWithRetry.To(successInfoPage, message.ConfirmationLink);
        successInfoPage.Description.Wait(Until.Visible);

        successInfoPage.Title.Should.WithRetry.ContainIgnoringCase(confirmedCompanyNewsPageTitle);
        successInfoPage.Description.Should.ContainIgnoringCase(confirmedCompanyNewsPageDescription);

        //  Check news become verified
        var actualNews = Admin.AdminNews.GetUserNews(user.Id).First();

        Assert.IsTrue(actualNews.IsConfirmed, "Created news should be verified after open confirmation link");

        //  Check verified news displayed
        actualNews.Title = Generator.Text();
        Admin.AdminNews.Update(actualNews);
        Admin.AdminNews.ChangeVerificationStatus(newsModel.Id);

        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        var newsPage = brandsPage.SwitchToNewsTab();
        newsPage.SearchInput.Wait(Until.Visible);
        newsPage.SearchInput.Set(newsModel.CompanyName);

        newsPage.GetBrandNews(newsModel.CompanyName).Wait(Until.Visible);

        //  Check deleted news disappear
        Admin.AdminNews.Delete(newsModel.Id);

        newsPage.RefreshPage();
        newsPage.SearchInput.Wait(Until.Visible);
        newsPage.SearchInput.Set(newsModel.CompanyName);

        newsPage.GetBrandNews(newsModel.CompanyName).Should.WithRetry.Not.Exist();
    }
}