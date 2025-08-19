using ErrorLogging;

namespace MOABackend.DataManagers;

internal class BinarySearches {
    //  Programming requirements 4.9
    //  This method performs an iterative binary search on a LinkedList<double>. It takes the list, searchValue, and a
    //  range defined by minimum and maximum indices as input. The method calculates the middle index, compares the
    //  middle value with the search value, and adjusts the search range accordingly. If a match is found, it returns
    //  the 1-based index. If the value is not found, it returns the adjusted position where the value should be inserted.
    //  Exceptions are logged, and in case of an error, it returns -666.
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
    //  This method implements a recursive binary search on a LinkedList<double>. It takes the list, searchValue, and the
    //  minimum and maximum indices for the search range. It calculates the middle index and compares the middle value with
    //  the search value. If the value is less than the middle value, it recursively searches the left half, and if it's
    //  greater, it searches the right half. When a match is found, it returns the index. If the value isn't found, it returns
    //  the position where the value should be inserted. Any errors during execution are logged, and the method returns -666 in
    //  case of an exception.
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

    //  This method checks the validity of the input parameters for binary search operations.
    //  It ensures that the list is not null or empty, and that the minimum and maximum indices
    //  are within the valid range of the list. Specifically, it checks that minimum is non-negative,
    //  maximum is within the bounds of the list, and minimum is not greater than maximum. If any of
    //  these conditions are violated, it throws an ArgumentException with the message
    //  Invalid input parameters.
    private static void ValidateInput (LinkedList<double> list, int minimum, int maximum){        
        if (list == null || list.Count == 0 || minimum < 0 || maximum >= list.Count || minimum > maximum){
            throw new ArgumentException("Invalid input parameters.");
        }
    }

    //  This method retrieves the value of an element at a specified index in a LinkedList<double>.
    //  It starts at the first node and iterates through the list, moving to the next node in each
    //  iteration, until it reaches the specified index. Once the desired index is reached, it returns
    //  the value of the node at that position. This method assumes the index is valid.
    private static double GetElementAt (LinkedList<double> list, int index){
        try {
            LinkedListNode<double> currentNode = list.First!;
        
            for (int i = 0; i < index; i++){
                currentNode = currentNode.Next!;
            }

            return currentNode.Value;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the LoadData method in the data creation class", ex);
            return 0.0;
        }
    }
}