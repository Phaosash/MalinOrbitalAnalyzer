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
            return -999;
        }
    }

    //  Programming requirements 4.10
    public static int BinarySearchRecursive (LinkedList<double> list, double searchValue, int minimum, int maximum){
        try {
            //if (minimum > maximum || list == null || list.Count == 0){
            //    throw new ArgumentException("Invalid input parameters.");
            //}

            //LoggingHandler.Instance.LogInformation($"Recursive call: minimum={minimum}, maximum={maximum}, list count={list.Count}");

            //ValidateInput(list, minimum, maximum);

            //if (minimum <= maximum){
            //    int middle = minimum + (maximum - minimum) / 2;

            //    double middleValue = GetElementAt(list, middle);

            //    LoggingHandler.Instance.LogInformation($"middle={middle}, middleValue={middleValue}");

            //    if (searchValue == middleValue){
            //        LoggingHandler.Instance.LogInformation($"Value found at index {middle}");
            //        return middle;
            //    }
            //    else if (searchValue < middleValue){
            //        LoggingHandler.Instance.LogInformation($"Searching left side: minimum={minimum}, middle={middle-1}");
            //        return BinarySearchRecursive(list, searchValue, minimum, middle - 1);
            //    } else {
            //        LoggingHandler.Instance.LogInformation($"Searching right side: middle={middle+1}, maximum={maximum}");
            //        return BinarySearchRecursive(list, searchValue, middle + 1, maximum);
            //    }
            //}
    
            //LoggingHandler.Instance.LogInformation($"Returning minimum={minimum} as nearest neighbor");
            //return minimum;
            if (minimum > maximum){
                return -1;
            }

            int mid = minimum + (maximum - minimum) / 2;

            LinkedListNode<double> currentNode = list.First;

            for (int i = 0; i < mid; i++)
            {
                currentNode = currentNode.Next;
            }

            if (currentNode?.Value == searchValue)
            {
                return mid;
            }
            else if (currentNode?.Value > searchValue)
            {
                return BinarySearchRecursive(list, searchValue, minimum, mid - 1);
            }
            else
            {
                return BinarySearchRecursive(list, searchValue, mid + 1, maximum);
            }
        } catch (Exception ex){
                LoggingHandler.Instance.LogError("BinarySearchRecursive Method in class library: ", ex);
                return -999;
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