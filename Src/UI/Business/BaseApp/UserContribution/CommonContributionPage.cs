using Atata;
using Core.EnvironmentSettings;
using UI.Atata;
using UI.Models;

namespace UI.Business.BaseApp.UserContribution;

public class CommonContributionPage<TOwner> : MainPage<TOwner>
    where TOwner : CommonContributionPage<TOwner>
{
    [FindByCss("input[formcontrolname='firstName']")]
    public CustomEditableTextField<TOwner> FirstnameInput { get; private set; }

    [FindByCss("input[formcontrolname='lastName']")]
    public CustomEditableTextField<TOwner> LastnameInput { get; private set; }

    [FindByCss("input[formcontrolname='email']")]
    public CustomEditableTextField<TOwner> EmailInput { get; private set; }

    [FindByCss("mat-checkbox[formcontrolname='isAgreed'] label")]
    public Clickable<TOwner, TOwner> PrivacyCheckbox { get; private set; }

    [WaitForSuccessAlert]
    [FindByCss("button[type='submit']")]
    public Button<SuccessInfoPage, TOwner> SubmitButton { get; private set; }

    public void FillUserData(UserUiModel userModel)
    {
        FirstnameInput.Wait(Until.Visible);
        FirstnameInput.Set(userModel.FirstName);
        LastnameInput.Set(userModel.LastName);
        EmailInput.Set(userModel.Email);
    }

    public void FillUserData(CredentialsStorage userModel)
    {
        FirstnameInput.Wait(Until.Visible);
        FirstnameInput.Set(userModel.FirstName);
        LastnameInput.Set(userModel.LastName);
        EmailInput.Set(userModel.Email);
    }
}