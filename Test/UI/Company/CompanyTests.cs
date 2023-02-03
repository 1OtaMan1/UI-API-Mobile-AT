using System;
using System.Collections.Generic;
using System.Linq;
using API.ApiUsers;
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

namespace Tests.UI.Company;

[TestClass]
public class CompanyTests : BaseUiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46577), TestCategory(CaptchaEnabled)]
    public void UnableCompanyAddByBotTest()
    {
        var userModel = new UserUiModel();
        var companyModel = new CompanyUiModel();

        //  Check add company page open
        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        var addCompanyPage = brandsPage.StartCompanyAdd();

        addCompanyPage.CompanyNameInput.Wait(Until.Visible);

        //  Check bot unable to add company
        addCompanyPage.FillCompanyData(companyModel);
        addCompanyPage.FillUserData(userModel);
        addCompanyPage.SubmitButton.ClickAndGo();

        TestActions.Add(() => Admin.AdminUser.Delete(Admin.AdminUser.GetList(userModel.Email).First().Id));

        Assert.ThrowsException<Exception>(() => Admin.AdminUser.GetList(userModel.Email).First().Id, "System should not let Bot add User");
        Assert.ThrowsException<Exception>(() => Admin.AdminCompany.GetList(companyModel.CompanyName).First().Id, "System should not let Bot add Company");
    }

    [TestMethod]
    [StoryId(46652), TestCategory(EmailNotification)]
    public void CompanyAddConfirmVerifyHideEmailTest()
    {
        #region Setup

        const string addedCompanyPageTitle = "CONFIRM COMPANY SUGGESTION";
        const string addedCompanyPageDescription = "To complete the process, please confirm your email address.";
        const string confirmedCompanyPageTitle = "COMPANY SUCCESSFULLY SUBMITTED";
        const string confirmedCompanyPageDescription = "Our moderators will review your submission asap";

        var companyModel = new CompanyUiModel();

        var user = new TenantApiUser(MailtrapUtil.CreateMailboxCredentialsStorage());
        TestActions.Add(() => MailtrapUtil.DeleteInbox(user.Credentials.InboxId));

        #endregion

        //  Check add company page open
        var addCompanyPage = GoWithRetry.To<AddCompanyPage>(url: Url.ToAddCompany);

        addCompanyPage.CompanyNameInput.Wait(Until.Visible);

        //  Check add company
        addCompanyPage.FillCompanyData(companyModel);
        addCompanyPage.FillUserData(user.Credentials);
        var successInfoPage = addCompanyPage.SubmitButton.ClickAndGo();
        successInfoPage.Description.Wait(Until.Visible);

        user.Credentials.Id = Admin.AdminUser.GetList(user.Credentials.Email).First().Id;
        TestActions.Add(() => Admin.AdminUser.Delete(user.Id));

        companyModel.Id = Admin.AdminCompany.GetUserCompanies(user.Id).First().Id;
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        successInfoPage.Title.Should.WithRetry.ContainIgnoringCase(addedCompanyPageTitle);
        successInfoPage.Description.Should.ContainIgnoringCase(addedCompanyPageDescription);

        //  Check company created email sent
        var subject = EmailSubject.ConfirmAddedCompany;
        var body = EmailBody.ConfirmAddedCompany;

        var message = user.Mailbox.GetHtmlMessage(subject, body);

        //  Check company activation
        successInfoPage = GoWithRetry.To(successInfoPage, message.ConfirmationLink);
        successInfoPage.Description.Wait(Until.Visible);

        successInfoPage.Title.Should.WithRetry.ContainIgnoringCase(confirmedCompanyPageTitle);
        successInfoPage.Description.Should.ContainIgnoringCase(confirmedCompanyPageDescription);

        //  Check company become verified
        var actualCompany = Admin.AdminCompany.GetUserCompanies(user.Id).First();

        Assert.IsTrue(actualCompany.IsConfirmed, "Created company should be verified after open confirmation link");

        //  Check verified company displayed
        Admin.AdminCompany.ChangeVerificationStatus(companyModel.Id);

        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        brandsPage.SearchInput.Wait(Until.Visible);
        brandsPage.SearchInput.Set(companyModel.CompanyName);

        brandsPage.GetCompany(companyModel.CompanyName).Wait(Until.Visible);

        //  Check disabled company disappear
        Admin.AdminCompany.ChangeActivationStatus(companyModel.Id);

        brandsPage.RefreshPage();
        brandsPage.SearchInput.Wait(Until.Visible);
        brandsPage.SearchInput.Set(companyModel.CompanyName);

        brandsPage.GetCompany(companyModel.CompanyName).Should.WithRetry.Not.Exist();
    }
}