using ErrorLogging;
using MOABackend.DataManagers;
using MOABackend.DataModels;

namespace MOABackend;

public class LibraryManager {
    private LinkedList<double> _sensorA = new();
    private LinkedList<double> _sensorB = new();

    public bool CreateSensorData (double average, double deviation){
        try {
            _sensorA = DataCreation.LoadData(average, deviation, true);
            _sensorB = DataCreation.LoadData(average, deviation, false);
        
            return true;
        } catch (Exception ex){ 
            LoggingHandler.Instance.LogError("Encountered an unexpected problem with the CreateSensorData method in the Library Manager", ex);
            return false; 
        }
    }

    public LinkedList<double> GetSensorData (bool isSensorA){
        return isSensorA ? _sensorA : _sensorB;
    }

    public bool RunSort (bool isSensorA){
        try {
            var sensorData = isSensorA ? _sensorA : _sensorB;
            SortAlgorithm sortAlgorithm = new();
            var algorithmToUse = isSensorA ? sortAlgorithm.SelectionSort : sortAlgorithm.InsertionSort;

            return algorithmToUse switch {
                "SelectionSort" => SortingAlgorithms.SelectionSort(sensorData),
                "InsertionSort" => SortingAlgorithms.InsertionSort(sensorData),
                _ => throw new ArgumentException("Unsupported sorting algorithm")
            };
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the RunSort method in Library Manager", ex);
            return false;
        }
    }

    public int RunSearch (bool isSensorA, int searchValue){
        try {
            var sensorData = isSensorA ? _sensorA : _sensorB;
            SearchTypes searchType = new();
            var searchToUse = isSensorA ? searchType.Iterative : searchType.Recursive;

            if (!DataValidator.IsSorted(sensorData)){
                return -999;
            }

            return searchToUse switch {
                "Iterative" => BinarySearches.BinarySearchIterative(sensorData, searchValue, 0, sensorData.Count - 1),
                "Recursive" => BinarySearches.BinarySearchRecursive(sensorData, searchValue, 0, sensorData.Count - 1),
                _ => throw new ArgumentException("Unsupported search type")
            };
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered an unexpected problem with the RunSearch method in the Library Manager", ex);
            return -666;
        }
    }
}