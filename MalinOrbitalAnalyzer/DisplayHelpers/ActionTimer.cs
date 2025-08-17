using System.Diagnostics;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal static class ActionTimer {
    public static long TimeAction (Func<bool> action, out bool result){
        Stopwatch sw = Stopwatch.StartNew();
        result = action();
        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    public static long TimeAction (Func<int> action, out int result){
        Stopwatch sw = Stopwatch.StartNew();
        result = action();
        sw.Stop();
        return sw.ElapsedTicks;
    }
}