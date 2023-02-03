using System;
using System.Collections.Generic;
using API.Exceptions;
using API.Models.Auth;
using Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Core.Constants.TestCategories;

namespace Tests.API.Account;

[TestClass]
public class AccountTests : BaseApiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [TestCategory(SmokeApi)]
    public void AdminShouldGetTokenTest()
    {
        var token = Admin.Account.GetToken(Admin.Credentials);
        Assert.IsTrue(token.Token != null, "Admin should get token");
    }

    [TestMethod]
    [TestCategory(SmokeApi)]
    public void AdminShouldCreateNewOriginalAccountTest()
    {
        var loginModel = new LoginApiModel { Username = Generator.Email(), Password = Generator.Email() };
        Admin.Account.Create(loginModel);

        Assert.ThrowsException<UnsuccessfulResponseException>(() => Admin.Account.Create(loginModel));
    }
}