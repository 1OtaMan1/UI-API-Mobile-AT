using System.Diagnostics;
using Core.Utils;

namespace Core.Helpers;

public static class CleanupActions
{
    public static void Execute(List<Action> actions = null, int repeatCount = 1)
    {
        Trace.TraceInformation("\n***************             TEST TEARDOWN             *************** " +
                               "\n________________________________START________________________________\n");

        if (actions != null)
        {
            actions.Reverse();
            actions.ForEach(action =>
            {
                try
                {
                    Retry.Exponential<Exception>(repeatCount, action);
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
                {
                    Trace.TraceWarning($"Failed to make cleanup action : {ex.Message}");
                }
            });
        }
        else
        {
            Trace.TraceInformation("Cleanup action list is empty");
        }

        Trace.TraceInformation("\n_________________________________END_________________________________\n");
    }
}