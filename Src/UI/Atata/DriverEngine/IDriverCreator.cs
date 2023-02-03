using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UI.Atata.DriverEngine;

public interface IDriverCreator
{
    AtataContext CreateWebDriver(TestContext context);
}