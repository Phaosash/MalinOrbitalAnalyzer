using MalinOrbitalAnalyzer.DataModels;
using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal static class DisplayRenderer {
    public static void DisplayListBoxData (ListBox listBox, IEnumerable<double> data){
        listBox.ItemsSource = null;
        listBox.ItemsSource = data.ToList();
    }

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

    private static List<int> GetHighlightedIndices (int targetIndex, int itemCount){
        var indices = new List<int> { targetIndex };

        for (int i = 1; i <= 2; i++){
            if (targetIndex - i >= 0) indices.Insert(0, targetIndex - i);
            if (targetIndex + i < itemCount) indices.Add(targetIndex + i);
        }

        indices.Insert(0, targetIndex);
        return indices;
    }
}