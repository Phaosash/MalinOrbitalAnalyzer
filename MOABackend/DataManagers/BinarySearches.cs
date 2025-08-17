namespace MOABackend.DataManagers;

internal class BinarySearches {
    //  Programming requirements 4.9
    public static int BinarySearchIterative (LinkedList<double> list, double searchValue, int minimum, int maximum){
        try {
            if (list == null || list.Count == 0 || minimum < 0 || maximum >= list.Count || minimum > maximum){
                throw new ArgumentException("Invalid input parameters.");
            }

            while (minimum <= maximum - 1){
                int middle = (minimum + maximum) / 2;
                double middleValue = list.ElementAt(middle);

                if (searchValue == middleValue){
                    return middle + 1;
                }
                else if (searchValue < middleValue){
                    maximum = middle - 1;
                } else {
                    minimum = middle + 1;
                }
            }

            return minimum;
        } catch {
            return -999;
        }
    }

    //  Programming requirements 4.10
    public static int BinarySearchRecursive (LinkedList<double> list, double searchValue, int minimum, int maximum){
        if (minimum <= maximum - 1){
            int middle = (minimum + maximum) / 2;
            double middleValue = list.ElementAt(middle);

            if (searchValue == middleValue){
                return middle;
            }
            else if (searchValue < middleValue){
                return BinarySearchRecursive(list, searchValue, minimum, middle - 1);
            } else {
                return BinarySearchRecursive(list, searchValue, middle + 1, maximum);
            }
        }

        return minimum;
    }
}