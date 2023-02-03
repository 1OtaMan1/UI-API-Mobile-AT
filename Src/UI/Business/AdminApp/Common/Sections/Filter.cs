using Atata;
using UI.Atata;

namespace UI.Business.AdminApp.Common.Sections;

[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
[ControlDefinition("div", ContainingClass = "filter-container")]
public class Filter<TOwner> : Control<TOwner>
    where TOwner : PageObject<TOwner>
{
    [FindByCss("input")]
    public CustomEditableTextField<TOwner> SearchInput { get; private set; }

    public ControlList<DropdownFilter, TOwner> DropdownFilters { get; private set; }

    [ControlDefinition("div", ContainingClass = "filter-select-label")]
    public class DropdownFilter : Control<TOwner>
    {
        public Clickable<TOwner, TOwner> Self { get; private set; }

        [FindByXPath("./preceding-sibling::label")]
        public Control<TOwner> Label { get; private set; }

        public Clickable<TOwner, TOwner> Option(string option)
        {
            return Controls.Create<Clickable<TOwner, TOwner>>("Option", new FindByXPathAttribute(
                $"option[contains(text(), '{option}')]"),
                new WaitForPageLoadAttribute(TriggerEvents.BeforeAndAfterClick, TriggerPriority.High) { AppliesTo = TriggerScope.Self });
        }

        public TOwner SelectOption(string option)
        {
            if (!Option(option).IsVisible)
            {
                Self.ClickAndGo();
            }

            return Option(option).ClickAndGo();
        }
    }

    [FindByClass("btn-reset-filter")]
    public Button<TOwner, TOwner> ResetButton { get; private set; }
}