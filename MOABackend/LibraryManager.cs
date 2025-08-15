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

    public int RunIterativeSearch (bool sensorA, int searchValue){       
        if (sensorA){
            bool isSorted = IsSorted(_sensorA);

            if (isSorted){
                return BinarySearches.BinarySearchIterative(_sensorA, searchValue, 0, _sensorA.Count - 1);
            }

            return -999;
        } else {
            bool isSorted = IsSorted(_sensorB);

            if (isSorted){
                return BinarySearches.BinarySearchIterative(_sensorB, searchValue, 0, _sensorB.Count - 1);
            }

            return -999;
        }
    }

    private static bool IsSorted (LinkedList<double> nodeList){
        if (nodeList == null){
            return false;
        }

        if (nodeList.Count < 2){
            return true;
        }

        LinkedListNode<double> current = nodeList.First!;

        while (current.Next != null){
            if (current.Value > current.Next.Value){
                return false;
            }          

            current = current.Next;
        }

        return true;
    }
}