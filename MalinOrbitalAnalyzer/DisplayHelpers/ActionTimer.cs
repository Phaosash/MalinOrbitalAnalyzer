using ErrorLoggingLibrary;
using System.Diagnostics;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal static class ActionTimer {
    //  This method executes a given function, records how long it takes in stopwatch ticks, and outputs the function's result.
    //  If an exception occurs, it logs the error, sets the result to -1, and returns 0.

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

    //  This method measures the execution time in milliseconds of a given action and returns the duration. If an exception is thrown,
    //  it logs the error and returns 0.
    public static long TimeAction (Action action){
        try {
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Time action method in TimeAction class: ", ex);
            return 0;
        }
    }
}