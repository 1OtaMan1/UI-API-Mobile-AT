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
public class UserTests : BaseApiTest
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
    public void ShouldDeleteUserTest()
    {
        #region Setup

        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        TestActions.Add(() => Admin.AdminUser.Delete(userModel.Id));

        #endregion

        Admin.User.DeleteUser(userModel.Id);

        var actualUsers = Admin.AdminUser.GetList(userModel.Email);
        Assert.IsFalse(actualUsers.Any(), "User should be deleted");
    }
}