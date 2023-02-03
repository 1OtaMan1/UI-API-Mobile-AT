using Atata;
using UI.Business.AdminApp.Common.Sections;

namespace UI.Business.AdminApp.Company;

using _ = CompaniesListPage;

[PageObjectDefinition("article")]
public class CompaniesListPage : MainAdminAppPage<_>
{
    public Filter<_> Filter { get; private set; }
}