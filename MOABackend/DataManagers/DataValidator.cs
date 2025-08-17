namespace MOABackend.DataManagers;

internal class DataValidator {
    public static bool IsSorted (LinkedList<double> nodeList){
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