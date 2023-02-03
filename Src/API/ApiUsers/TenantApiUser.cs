using API.Interfaces;
using API.Mailtrap;
using API.Rest;
using API.Services.Account;
using API.Services.AdminCompany;
using API.Services.AdminDashboard;
using API.Services.AdminFeedback;
using API.Services.AdminNews;
using API.Services.AdminUser;
using API.Services.Company;
using API.Services.News;
using API.Services.User;
using Core.EnvironmentSettings;

namespace API.ApiUsers;

public class TenantApiUser
{
    public readonly CredentialsStorage Credentials;

    public static implicit operator CredentialsStorage(TenantApiUser user) => user.Credentials;

    public TenantApiUser(CredentialsStorage credentials)
    {
        Credentials = credentials;
    }

    public Guid Id => Credentials.Id;

    public IMailBox Mailbox => new MailtrapInbox(Credentials);

    public IAccountService Account => new AccountService(new RequestManager(RestClientTypes.Base, Credentials, ApplicationUrls.Api));

    public IAdminCompanyService AdminCompany => new AdminCompanyService(new RequestManager(RestClientTypes.Admin, Credentials, ApplicationUrls.Api));

    public IAdminDashboardService AdminDashboard => new AdminDashboardService(new RequestManager(RestClientTypes.Admin, Credentials, ApplicationUrls.Api));

    public IAdminFeedbackService AdminFeedback => new AdminFeedbackService(new RequestManager(RestClientTypes.Admin, Credentials, ApplicationUrls.Api));

    public IAdminNewsService AdminNews => new AdminNewsService(new RequestManager(RestClientTypes.Admin, Credentials, ApplicationUrls.Api));

    public IAdminUserService AdminUser => new AdminUserService(new RequestManager(RestClientTypes.Admin, Credentials, ApplicationUrls.Api));

    public ICompanyService Company => new CompanyService(new RequestManager(RestClientTypes.Base, Credentials, ApplicationUrls.Api));

    public IUserService User => new UserService(new RequestManager(RestClientTypes.Base, Credentials, ApplicationUrls.Api));

    public INewsService News => new NewsService(new RequestManager(RestClientTypes.Base, Credentials, ApplicationUrls.Api));
}