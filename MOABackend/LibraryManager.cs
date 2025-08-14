using Galileo6;

namespace MOABackend;

public class LibraryManager {
    //  Programming requirements 4.1
    private readonly LinkedList<double> _sensorA = new();
    private readonly LinkedList<double> _sensorB = new();

    //  Programming requirements 4.2
    public void LoadData (double average, double deviation){
        const int listSize = 400;
        ReadData readData = new();
        ClearSensors();

        for (int i = 0; i < listSize; i++){
            _sensorA.AddLast(readData.SensorA(average, deviation));
            _sensorB.AddLast(readData.SensorB(average, deviation));
        }
    }

    private void ClearSensors (){
        _sensorA.Clear();
        _sensorB.Clear();
    }

    public LinkedList<double> ReturnSensorA (){
        return _sensorA;
    }

    public LinkedList<double> ReturnSensorB (){
        return _sensorB;
    }

    //  Programming requirements 4.5
    public static int NumberOfNodes (LinkedList<double> nodeList){
        return nodeList.Count;
    }

    public bool RunSelectionSort (bool sensorA){      
        if (sensorA){
            return SortingAlgorithms.SelectionSort(_sensorA);
        } else {
             return SortingAlgorithms.SelectionSort(_sensorB);
        }
    }

    public bool RunInsertionSort (bool sensorA){
        if (sensorA){
            return SortingAlgorithms.InsertionSort(_sensorA);
        } else {
            return SortingAlgorithms.InsertionSort(_sensorB);
        }
    }
}