using MOABackend.DataHandlers;

namespace MOABackend;

public class SensorManagerB {
    //  Programming requirements 4.1
    private LinkedList<double> _sensorB = new();

    //  This method returns a LinkedList of double values representing the data stored in _sensorB
    public LinkedList<double> GetSensorA (){
        return _sensorB;
    }

    //  This method clears the existing sensor data and reloads it using the `DataLoader.LoadData` method
    //  with the provided `average` and `deviation` values, ensuring new data is generated.
    public void SetSensorB (double average, double deviation){
        _sensorB.Clear();
        _sensorB = DataLoader.LoadData(_sensorB, average, deviation, true);
    }

    //  This method checks if the data is sorted; if so, it performs an iterative binary search for targetValue
    //  and returns the index. If not sorted, it returns -999
    public int RunIterativeSearch (int targetValue){
        bool isSorted = DataValidator.IsSorted(_sensorB);

        if (isSorted){
            return BinarySearches.BinarySearchIterative(_sensorB, targetValue, 0, _sensorB.Count - 1);
        }

        return -999;
    }

    //  This method sorts data using the selection sort algorithm and returns `true` if the sort is successful
    public bool RunSelectionSort (){
        return SortingAlgorithms.SelectionSort(_sensorB);
    }

    //  This method sorts data using the selection sort algorithm and returns `true` if the sort is successful
    public bool RunInsertionSort (){
        return SortingAlgorithms.InsertionSort(_sensorB);
    }
}