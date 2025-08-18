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

    public int RunIterativeSearch (bool isSensorA, int searchValue){
        LinkedList<double> sensorData = isSensorA ? _sensorA : _sensorB;

        bool isSorted = DataValidator.IsSorted(sensorData);

        if (isSorted){
            return BinarySearches.BinarySearchIterative(sensorData, searchValue, 0, sensorData.Count - 1);
        } else {
            LoggingHandler.Instance.LogInformation("Sensor data is not sorted");
            return -999;
        }
    }

    public int RunRecursiveSearch (bool isSensorA, int searchValue){
        LinkedList<double> sensorData = isSensorA ? _sensorA : _sensorB;

        bool isSorted = DataValidator.IsSorted(sensorData);

        if (isSorted){
            return BinarySearches.BinarySearchRecursive(sensorData, searchValue, 0, sensorData.Count - 1);
        } else {
            LoggingHandler.Instance.LogInformation("Sensor data is not sorted");
            return -999;
        }        
    }
}