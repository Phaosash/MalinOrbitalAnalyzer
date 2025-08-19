using ErrorLogging;

namespace MOABackend.DataManagers;

internal class BinarySearches {
    //  Programming requirements 4.9
    public static int BinarySearchIterative (LinkedList<double> list, double searchValue, int minimum, int maximum){
        try {
            ValidateInput(list, minimum, maximum);

            while (minimum <= maximum){
                int middle = minimum + (maximum - minimum) / 2;
                double middleValue = GetElementAt(list, middle);

                if (searchValue == middleValue){
                    return middle + 1;
                } else if (searchValue < middleValue){
                    maximum = middle - 1;
                } else {
                    minimum = middle + 1;
                }
            }

            return minimum;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("BinarySearchIterative Method in class library: ", ex);
            return -666;
        }
    }

    //  Programming requirements 4.10
    public static int BinarySearchRecursive (LinkedList<double> list, double searchValue, int minimum, int maximum){
        try {
            if (maximum >= minimum){
                int mid = minimum + (maximum - minimum) / 2;
                
                double midValue = 0;

                int count = 0;
                
                foreach (var value in list){
                    if (count == mid){
                        midValue = value;
                        break;
                    }
                    count++;
                }

                if (searchValue < midValue){
                    return BinarySearchRecursive(list, searchValue, minimum, mid - 1);
                } else if (searchValue == midValue){
                    return mid;
                } else {
                    return BinarySearchRecursive(list, searchValue, mid + 1, maximum);
                }
            }

            return minimum;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("BinarySearchRecursive Method in class library: ", ex);
            return -666;
        }
    }

    private static void ValidateInput (LinkedList<double> list, int minimum, int maximum){        
        if (list == null || list.Count == 0 || minimum < 0 || maximum >= list.Count || minimum > maximum){
            throw new ArgumentException("Invalid input parameters.");
        }
    }

    private static double GetElementAt (LinkedList<double> list, int index){
        LinkedListNode<double> currentNode = list.First!;
        
        for (int i = 0; i < index; i++){
            currentNode = currentNode.Next!;
        }

        return currentNode.Value;
    }
}