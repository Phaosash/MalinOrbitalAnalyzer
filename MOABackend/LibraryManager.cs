using ErrorLogging;
using MOABackend.DataManagers;

namespace MOABackend;

public class LibraryManager {
    private LinkedList<double> _sensorA = new();
    private LinkedList<double> _sensorB = new();

    //  This method is responsible for generating sensor data for two sensors, Sensor A and Sensor B, using the
    //  DataCreation.LoadData method. It takes two parameters, average and deviation, which are used to generate
    //  data for both sensors. The method calls LoadData twice: once for Sensor A (with the true flag) and once for
    //  Sensor B (with the false flag). If both data loads succeed, the method returns true. If an error occurs during
    //  the data creation process, the method logs the error using the LoggingHandler and returns false.
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

    //  This method returns a LinkedList<double> containing sensor data based on the isSensorA parameter.
    //  If isSensorA is true, it returns the data for Sensor A (_sensorA); if isSensorA is false, it
    //  returns the data for Sensor B (_sensorB). This method allows access to the sensor data for either
    //  sensor by passing the appropriate boolean value.
    public LinkedList<double> GetSensorData (bool isSensorA){
        return isSensorA ? _sensorA : _sensorB;
    }

    //  This method attempts to sort either _sensorA or _sensorB using either SelectionSort or InsertionSort,
    //  depending on the isSensorA flag. If the sorting is successful, it returns true, otherwise, it catches
    //  any exceptions, logs them, and returns false. The method chooses the appropriate sorting algorithm based
    //  on the flag and works with the corresponding sensor data.
    public bool RunSort (bool isSensorA){
        try {
            var sensorData = isSensorA ? _sensorA : _sensorB;
            if (isSensorA) {
                SortingAlgorithms.SelectionSort(sensorData);
            } else {
                SortingAlgorithms.InsertionSort(sensorData);
            }

            LoggingHandler.Instance.LogInformation($"Sorting completed successfully for {(isSensorA ? "Sensor A" : "Sensor B")}.");

            return true;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the RunSort method in Library Manager", ex);
            return false;
        }
    }

    //  This method attempts to perform an iterative binary search on either _sensorA or _sensorB, depending on the isSensorA flag.
    //  It first checks if the data is sorted using the DataValidator.IsSorted method. If sorted, it performs a binary search;
    //  otherwise, it logs that the data is not sorted and returns -999. If an exception occurs, it logs the error and returns -666.
    public int RunIterativeSearch (bool isSensorA, int searchValue){
        try {
            LinkedList<double> sensorData = isSensorA ? _sensorA : _sensorB;

            bool isSorted = DataValidator.IsSorted(sensorData);

            if (isSorted){
                return BinarySearches.BinarySearchIterative(sensorData, searchValue, 0, sensorData.Count - 1);
            } else {
                LoggingHandler.Instance.LogInformation("Sensor data is not sorted");
                return -999;
            }
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the RunIterativeSearch method in Library Manager", ex);
            return -666;
        }
    }

    //  This method performs a recursive binary search on either _sensorA or _sensorB, based on the isSensorA flag.
    //  It first checks if the data is sorted using DataValidator.IsSorted. If the data is sorted, it proceeds with the
    //  recursive binary search. If not, it logs the issue and returns -999. If an exception occurs, it logs the error
    //  and returns -666.
    public int RunRecursiveSearch (bool isSensorA, int searchValue){
        try {
            LinkedList<double> sensorData = isSensorA ? _sensorA : _sensorB;

            bool isSorted = DataValidator.IsSorted(sensorData);

            if (isSorted){
                return BinarySearches.BinarySearchRecursive(sensorData, searchValue, 0, sensorData.Count - 1);
            } else {
                LoggingHandler.Instance.LogInformation("Sensor data is not sorted");
                return -999;
            }  
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the RunRecursiveSearch method in Library Manager", ex);
            return -666;
        }
    }
}