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
public class AdminNewsTests : BaseApiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46847), TestCategory(SmokeApi)]
    public void AdminShouldAddNewsTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();

        #endregion

        newsModel.Id = Admin.AdminNews.Create(newsModel);

        var actualNews = Admin.AdminNews.GetCompanyNews(companyModel.Id).First();
        Assert.AreEqual(newsModel.Title, actualNews.Title, "Admin should add News");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldUpdateNewsTest()
    {
        #region Setup

        var updatedTitle = Generator.News();

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        newsModel.Title = updatedTitle;

        #endregion

        Admin.AdminNews.Update(newsModel);

        var actualNews = Admin.AdminNews.GetCompanyNews(companyModel.Id).First();
        Assert.AreEqual(updatedTitle, actualNews.Title, "Admin should add News");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldDeleteNewsTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        Admin.AdminNews.Delete(newsModel.Id);

        var news = Admin.AdminNews.GetCompanyNews(companyModel.Id);
        Assert.IsFalse(news.Any(), "Admin should delete News");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldGetNewsListTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        var news = Admin.AdminNews.GetList();
        Assert.IsTrue(news.Any(_ => _.Id.Equals(newsModel.Id)), "Admin should get News list");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldGetCompanyNewsListTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        var news = Admin.AdminNews.GetCompanyNews(companyModel.Id);
        Assert.IsTrue(news.Any(_ => _.Id.Equals(newsModel.Id)), "Admin should get Company News list");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldGetUserNewsListTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        var news = Admin.AdminNews.GetUserNews(Admin.Id);
        Assert.IsTrue(news.Any(_ => _.Id.Equals(newsModel.Id)), "Admin should get User News list");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldGetNewsCountTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        var newsCount = Admin.AdminNews.GetCount();
        Assert.IsTrue(newsCount.AllNews > (int)default, "Admin should get News count");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldGetCompanyNewsCountTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        var newsCount = Admin.AdminNews.GetCompanyNewsCount(companyModel.Id);
        Assert.IsTrue(newsCount > (int)default, "Admin should get Company News count");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldGetUserNewsCountTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        var newsCount = Admin.AdminNews.GetUserNewsCount(Admin.Id);
        Assert.IsTrue(newsCount > (int)default, "Admin should get User News count");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldGetNewsDetailsTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        var actualNews = Admin.AdminNews.Get(newsModel.Id);
        Assert.AreEqual(newsModel.IsConfirmed, actualNews.IsConfirmed, "Admin should get News details");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldConfirmNewsTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        Admin.AdminNews.ChangeConfirmationStatus(newsModel.Id);

        var actualNews = Admin.AdminNews.Get(newsModel.Id);
        Assert.IsTrue(actualNews.IsConfirmed, "Admin should confirm News");
    }

    [TestMethod]
    [StoryId(46846), TestCategory(SmokeApi)]
    public void AdminShouldVerifyNewsTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var newsModel = new AdminNewsApiModelBuilder(companyModel.Id).Build();
        newsModel.Id = Admin.AdminNews.Create(newsModel);

        #endregion

        Admin.AdminNews.ChangeVerificationStatus(newsModel.Id);

        var actualNews = Admin.AdminNews.Get(newsModel.Id);
        Assert.IsTrue(actualNews.IsVerified, "Admin should verify News");
    }
}