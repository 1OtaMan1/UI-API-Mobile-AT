using Atata;

namespace UI.Atata;

[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
public class CustomEditableTextField<TOwner> : EditableField<string, TOwner> 
    where TOwner : PageObject<TOwner>
{
    protected override string GetValue()
    {
        return Scope.Text;
    }

    protected override void SetValue(string value)
    {
        Scope.FillInWith(value);
    }

    public TOwner Clear()
    {
        ExecuteTriggers(TriggerEvents.BeforeSet);
        Log.Start(new DataClearingLogSection(this));

        OnClear();

        Log.EndSection();
        ExecuteTriggers(TriggerEvents.AfterSet);

        return Owner;
    }

    protected virtual void OnClear()
    {
        Scope.Clear();
    }
}