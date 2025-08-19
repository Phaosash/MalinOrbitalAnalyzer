using ErrorLogging;

namespace MOABackend.DataManagers;

internal class DataValidator {
    //  This method checks if a LinkedList<double> is sorted in ascending order.
    //  It iterates through the list, comparing each element with the next one.
    //  If any pair is found where the current element is greater than the next
    //  (with a small epsilon tolerance for floating-point precision), it returns
    //  false. If the list is null or contains fewer than two elements, it assumes
    //  the list is sorted and returns true. Any exceptions encountered during
    //  execution are logged, and the method returns false in case of an error.
    public static bool IsSorted (LinkedList<double> nodeList){        
        try {
            if (nodeList == null || nodeList.Count < 2){
                return true;
            }

            LinkedListNode<double> current = nodeList.First!;
            const double epsilon = 1e-10;

            while (current.Next != null){
                if (current.Value > current.Next.Value && Math.Abs(current.Value - current.Next.Value) > epsilon){
                    return false;
                }

                current = current.Next;
            }

            return true;
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the IsSorted method in the data validator class", ex);
            return false;
        }
    }
}