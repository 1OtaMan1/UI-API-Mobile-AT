using System;
using System.Diagnostics;
using API.ApiUsers;
using Core.EnvironmentSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.API;

[TestClass]
public class BaseApiTest
{
    public TestContext TestContext { get; set; }

    protected static TenantApiUser ContributorMailtrap = new(ApiRoles.ContributorMailtrap);
    protected static TenantApiUser SelfSignedContributorMailtrap = new(ApiRoles.SelfSignedContributorMailtrap);
    protected static TenantApiUser DisabledContributorMailtrap = new(ApiRoles.DisabledContributorMailtrap);
    protected static TenantApiUser Admin = new(ApiRoles.Admin);

    [TestInitialize]
    public void SetUp()
    {
        Trace.TraceInformation("\nStart time: " + DateTime.Now);
    }

    [TestCleanup]
    public void TearDown()
    {
        var testName = $"{TestContext.TestName}";
        var currentTestOutcome = TestContext.CurrentTestOutcome;
        var message = $"\nEnd time: {DateTime.Now}" +
                      $"\nTest name: '{testName}'" +
                      $"\nStatus: {currentTestOutcome.ToString().ToUpperInvariant()}\n";
        if (currentTestOutcome != UnitTestOutcome.Passed)
        {
            Trace.TraceError(message);
        }
        else
        {
            Trace.TraceInformation(message);
        }
    }
}