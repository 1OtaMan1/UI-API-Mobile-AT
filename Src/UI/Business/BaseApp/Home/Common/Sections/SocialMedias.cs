using Atata;
using UI.Atata;

namespace UI.Business.BaseApp.Home.Common.Sections;

[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
[ControlDefinition("app-social-media-panel")]
public class SocialMedias<TOwner> : Control<TOwner>
    where TOwner : PageObject<TOwner>
{
    [FindByCss("share-button[button='facebook']")]
    public Button<TOwner, TOwner> FacebookIcon { get; private set; }

    [FindByCss("share-button[button='twitter']")]
    public Button<TOwner, TOwner> TwitterIcon { get; private set; }

    [FindByCss("share-button[button='linkedin']")]
    public Button<TOwner, TOwner> LinkedInIcon { get; private set; }

    [FindByCss("share-button[button='email']")]
    public Button<TOwner, TOwner> EmailIcon { get; private set; }

    [WaitForSuccessAlert]
    [FindByCss("share-button[button='copy']")]
    public Button<TOwner, TOwner> LinkIcon { get; private set; }
}