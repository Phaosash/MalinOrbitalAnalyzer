namespace MOABackend.DataManagers;

internal class DataValidator {
    public static bool IsSorted (LinkedList<double> nodeList){        
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
    }
}