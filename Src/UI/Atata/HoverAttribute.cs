using Atata;

namespace UI.Atata;

public class HoverAttribute : TriggerAttribute
{
    public HoverAttribute(TriggerEvents on = TriggerEvents.BeforeClick, TriggerPriority priority = TriggerPriority.Low)
        : base(on, priority)
    {
    }

    protected override void Execute<TOwner>(TriggerContext<TOwner> context)
    {
        ((Control<TOwner>)context.Component).Hover();
    }
}