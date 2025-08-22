using ErrorLoggingLibrary;
using MOABackend.DataManagers;

namespace MOABackend;

public class LibraryManager {
    private LinkedList<double> _sensorA;
    private LinkedList<double> _sensorB;

    //  This constructor initializes two collections, _sensorA and _sensorB, as new instances—likely to store sensor data separately.
    public LibraryManager (){
        _sensorA = new();
        _sensorB = new();
    }

    //  This method generates and assigns sensor A and B data based on given average and deviation values, returning true if successful.
    //  If an error occurs, it logs the exception and returns false.
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

    //  This method simply returns the number of items currently stored in the _sensorA collection.
    public int ReturnSensorACount (){
        return _sensorA.Count;
    }

    //  This method simply returns the number of items currently stored in the _sensorB collection.
    public int ReturnSensorBCount (){
        return _sensorB.Count;
    }

    //  This method returns the linked list containing sensor A’s data.
    public LinkedList<double> GetSensorDataA (){
        return _sensorA;
    }

    //  This method returns the linked list containing sensor B's data.
    public LinkedList<double> GetSensorDataB (){
        return _sensorB;
    }

    //  This method checks if sensor A’s data is sorted and, if so, performs an iterative binary search for the target value, returning the found index.
    //  If the data isn’t sorted, it logs this and returns -999, and if an exception occurs, it logs the error and returns -666.
    public int PerformIterativeSearchA (int searchTarget){
        try {
            bool isSorted = DataValidator.IsSorted(_sensorA);

            if (isSorted){
                return BinarySearches.BinarySearchIterative(_sensorA, searchTarget, 0, _sensorA.Count - 1);
            } else {
                LoggingHandler.Instance.LogInformation("Sensor data is not sorted");
                return -999;
            }
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the PerformIterativeSearchA method in Library Manager", ex);
            return -666;
        }
    }

    //  This method verifies if sensor B’s data is sorted and performs an iterative binary search for the target value within it, returning the found index.
    //  If the data is unsorted, it logs the information and returns -999, and if an error occurs, it logs the exception and returns -666.
    public int PerformIterativeSearchB (int searchTarget){
        try {
            bool isSorted = DataValidator.IsSorted(_sensorB);

            if (isSorted){
                return BinarySearches.BinarySearchIterative(_sensorB, searchTarget, 0, _sensorB.Count - 1);
            }
            else {
                LoggingHandler.Instance.LogInformation("Sensor data is not sorted");
                return -999;
            }
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the PerformIterativeSearchB method in Library Manager", ex);
            return -666;
        }
    }

    //  This method applies a selection sort to sensor A’s data and logs a success message upon completion,
    //  while catching and logging any exceptions that occur during sorting.
    public void PerformSelectionSortA (){
        try {
            SortingAlgorithms.SelectionSort(_sensorA);
            LoggingHandler.Instance.LogInformation($"Sorting completed successfully for Sensor A.");
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the PerformSelectionSortA method in Library Manager", ex);
        }
    }

    //  This method sorts sensor B’s data using selection sort and logs a success message when finished, handling and logging any exceptions that arise during the process.
    public void PerformSelectionSortB(){
        try {
            SortingAlgorithms.SelectionSort(_sensorB);
            LoggingHandler.Instance.LogInformation($"Selection sorting completed successfully for Sensor B.");
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the PerformSelectionSortB method in Library Manager", ex);
        }
    }

    //  This method sorts sensor A’s data using insertion sort and logs a success message upon completion, while catching and logging any errors that occur during sorting.
    public void PerformInsertionSortA (){
        try {
            SortingAlgorithms.InsertionSort(_sensorA);
            LoggingHandler.Instance.LogInformation($"Insertion sorting completed successfully for Sensor A.");
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the PerformInsertionSortA method in Library Manager", ex);
        }
    }

    //  This method sorts sensor B’s data using insertion sort, logging a success message when done and capturing any exceptions to log errors.
    public void PerformInsertionSortB (){
        try {
            SortingAlgorithms.InsertionSort(_sensorB);
            LoggingHandler.Instance.LogInformation($"Insertion sorting completed successfully for Sensor B.");
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the PerformInsertionSortB method in Library Manager", ex);
        }
    }

    //  This method checks if sensor A’s data is sorted and performs a recursive binary search for the target value, returning the index if found.
    //  If the data isn’t sorted, it logs this and returns -999; if an exception occurs, it logs the error and returns -666.
    public int PerformRecursiveSearchA (int searchTarget){
        try {
            bool isSorted = DataValidator.IsSorted(_sensorA);

            if (isSorted){
                return BinarySearches.BinarySearchRecursive(_sensorA, searchTarget, 0, _sensorA.Count - 1);
            } else {
                LoggingHandler.Instance.LogInformation("Sensor A's data wasn't sorted when trying to perform the recursive search.");
                return -999;
            }
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the PerformRecursiveSearchA method in Library Manager", ex);
            return -666;
        }
    }

    //  This method verifies that sensor B’s data is sorted before performing a recursive binary search for the target value, returning the index if found.
    //  If the data isn’t sorted, it logs this and returns -999, while exceptions are caught, logged, and result in a -666 return.
    public int PerformRecursiveSearchB (int searchTarget){
        try {
            bool isSorted = DataValidator.IsSorted(_sensorB);

            if (isSorted){
                return BinarySearches.BinarySearchRecursive(_sensorB, searchTarget, 0, _sensorB.Count - 1);
            } else {
                LoggingHandler.Instance.LogInformation("Sensor B's Data wasnt sorted when trying to perform the recursive search.");
                return -999;
            }
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the PerformRecursiveSearchB method in Library Manager", ex);
            return -666;
        }
    }
}