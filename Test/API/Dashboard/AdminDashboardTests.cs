using System;
using System.Collections.Generic;
using API.Helpers;
using Core.Helpers;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Core.Constants.TestCategories;

namespace Tests.API.Dashboard;

[TestClass]
public class AdminDashboardTests : BaseApiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46842), TestCategory(SmokeApi)]
    public void AdminShouldGetDashboardTest()
    {
        #region Setup

        var companyModel = new AdminCompanyApiModelBuilder().Build();
        companyModel.Id = Admin.AdminCompany.Create(companyModel);
        TestActions.Add(() => Admin.AdminCompany.Delete(companyModel.Id));

        #endregion

        var dashboard = Admin.AdminDashboard.Get();
        Assert.IsTrue(dashboard.TotalCompanies > (int)default, "Admin should get Dashboard");
    }
}