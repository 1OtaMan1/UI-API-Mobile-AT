using Atata;

namespace UI.Atata;

public class WaitForPageLoadAttribute : WaitForScriptAttribute
{
    public WaitForPageLoadAttribute(TriggerEvents on = TriggerEvents.None,
        TriggerPriority priority = TriggerPriority.Medium)
        : base(on, priority)
    {
        Timeout = 30;
        RetryInterval = 1;
        AppliesTo = TriggerScope.Children;
    }

    protected override string BuildReportMessage<TOwner>(TriggerContext<TOwner> context)
        => "Wait for page load all components";

    protected override string BuildScript<TOwner>(TriggerContext<TOwner> context)
        => @"

        function sleep() {
            var start = new Date().getTime();
            while (new Date().getTime() < start + 300);
        }
        

        function spinnersAreMissing() {
            return (!window.isPendingRequest) && 
                   (document.getElementsByClassName('spin-box').length == 0) &&
                   (document.getElementsByClassName('backdrop').length == 0) &&
                   (document.getElementsByClassName('backdrop ng-star-inserted').length == 0) &&
                   (document.getElementsByClassName('ng-pending').length == 0) &&
                   (document.getElementsByClassName('spinner').length == 0);          
        }

        
        var result = false;
        console.log('looping');
        for(var i = 0; i < 5; i++) {    
            sleep();
            console.log('iteration ' + i);
            result = spinnersAreMissing();
            if (result == false) {
                break;
            }           
        }
    
        return result;";
}