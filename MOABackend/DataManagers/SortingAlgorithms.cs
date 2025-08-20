using ErrorLoggingLibrary;

namespace MOABackend.DataManagers;

internal class SortingAlgorithms {
    //  Programming requirements 4.7
    //  This method sorts a LinkedList<double> by repeatedly selecting the smallest element from the
    //  unsorted portion and swapping it with the first unsorted element. It checks for errors, such
    //  as an empty list or a list with one element, and handles them by returning true immediately.
    //  If sorting occurs, it flags wasSorted as true. If an error is encountered, it logs the
    //  exception and returns false.
    public static bool SelectionSort (LinkedList<double> list){
        try {
            if (list == null || list.Count <= 1){
                return true;
            }        

            bool wasSorted = false;
            int max = list.Count;

            for (int i = 0; i < max - 1; i++){
                int min = i;

                for (int j = i + 1; j < max; j++){
                    if (list.ElementAt(j) < list.ElementAt(min)){
                        min = j;
                    }
                }

                var currentMin = list.Find(list.ElementAt(min));
                var currentI = list.Find(list.ElementAt(i));

                if (currentMin != null && currentI != null && currentMin != currentI){
                    (currentI.Value, currentMin.Value) = (currentMin.Value, currentI.Value);
                    wasSorted = true;
                }
            }

            return wasSorted;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the SelectionSort method in Sorting Algorithms", ex);
            return false;
        }
    }

    //  Programming requirements 4.8
    //  This method sorts a LinkedList<double> by iterating through each element and comparing
    //  it with the previous elements, inserting it into the correct position in the sorted
    //  portion of the list. If the list is null or contains one or fewer elements, it returns
    //  true immediately. During sorting, it ensures that each swap is performed between adjacent
    //  nodes, and handles any exceptions by logging the error and returning false in case of failure.
    public static bool InsertionSort(LinkedList<double> list){
        try {
            if (list == null || list.Count <= 1){
                return true;
            }

            int max = list.Count;

            for (int i = 0; i < max; i++){
                for (int j = i; j > 0; j--){
                    if (list.ElementAt(j - 1) > list.ElementAt(j)){
                        var current = list.Find(list.ElementAt(j));
                        var previous = list.Find(list.ElementAt(j - 1));

                        if (current != null && previous != null){
                            (previous.Value, current.Value) = (current.Value, previous.Value);
                        } else {
                            return false;
                        }
                    } else {
                        break;
                    }
                }
            }

            return true;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the InsertionSort method in Sorting Algorithms", ex);
            return false;
        }
    }
}