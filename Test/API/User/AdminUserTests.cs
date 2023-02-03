using System;
using System.Collections.Generic;
using System.Linq;
using API.Helpers;
using Core.Helpers;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Core.Constants.TestCategories;

namespace Tests.API.User;

[TestClass]
public class AdminUserTests : BaseApiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeApi)]
    public void AdminShouldCreateUserTest()
    {
        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        TestActions.Add(() => Admin.AdminUser.Delete(userModel.Id));

        var actualUser = Admin.AdminUser.GetList(userModel.Email).First();
        Assert.AreEqual(userModel.Id, actualUser.Id, "User should be created");
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeApi)]
    public void AdminShouldEditCreatedUserTest()
    {
        #region Setup

        var updatedName = Generator.FirstName();

        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        TestActions.Add(() => Admin.AdminUser.Delete(userModel.Id));

        userModel.Name = updatedName;

        #endregion

        Admin.AdminUser.Update(userModel);

        var actualUser = Admin.AdminUser.Get(userModel.Id);
        Assert.AreEqual(updatedName, actualUser.Name, "User should be updated");
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeApi)]
    public void AdminShouldDeleteCreatedUserTest()
    {
        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        Admin.AdminUser.Delete(userModel.Id);

        var users = Admin.AdminUser.GetList(userModel.Email);
        Assert.IsFalse(users.Any(), "User should be deleted");
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeApi)]
    public void AdminShouldGetUsersListTest()
    {
        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        TestActions.Add(() => Admin.AdminUser.Delete(userModel.Id));

        var users = Admin.AdminUser.GetList(userModel.Email);
        Assert.IsTrue(users.Any(), "Admin should get Users list");
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeApi)]
    public void AdminShouldGetUsersCountTest()
    {
        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        TestActions.Add(() => Admin.AdminUser.Delete(userModel.Id));

        var usersCount = Admin.AdminUser.GetCount(userModel.Email);
        Assert.IsTrue(usersCount > (int)default, "Admin should get Users count");
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeApi)]
    public void AdminShouldGetUserDetailsTest()
    {
        #region Setup

        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        TestActions.Add(() => Admin.AdminUser.Delete(userModel.Id));

        #endregion

        var actualUser = Admin.AdminUser.Get(userModel.Id);
        Assert.AreEqual(userModel.Surname, actualUser.Surname, "Admin should get User details");
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeApi)]
    public void AdminShouldDisableUserTest()
    {
        #region Setup

        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        TestActions.Add(() => Admin.AdminUser.Delete(userModel.Id));

        #endregion

        Admin.AdminUser.ChangeActivationStatus(userModel.Id);

        var actualUser = Admin.AdminUser.Get(userModel.Id);
        Assert.IsTrue(actualUser.IsDisabled, "Admin should disable User");
    }
}