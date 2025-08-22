using MalinOrbitalAnalyzer.DataModels;
using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal static class DisplayRenderer {
    //  This method refreshes the contents of a ListBox by first clearing its item source and then setting it to a list created from the given data collection.
    //  This ensures the ListBox displays the updated data.
    public static void DisplayListBoxData (ListBox listBox, IEnumerable<double> data){
        listBox.ItemsSource = null;
        listBox.ItemsSource = data.ToList();
    }

    //  This method clears the current items in a ListView and populates it with paired data from two linked lists, aligning their elements side by side even
    //  if their lengths differ. If an error occurs during this process, it displays an error dialog with details about the exception.
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

    //  This method selects and highlights specific items in a ListBox based on a target index, ensuring the item is visible by scrolling to it. If an error occurs,
    //  it shows an error dialog with relevant details.
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

    //  This method returns a list of indices centered around a target index, including up to two items before and after it, while ensuring
    //  they stay within valid bounds. If an exception occurs, it logs the error and returns an empty list.
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