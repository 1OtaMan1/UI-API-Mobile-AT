using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using API.Helpers;
using Atata;
using Core.Extensions;
using Core.Helpers;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Atata.Extensions;
using UI.Business.AdminApp.User;
using UI.Models;
using UI.Models.Enums;
using static Core.Constants.Sizes;
using static Core.Constants.TestCategories;

namespace Tests.UI.User;

[TestClass]
public class UserTests : BaseUiTest
{
    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeUi)]
    public void UserDetailsControlsDisplayTest()
    {
        const string expectedUserCreatedDate = "07.04.2022";
        const string expectedUserCreatedDateOtherVersion = "07/04/2022";

        //  Check open user details page
        var usersListPage = LoginAndGo.To<UsersListPage>(Url.ToUsersList, Admin);
        usersListPage.Users[0].Wait(Until.Visible);

        var userDetailsPage = usersListPage.GetUser(SelfSignedContributorMailtrap.Credentials.Email).Email.ClickAndGo();
        userDetailsPage.GoToUsersListButton.Wait(Until.Visible);

        //  Check user detail container
        userDetailsPage.UserDetails.Email.Should.ContainIgnoringCase(SelfSignedContributorMailtrap.Credentials.Email);
        userDetailsPage.UserDetails.FirstName.Should.ContainIgnoringCase(SelfSignedContributorMailtrap.Credentials.FirstName);
        userDetailsPage.UserDetails.LastName.Should.ContainIgnoringCase(SelfSignedContributorMailtrap.Credentials.LastName);
        userDetailsPage.UserDetails.Status.Should.ContainIgnoringCase(StatusDetails.Enabled.Description());
        var actualUserCreatedDate = userDetailsPage.UserDetails.CreatedDate.Value;
        Assert.IsTrue(actualUserCreatedDate.Contains(expectedUserCreatedDate)
                      || actualUserCreatedDate.Contains(expectedUserCreatedDateOtherVersion), "There should be User creation date displayed");

        //  Check action buttons displayed
        userDetailsPage.ActionButtons.EditUserButton.Wait(Until.Visible);
        userDetailsPage.ActionButtons.BlockUserButton.Wait(Until.Visible);
        userDetailsPage.ActionButtons.AssignAllToAdminButton.Wait(Until.Visible);
        userDetailsPage.ActionButtons.DeleteNewsButton.Wait(Until.Visible);
        userDetailsPage.ActionButtons.DeleteCompaniesButton.Wait(Until.Visible);
        userDetailsPage.ActionButtons.DeleteUserButton.Wait(Until.Visible);

        //  Check news and companies list displayed
        userDetailsPage.Companies[0].Wait(Until.Visible);
        userDetailsPage.News[0].Wait(Until.Visible);
    }

    [DataRow(true)]
    [DataRow(false)]
    [TestMethod]
    [StoryId(46848), TestCategory(SmokeUi)]
    public void AddUserTest(bool isDisabled)
    {
        #region Setup

        var expectedUserModel = new UserUiModel
        {
            Email = Generator.Email(),
            FirstName = Generator.FirstName(),
            LastName = Generator.LastName(),
            IsDisabled = isDisabled
        };

        var expectedDate = DateTime.Today.Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
        var expectedDateOtherVersion = DateTime.Today.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        #endregion

        //  Check add user button displayed
        var usersListPage = LoginAndGo.To<UsersListPage>(Url.ToUsersList, Admin);
        usersListPage.AddUserButton.Wait(Until.Visible);

        //  Check add user page open
        var addUserPage = usersListPage.AddUserButton.ClickAndGo();
        addUserPage.EmailInput.Wait(Until.Visible);

        //  Check set user data
        addUserPage.EmailInput.Set(expectedUserModel.Email);
        addUserPage.FirstNameInput.Set(expectedUserModel.FirstName);
        addUserPage.LastNameInput.Set(expectedUserModel.LastName);
        if (isDisabled)
        {
            addUserPage.IsDisabled.Check();
        }

        var actualEmail = addUserPage.EmailInput.Attributes.Value.Value;
        var actualFirstName = addUserPage.FirstNameInput.Attributes.Value.Value;
        var actualLastName = addUserPage.LastNameInput.Attributes.Value.Value;

        Assert.AreEqual(expectedUserModel.Email, actualEmail, $"Email field should contain: {expectedUserModel.Email}");
        Assert.AreEqual(expectedUserModel.FirstName, actualFirstName, $"Name field should contain: {expectedUserModel.FirstName}");
        Assert.AreEqual(expectedUserModel.LastName, actualLastName, $"Surname field should contain: {expectedUserModel.LastName}");
        if (isDisabled)
        {
            addUserPage.IsDisabled.Should.BeChecked();
        }
        else
        {
            addUserPage.IsDisabled.Should.Not.BeChecked();
        }

        //  Check created user
        var userDetailsPage = addUserPage.SubmitButton.ClickAndGo();

        Retry.Exponential<InvalidOperationException>(RepeatActionTimes, () =>
            expectedUserModel.Id = Admin.AdminUser.GetList(expectedUserModel.Email).First().Id);
        TestActions.Add(() => Admin.AdminUser.Delete(expectedUserModel.Id));

        userDetailsPage.UserDetails.Email.Should.ContainIgnoringCase(expectedUserModel.Email);
        userDetailsPage.UserDetails.FirstName.Should.ContainIgnoringCase(expectedUserModel.FirstName);
        userDetailsPage.UserDetails.LastName.Should.ContainIgnoringCase(expectedUserModel.LastName);
        var actualUserCreatedDate = userDetailsPage.UserDetails.CreatedDate.Value;
        Assert.IsTrue(actualUserCreatedDate.Contains(expectedDate)
                      || actualUserCreatedDate.Contains(expectedDateOtherVersion), "There should be User creation date displayed");

        userDetailsPage.UserDetails.Status.Should.ContainIgnoringCase(isDisabled
            ? StatusDetails.Disabled.Description()
            : StatusDetails.Enabled.Description());
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeUi)]
    public void DeleteUserTest()
    {
        #region Setup

        const string modalTitle = "Delete user";
        const string modalDescription = "Are you sure you want to delete this user?";

        var userModel = new UserApiModelBuilder().Build();
        userModel.Id = Admin.AdminUser.Create(userModel);
        TestActions.Add(() => Admin.AdminUser.Delete(userModel.Id));

        #endregion

        //  Check user delete modal appear
        var userDetailsPage = LoginAndGo.To<UserDetailsPage>(Url.ToUserDetails(userModel.Id), Admin);
        userDetailsPage.ActionButtons.DeleteUserButton.Wait(Until.Visible);
        var confirmationModal = userDetailsPage.ActionButtons.DeleteUserButton.ClickAndGo();

        confirmationModal.Title.Should.WithRetry.ContainIgnoringCase(modalTitle);
        confirmationModal.Description.Should.WithRetry.ContainIgnoringCase(modalDescription);

        //  Check user been deleted
        var usersListPage = confirmationModal.SubmitButton.ClickAndGo<UsersListPage>();
        usersListPage.Filter.SearchInput.Wait(Until.Visible);
        usersListPage.Filter.SearchInput.Set(userModel.Email);

        usersListPage.GetUser(userModel.Email).Should.WithRetry.Not.Exist();
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeUi)]
    public void DeleteUserCompanyAndNewsConfirmationModalsTest()
    {
        #region Setup

        const string companyDeleteModalTitle = "Delete user companies";
        const string companyDeleteModalDescription = "Are you sure you want to delete user companies?";
        const string newsDeleteModalTitle = "Delete user news";
        const string newsDeleteModalDescription = "Are you sure you want to delete user news?";

        #endregion

        //  Check user got news and companies
        var userDetailsPage = LoginAndGo.To<UserDetailsPage>(Url.ToUserDetails(SelfSignedContributorMailtrap.Id), Admin);

        userDetailsPage.Companies[0].Wait(Until.Visible);
        userDetailsPage.News[0].Wait(Until.Visible);

        //  Check companies delete confirmation modal appear
        userDetailsPage.ActionButtons.DeleteCompaniesButton.Wait(Until.Visible);
        var confirmationModal = userDetailsPage.ActionButtons.DeleteCompaniesButton.ClickAndGo();

        confirmationModal.Title.Should.WithRetry.ContainIgnoringCase(companyDeleteModalTitle);
        confirmationModal.Description.Should.WithRetry.ContainIgnoringCase(companyDeleteModalDescription);

        //  Check cancel companies delete
        confirmationModal.CancelButton.ClickAndGo();

        userDetailsPage.Companies[0].Wait(Until.Visible);
        userDetailsPage.News[0].Wait(Until.Visible);

        //  Check user news delete modal appear
        userDetailsPage.ActionButtons.DeleteNewsButton.Wait(Until.Visible);
        confirmationModal = userDetailsPage.ActionButtons.DeleteNewsButton.ClickAndGo();

        confirmationModal.Title.Should.WithRetry.ContainIgnoringCase(newsDeleteModalTitle);
        confirmationModal.Description.Should.WithRetry.ContainIgnoringCase(newsDeleteModalDescription);

        //  Check cancel news delete
        confirmationModal.CancelButton.ClickAndGo();

        userDetailsPage.Companies[0].Wait(Until.Visible);
        userDetailsPage.News[0].Wait(Until.Visible);
    }
}