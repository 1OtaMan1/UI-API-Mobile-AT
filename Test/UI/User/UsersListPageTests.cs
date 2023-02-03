using System;
using System.Linq;
using Atata;
using Core.Extensions;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Atata.Extensions;
using UI.Business.AdminApp.User;
using UI.Models.Enums;
using static Core.Constants.Sizes;
using static Core.Constants.TestCategories;

namespace Tests.UI.User;

[TestClass]
public class UsersListPageTests : BaseUiTest
{
    [TestMethod]
    [StoryId(46848), TestCategory(SmokeUi)]
    public void UsersListMainElementsDisplayTest()
    {
        //  Check users counter displayed
        var usersListPage = LoginAndGo.To<UsersListPage>(Url.ToUsersList, Admin);
        usersListPage.Users[0].Wait(Until.Visible);

        var usersCount = Convert.ToInt32(usersListPage.Title.Value.Replace("Users (", "").Replace(")", ""));
        Assert.IsTrue(usersCount > (int)default, "There should be Users count displayed");

        //  Check search input displayed
        usersListPage.Filter.SearchInput.Wait(Until.Visible);

        //  Check user status filter displayed
        usersListPage.Filter.DropdownFilters[0].Wait(Until.Visible);

        //  Check reset filter button not displayed by default
        usersListPage.Filter.ResetButton.Wait(Until.MissingOrHidden);

        //  Check default users ordering
        usersListPage.Filter.SearchInput.Set(ContributorMailtrap.Credentials.LastName);
        usersListPage.Users[1].Wait(Until.Visible);

        Retry.Exponential<AssertFailedException>(RepeatActionTimes, () => 
            Assert.IsTrue(usersListPage.Users[0].Email.Content.Value.Contains(ContributorMailtrap.Credentials.Email),
            "By default Users should be ordered by Email"));
        Assert.IsTrue(usersListPage.Users[1].Email.Content.Value.Contains(SelfSignedContributorMailtrap.Credentials.Email),
            "By default Users should be ordered by Email");

        //  Check users list column
        var userElement = usersListPage.GetUser(ContributorMailtrap.Credentials.Email);

        userElement.Name.Content.Should.ContainIgnoringCase(ContributorMailtrap.Credentials.FirstName);
        userElement.Surname.Content.Should.ContainIgnoringCase(ContributorMailtrap.Credentials.LastName);
        userElement.CreatedDate.Wait(Until.Visible);
        userElement.IsDisabled.Content.Should.ContainIgnoringCase("No");
    }

    [TestMethod]
    [StoryId(46848), TestCategory(SmokeUi)]
    public void UsersListFilterAndOrderingTest()
    {
        //  Check order by Surname
        var usersListPage = LoginAndGo.To<UsersListPage>(Url.ToUsersList, Admin);
        usersListPage.OrderBy.Surname.Wait(Until.Visible);
        usersListPage.OrderBy.Surname.ClickAndGo().Wait(OneItem);

        var adminIndex = usersListPage.Users.IndexOf(_ => _.Email.Content.Value.Contains(Admin.Credentials.Email)).Value;
        var disabledContributorIndex =
            usersListPage.Users.IndexOf(_ => _.Surname.Content.Value.Contains(DisabledContributorMailtrap.Credentials.LastName)).Value;
        Assert.IsTrue(adminIndex < disabledContributorIndex, "Descending order by Surname should work properly");

        usersListPage.OrderBy.Surname.ClickAndGo().Wait(OneItem);

        adminIndex = usersListPage.Users.IndexOf(_ => _.Email.Content.Value.Contains(Admin.Credentials.Email)).Value;
        disabledContributorIndex = usersListPage.Users.IndexOf(_ => _.Surname.Content.Value.Contains(DisabledContributorMailtrap.Credentials.LastName)).Value;
        Assert.IsTrue(adminIndex > disabledContributorIndex, "Ascending order by Surname should work properly");

        //  Check filtering by email and reset button appear
        usersListPage.Filter.SearchInput.Set(ContributorMailtrap.Credentials.Email);

        usersListPage.GetUser(ContributorMailtrap.Credentials.Email).Wait(Until.Visible);
        usersListPage.Filter.ResetButton.Wait(Until.Visible);
        Assert.IsTrue(usersListPage.Users.All(user => user.Email.Content.Value.Contains(ContributorMailtrap.Credentials.Email)),
            "Users should be filtered by Email");

        //  Check filtered by name
        usersListPage.Filter.SearchInput.Set(SelfSignedContributorMailtrap.Credentials.FirstName);

        usersListPage.GetUser(SelfSignedContributorMailtrap.Credentials.Email).Wait(Until.Visible);
        Assert.IsTrue(usersListPage.Users.All(user => user.Name.Content.Value.Contains(SelfSignedContributorMailtrap.Credentials.FirstName)),
            "Users should be filtered by Email");

        //  Check filtered by surname
        usersListPage.Filter.SearchInput.Set(ContributorMailtrap.Credentials.LastName);

        usersListPage.GetUser(ContributorMailtrap.Credentials.Email).Wait(Until.Visible);
        Assert.IsTrue(usersListPage.Users.All(user => user.Surname.Content.Value.Contains(ContributorMailtrap.Credentials.LastName)),
            "Users should be filtered by Email");

        //  Check order by name while been filtered
        usersListPage.OrderBy.Name.ClickAndGo();

        Retry.Exponential<AssertFailedException>(RepeatActionTimes, () =>
            Assert.IsTrue(usersListPage.Users[0].Email.Content.Value.Contains(DisabledContributorMailtrap.Credentials.Email),
                "By default Users should be ordered by Email"));
        Assert.IsTrue(usersListPage.Users[1].Email.Content.Value.Contains(ContributorMailtrap.Credentials.Email),
            "By default Users should be ordered by Email");

        usersListPage.OrderBy.Name.ClickAndGo();

        Retry.Exponential<AssertFailedException>(RepeatActionTimes, () =>
            Assert.IsTrue(usersListPage.Users[0].Email.Content.Value.Contains(SelfSignedContributorMailtrap.Credentials.Email),
                "By default Users should be ordered by Email"));
        Assert.IsTrue(usersListPage.Users[1].Email.Content.Value.Contains(ContributorMailtrap.Credentials.Email),
            "By default Users should be ordered by Email");

        //  Check filtering by status
        usersListPage.Filter.SearchInput.Clear();
        usersListPage.Filter.DropdownFilters[0].SelectOption(StatusFilter.Disabled.Description());

        usersListPage.GetUser(DisabledContributorMailtrap.Credentials.Email).Wait(Until.Visible);
        usersListPage.GetUser(ContributorMailtrap.Credentials.Email).Wait(Until.MissingOrHidden);

        //  Check reset filter work
        usersListPage.Filter.ResetButton.ClickAndGo();

        usersListPage.GetUser(DisabledContributorMailtrap.Credentials.Email).Wait(Until.Visible);
        usersListPage.GetUser(ContributorMailtrap.Credentials.Email).Wait(Until.Visible);

        //  Check order by creation date
        usersListPage.OrderBy.CreatedDate.ClickAndGo().Wait(OneItem);

        adminIndex = usersListPage.Users.IndexOf(_ => _.Email.Content.Value.Contains(Admin.Credentials.Email)).Value;
        disabledContributorIndex = usersListPage.Users.IndexOf(_ => _.Name.Content.Value.Contains(DisabledContributorMailtrap.Credentials.FirstName)).Value;
        Assert.IsTrue(adminIndex < disabledContributorIndex, "Descending order by Creation Date should work properly");

        usersListPage.OrderBy.CreatedDate.ClickAndGo().Wait(OneItem);

        adminIndex = usersListPage.Users.IndexOf(_ => _.Email.Content.Value.Contains(Admin.Credentials.Email)).Value;
        disabledContributorIndex = usersListPage.Users.IndexOf(_ => _.Name.Content.Value.Contains(DisabledContributorMailtrap.Credentials.FirstName)).Value;
        Assert.IsTrue(adminIndex > disabledContributorIndex, "Ascending order by Creation Date should work properly");

        //  Check order by status
        usersListPage.OrderBy.IsBlocked.ClickAndGo().Wait(OneItem);

        adminIndex = usersListPage.Users.IndexOf(_ => _.Email.Content.Value.Contains(Admin.Credentials.Email)).Value;
        disabledContributorIndex = usersListPage.Users.IndexOf(_ => _.Name.Content.Value.Contains(DisabledContributorMailtrap.Credentials.FirstName)).Value;
        Assert.IsTrue(adminIndex < disabledContributorIndex, "Descending order by Status should work properly");

        usersListPage.OrderBy.IsBlocked.ClickAndGo().Wait(OneItem);

        adminIndex = usersListPage.Users.IndexOf(_ => _.Email.Content.Value.Contains(Admin.Credentials.Email)).Value;
        disabledContributorIndex = usersListPage.Users.IndexOf(_ => _.Name.Content.Value.Contains(DisabledContributorMailtrap.Credentials.FirstName)).Value;
        Assert.IsTrue(adminIndex > disabledContributorIndex, "Ascending order by Status should work properly");

        //  Check order by email
        usersListPage.OrderBy.Email.ClickAndGo().Wait(OneItem);

        adminIndex = usersListPage.Users.IndexOf(_ => _.Email.Content.Value.Contains(Admin.Credentials.Email)).Value;
        disabledContributorIndex = usersListPage.Users.IndexOf(_ => _.Name.Content.Value.Contains(DisabledContributorMailtrap.Credentials.FirstName)).Value;
        Assert.IsTrue(adminIndex < disabledContributorIndex, "Descending order by Status should work properly");

        usersListPage.OrderBy.Email.ClickAndGo().Wait(OneItem);

        adminIndex = usersListPage.Users.IndexOf(_ => _.Email.Content.Value.Contains(Admin.Credentials.Email)).Value;
        disabledContributorIndex = usersListPage.Users.IndexOf(_ => _.Name.Content.Value.Contains(DisabledContributorMailtrap.Credentials.FirstName)).Value;
        Assert.IsTrue(adminIndex > disabledContributorIndex, "Ascending order by Status should work properly");
    }
}