using MOABackend.DataHandlers;

namespace MOABackend;

public class SensorManagerA {
    //  Programming requirements 4.1
    private LinkedList<double> _sensorA = new();

    //  This method returns a LinkedList of double values representing the data stored in _sensorA.
    public LinkedList<double> GetSensorA (){
        return _sensorA;
    }

    //  This method clears the existing sensor data and reloads it using the `DataLoader.LoadData` method
    //  with the provided `average` and `deviation` values, ensuring new data is generated.
    public void SetSensorA (double average, double deviation){
        _sensorA.Clear();
        _sensorA = DataLoader.LoadData(_sensorA, average, deviation, true);
    }

    //  This method checks if the data is sorted; if so, it performs an iterative binary search for targetValue
    //  and returns the index. If not sorted, it returns -999
    public int RunIterativeSearch (int targetValue){
        bool isSorted = DataValidator.IsSorted(_sensorA);

        if (isSorted){
            return BinarySearches.BinarySearchIterative(_sensorA, targetValue, 0, _sensorA.Count - 1);
        }

        return -999;
    }

    //  This method sorts data using the selection sort algorithm and returns `true` if the sort is successful
    public bool RunSelectionSort (){
        return SortingAlgorithms.SelectionSort(_sensorA);
    }

    //  This method sorts data using the selection sort algorithm and returns `true` if the sort is successful
    public bool RunInsertionSort (){
        return SortingAlgorithms.InsertionSort(_sensorA);
    }

    //  Programming requirements 4.5
    //  This method returns the total number of elements (nodes) in the given LinkedList<double>
    private static int NumberOfNodes(LinkedList<double> nodeList){
        return nodeList.Count;
    }
}