using System;
using System.Collections.Generic;
using System.Linq;
using API.Models.Admin.Feedback;
using Core.Helpers;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Core.Constants.TestCategories;

namespace Tests.API.Feedback;

[TestClass]
public class AdminFeedbackTests : BaseApiTest
{
    private static readonly AdminFeedbackListItemApiModel ExpectedFeedback =
        Admin.AdminFeedback.GetList().First(_ => _.CreatorId.Equals(SelfSignedContributorMailtrap.Id)); // Hardcoded data, please restore if it been deleted

    private static readonly List<Action> TestActions = new();

    [TestCleanup]
    public void RemoveTestData()
    {
        CleanupActions.Execute(TestActions);
        TestActions.Clear();
    }

    [TestMethod]
    [StoryId(46809), TestCategory(SmokeApi)]
    public void AdminShouldReplyFeedbackTest()
    {
        var replyModel = new AdminFeedbackReplyAddApiModel
        { Email = ExpectedFeedback.Email, FeedbackId = ExpectedFeedback.Id, ReplyMessage = Generator.Reply() };
        Admin.AdminFeedback.Reply(replyModel);
        Assert.IsTrue(Admin.AdminFeedback.GetRepliesList(ExpectedFeedback.Id).Any(_ => _.ReplyMessage.Equals(replyModel.ReplyMessage)),
            "Admin should add Feedback Reply");
    }

    [TestMethod]
    [StoryId(46809), TestCategory(SmokeApi)]
    public void AdminShouldGetFeedbackListTest()
    {
        var actualFeedbackList = Admin.AdminFeedback.GetList();
        Assert.IsTrue(actualFeedbackList.Any(_ => _.Id.Equals(ExpectedFeedback.Id)), "Admin should get Feedback list");
    }

    [TestMethod]
    [StoryId(46809), TestCategory(SmokeApi)]
    public void AdminShouldGetFeedbackCountTest()
    {
        var feedbackCount = Admin.AdminFeedback.GetCount();
        Assert.IsTrue(feedbackCount.AllFeedbacks > (int)default, "Admin should get Feedback counts");
    }

    [TestMethod]
    [StoryId(46809), TestCategory(SmokeApi)]
    public void AdminShouldGetFeedbackRepliesCountTest()
    {
        var repliesCount = Admin.AdminFeedback.GetRepliesCount(ExpectedFeedback.Id);
        Assert.IsTrue(repliesCount > (int)default, "Admin should get Feedback Replies count");
    }

    [TestMethod]
    [StoryId(46809), TestCategory(SmokeApi)]
    public void AdminShouldGetFeedbackDetailsTest()
    {
        var actualFeedback = Admin.AdminFeedback.Get(ExpectedFeedback.Id);
        Assert.AreEqual(ExpectedFeedback.FeedbackMessage, actualFeedback.FeedbackMessage, "Admin should get Feedback");
    }

    [TestMethod]
    [StoryId(46809), TestCategory(SmokeApi)]
    public void AdminShouldGetFeedbackRepliesListTest()
    {
        var repliesList = Admin.AdminFeedback.GetRepliesList(ExpectedFeedback.Id);
        Assert.IsTrue(repliesList.Any(), "Admin should get Feedback Replies list");
    }
}