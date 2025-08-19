using MalinOrbitalAnalyzer.DataModels;
using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal static class DisplayRenderer {
    //  This method populates a ListBox with data from an IEnumerable<double>. It first clears any existing
    //  data by setting the ItemsSource property to null.
    //  The .ToList() is needed to ensure that the display updates correctly, otherwise it wont update as intended.
    public static void DisplayListBoxData (ListBox listBox, IEnumerable<double> data){
        listBox.ItemsSource = null;
        listBox.ItemsSource = data.ToList();
    }

    //  This method fills a ListView with data from two LinkedList<double> collections, setA and setB.
    //  It first clears any existing items in the ListView. It then uses enumerators for both linked
    //  lists (setA and setB) to iterate through their elements. For each iteration, it checks if there
    //  is data in either list and creates a SensorData object, assigning values to SensorA and SensorB
    //  based on whether the enumerators can move to the next element. The created SensorData object is
    //  added to the ListView. If an error occurs during this process, an error dialog is displayed,
    //  showing a message and exception details.
    public static void PopulateListView (ListView listView, LinkedList<double> setA, LinkedList<double> setB){
        try {
            listView.Items.Clear();
            var enumerators = new[] { setA.GetEnumerator(), setB.GetEnumerator() };

            var hasA = enumerators[0].MoveNext();
            var hasB = enumerators[1].MoveNext();

            while (hasA || hasB){
                var a = hasA ? enumerators[0].Current : (double?)null;
                var b = hasB ? enumerators[1].Current : (double?)null;

                listView.Items.Add(new SensorData { SensorA = a, SensorB = b });

                hasA = hasA && enumerators[0].MoveNext();
                hasB = hasB && enumerators[1].MoveNext();
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Unable to update the display, something has gone wrong!", ex, "PopulateListView error");
        }
    }

    //  This method highlights specific items in a ListBox based on a given targetIndex. It first checks if the
    //  ListBox is null or if the targetIndex is out of bounds. If the checks pass, it clears any existing selections
    //  from the ListBox and calculates the indices of the items to highlight using the GetHighlightedIndices method.
    //  The method then adds the items at those indices to the SelectedItems collection of the ListBox. Finally, it
    //  ensures that the ListBox scrolls to the targetIndex item. If any errors occur, an error dialog is shown,
    //  providing details about the issue.
    public static void HighlightIndices (ListBox listBox, int targetIndex){
        try {
            if (listBox == null || targetIndex < 0 || targetIndex >= listBox.Items.Count){
                return;
            }

            listBox.SelectedItems.Clear();
            var selectedIndices = GetHighlightedIndices(targetIndex, listBox.Items.Count);
            
            foreach (var index in selectedIndices){
                listBox.SelectedItems.Add(listBox.Items[index]);
            }

            listBox.ScrollIntoView(listBox.Items[targetIndex]);
        } catch (Exception ex){
            ErrorDialogService.ShowError("Unable to update the display, something has gone wrong!", ex, "HighlightIndices error");
        }
    }

    //  This method calculates the indices of items to highlight around a given targetIndex in a ListBox,
    //  ensuring that the indices are within valid bounds. It starts by adding the targetIndex to the list
    //  of indices. Then, for i = 1 and i = 2, it checks if the indices targetIndex - i and targetIndex + i
    //  are within the valid range (i.e., between 0 and itemCount - 1). It adds these indices to the list
    //  accordingly, both before and after the targetIndex. The method then returns the list of indices, including
    //  the targetIndex again at the start. If an exception occurs during the process, an error dialog is shown,
    //  and an empty list is returned (although return [] should be corrected to return new List<int>(); to avoid a
    //  compilation error).
    private static List<int> GetHighlightedIndices (int targetIndex, int itemCount){
        try {
            var indices = new List<int> { targetIndex };

            for (int i = 1; i <= 2; i++){
                if (targetIndex - i >= 0) indices.Insert(0, targetIndex - i);
                if (targetIndex + i < itemCount) indices.Add(targetIndex + i);
            }

            indices.Insert(0, targetIndex);
            return indices;
        } catch (Exception ex){
            ErrorDialogService.ShowError("Unable to update the display, something has gone wrong!", ex, "HighlightIndices error");
            return [];
        }
    }
}