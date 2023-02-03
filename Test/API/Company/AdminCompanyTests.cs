using System;
using System.Collections.Generic;
using System.Linq;
using API.Helpers;
using Core.Helpers;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Core.Constants.TestCategories;

namespace Tests.API.Company;

[TestClass]
public class AdminCompanyTests : BaseApiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46844), TestCategory(SmokeApi)]
    public void AdminShouldCreateCompanyTest()
    {
        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var actualCompany = Admin.AdminCompany.GetList(companyModel.CompanyName).First();
        Assert.AreEqual(companyModel.Id, actualCompany.Id, "Company should be created");
    }

    [TestMethod]
    [StoryId(46845), TestCategory(SmokeApi)]
    public void AdminShouldEditCreatedCompanyTest()
    {
        #region Setup

        var updatedCompanyName = Generator.CompanyName();

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        companyModel.CompanyName = updatedCompanyName;

        #endregion

        Admin.AdminCompany.Update(companyModel);

        var actualCompany = Admin.AdminCompany.GetList(updatedCompanyName).First();
        Assert.AreEqual(companyModel.Id, actualCompany.Id, "Company should be updated");
    }

    [TestMethod]
    [StoryId(46845), TestCategory(SmokeApi)]
    public void AdminShouldDeleteCreatedCompanyTest()
    {
        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        Admin.AdminCompany.Delete(companyModel.Id);

        Assert.ThrowsException<InvalidOperationException>(
            () => Admin.AdminCompany.GetList(companyModel.CompanyName).First(), "Company should be deleted");
    }

    [TestMethod]
    [StoryId(46655), TestCategory(SmokeApi)]
    public void AdminShouldGetCompaniesListTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        #endregion

        var companies = Admin.AdminCompany.GetList().ToList();
        CollectionAssert.Contains(companies.Select(_ => _.CompanyName).ToList(), companyModel.CompanyName, "Admin should get Companies list");
    }

    [TestMethod]
    [StoryId(46655), TestCategory(SmokeApi)]
    public void AdminShouldGetUserCompaniesListTest()
    {
        var companies = Admin.AdminCompany.GetUserCompanies(SelfSignedContributorMailtrap.Id).ToList();
        Assert.IsTrue(companies.Any(), "Admin should get User's Companies list");
    }

    [TestMethod]
    [StoryId(46655), TestCategory(SmokeApi)]
    public void AdminShouldGetCompaniesNamesTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        #endregion

        var companies = Admin.AdminCompany.GetNamesWithIds();
        CollectionAssert.Contains(companies.Select(_ => _.Name).ToList(), companyModel.CompanyName, "Admin should get Companies Names list");
    }

    [TestMethod]
    [StoryId(46655), TestCategory(SmokeApi)]
    public void AdminShouldGetCompanyNameByIdTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        #endregion

        var actualCompany = Admin.AdminCompany.GetNameWithId(companyModel.Id);
        Assert.AreEqual(companyModel.CompanyName, actualCompany.Name, "Admin should get Company Name by Id");
    }

    [TestMethod]
    [StoryId(46655), TestCategory(SmokeApi)]
    public void AdminShouldGetCompaniesCountTest()
    {
        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        var companiesCount = Admin.AdminCompany.GetCount();
        Assert.IsTrue(companiesCount.AllCompanies > (int)default, "Admin should get Companies count");
    }

    [TestMethod]
    [StoryId(46655), TestCategory(SmokeApi)]
    public void AdminShouldGetUserCompaniesCountTest()
    {
        var companiesCount = Admin.AdminCompany.GetUserCompaniesCount(SelfSignedContributorMailtrap.Id);
        Assert.IsTrue(companiesCount > (int)default, "Admin should get User's Companies count");
    }

    [TestMethod]
    [StoryId(46845), TestCategory(SmokeApi)]
    public void AdminShouldGetCompanyDetailsTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        #endregion

        var actualCompany = Admin.AdminCompany.Get(companyModel.Id);
        Assert.AreEqual(companyModel.CompanyName, actualCompany.CompanyName, "Admin should get Company details");
        Assert.IsFalse(actualCompany.IsConfirmed, "Admin should get Company details");
        Assert.IsFalse(actualCompany.IsVerified, "Admin should get Company details");
    }

    [TestMethod]
    [StoryId(46845), TestCategory(SmokeApi)]
    public void AdminShouldVerifyCompanyTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        #endregion

        Admin.AdminCompany.ChangeVerificationStatus(companyModel.Id);

        var actualCompany = Admin.AdminCompany.Get(companyModel.Id);
        Assert.IsTrue(actualCompany.IsVerified, "Admin should verify Company");
    }

    [TestMethod]
    [StoryId(46845), TestCategory(SmokeApi)]
    public void AdminShouldConfirmCompanyTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        #endregion

        Admin.AdminCompany.ChangeConfirmationStatus(companyModel.Id);

        var actualCompany = Admin.AdminCompany.Get(companyModel.Id);
        Assert.IsTrue(actualCompany.IsConfirmed, "Admin should confirm Company");
    }

    [TestMethod]
    [StoryId(46845), TestCategory(SmokeApi)]
    public void AdminShouldActivateCompanyTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        #endregion

        Admin.AdminCompany.ChangeActivationStatus(companyModel.Id);

        var actualCompany = Admin.AdminCompany.Get(companyModel.Id);
        Assert.IsTrue(actualCompany.IsWorking, "Admin should activate Company");
    }
}