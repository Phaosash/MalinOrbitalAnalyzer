using ErrorLoggingLibrary;
using System.Diagnostics;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal static class ActionTimer {
    //  This method measures the time it takes to execute a given action. It accepts a Func<bool>
    //  delegate called action, which represents the action to be executed. The method uses a
    //  Stopwatch to record the elapsed time, calls the action, and stores the result in the out parameter
    //  result. The time taken for the action to execute is returned in milliseconds. If an exception
    //  occurs during execution, the method logs the error using LoggingHandler.Instance.LogError, sets
    //  result to false, and returns 0 to indicate failure.
    public static long TimeAction (Func<bool> action, out bool result){
        try {
            Stopwatch sw = Stopwatch.StartNew();
            result = action();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError ("Time action method in ActionTimer class: ", ex);
            result = false;
            return 0;
        }
    }

    //  This method measures the time taken to execute a given action, but in this case, the action is
    //  expected to return an int value. It accepts a Func<int> delegate called action and uses a Stopwatch
    //  to track the elapsed time in ticks (the smallest unit of time the Stopwatch can measure). The result
    //  of the action is stored in the out parameter result. If an exception occurs during execution, the
    //  method logs the error using LoggingHandler.Instance.LogError, sets result to -1, and returns 0 to
    //  indicate failure. The method returns the elapsed time in ticks rather than milliseconds.
    public static long TimeAction (Func<int> action, out int result){
        try {
            Stopwatch sw = Stopwatch.StartNew();
            result = action();
            sw.Stop();
            return sw.ElapsedTicks;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError ("Time action method in TimeAction class: ", ex);
            result = -1;
            return 0;
        }
    }
}