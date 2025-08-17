using MOABackend.DataManagers;

namespace MOABackend;

public class LibraryManager {
    //  Programming requirements 4.1
    private LinkedList<double> _sensorA = new();
    private LinkedList<double> _sensorB = new();

    public bool CreateSensorData (double average, double deviation){
        _sensorA.Clear();
        _sensorB.Clear();

        _sensorA = DataCreation.LoadData(average, deviation, true);
        _sensorB = DataCreation.LoadData(average, deviation, false);
        
        return true;
    }

    public LinkedList<double> ReturnSensorA (){
        return _sensorA;
    }

    public LinkedList<double> ReturnSensorB (){
        return _sensorB;
    }

    //  Programming requirements 4.5
    //  This redundent method returns an integer value of the number of items in the LinkedList,
    //  as I could just do the .count on the list when using it directly if needed.
    private static int NumberOfNodes (LinkedList<double> nodeList){
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

    public int RunRecursiveSearch (bool sensorA, int searchValue){  
        return 10;
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