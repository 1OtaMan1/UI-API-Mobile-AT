using System;
using System.Collections.Generic;
using System.Linq;
using API.Helpers;
using Core.Helpers;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Core.Constants.TestCategories;

namespace Tests.API.News;

[TestClass]
public class NewsTests : BaseApiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void UserShouldGetCompaniesWithNewsListTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().SetConfirmationStatus(true)
            .SetVerificationStatus(true).Build();

        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);
        Admin.AdminNews.ChangeConfirmationStatus(newsModel.Id);
        Admin.AdminNews.ChangeVerificationStatus(newsModel.Id);

        #endregion

        var news = ContributorMailtrap.News.GetCompaniesWithNewsList();
        Assert.IsTrue(news.Any(company => company.News.Any(_ => _.Title.Equals(newsModel.Title))), "User should get Companies with News list");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void UserShouldGetCompanyNewsListTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().SetConfirmationStatus(true)
            .SetVerificationStatus(true).Build();

        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);
        Admin.AdminNews.ChangeConfirmationStatus(newsModel.Id);
        Admin.AdminNews.ChangeVerificationStatus(newsModel.Id);

        #endregion

        var companyNews = ContributorMailtrap.News.GetCompanyNews(companyModel.Id);
        Assert.IsTrue(companyNews.News.Any(_ => _.Title.Equals(newsModel.Title)), "User should get Company News list");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void UserShouldConfirmNewsTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        ContributorMailtrap.News.ConfirmNews(new[] { newsModel.Id });

        var actualNews = Admin.AdminNews.Get(newsModel.Id);
        Assert.IsTrue(actualNews.IsConfirmed, "User should confirm News");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void UserShouldGetCompanyShortNewsListTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().SetConfirmationStatus(true)
            .SetVerificationStatus(true).Build();

        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);
        Admin.AdminNews.ChangeConfirmationStatus(newsModel.Id);
        Admin.AdminNews.ChangeVerificationStatus(newsModel.Id);

        #endregion

        var news = ContributorMailtrap.News.GetCompanyNewsShortList(companyModel.Id);
        Assert.IsTrue(news.Any(_ => _.Title.Equals(newsModel.Title)), "User should get Company short News list");
    }
}