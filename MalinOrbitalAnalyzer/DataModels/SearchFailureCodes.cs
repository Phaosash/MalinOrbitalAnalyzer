namespace MalinOrbitalAnalyzer.DataModels;

internal readonly struct SearchFailureCodes {
    //  Used to detectct if a critical failure has occured when performing the searches
    public static readonly int CRITICAL_SEARCH_FAILURE_CODE = -666;

    //  Used to detect if that data hasn't been sorted when performing the searches
    public static readonly int DATA_NOT_SORTED_ERROR_CODE = -999;
}
