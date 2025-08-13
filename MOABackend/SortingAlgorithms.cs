namespace MOABackend;

internal class SortingAlgorithms {
    //  Programming requirements 4.7
    public static bool SelectionSort (LinkedList<double> list){
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
    }

    //  Programming requirements 4.8
    public static bool InsertionSort(LinkedList<double> list){
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
    }
}