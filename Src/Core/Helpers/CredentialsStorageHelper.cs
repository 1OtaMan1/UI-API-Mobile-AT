using Bogus;
using Core.EnvironmentSettings;

namespace Core.Helpers;

public static class CredentialsStorageHelper
{
    public static CredentialsStorage Create(string? email = null)
    {
        var faker = new Faker();

        var storage = new CredentialsStorage {FirstName = faker.Person.FirstName.Replace("'", ""), LastName = faker.Person.LastName.Replace("'", "")};
        storage.Email = email ?? $"taf.user.test+{storage.FirstName.ToLower() + "." + storage.LastName.ToLower() + Generator.TwoDigitNumber()}@gmail.com";
        storage.MailServerPassword = "none";
        storage.Id = Guid.Empty;

        return storage;
    }
}