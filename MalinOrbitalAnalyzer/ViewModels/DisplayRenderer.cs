using MalinOrbitalAnalyzer.DisplayHelpers;
using MalinOrbitalAnalyzer.Models;
using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.HelperClasses;

internal static class DisplayRenderer {
    public static void DisplayListBoxData (ListBox listBox, IEnumerable<double> data){
        listBox.ItemsSource = null;
        listBox.ItemsSource = data.ToList();
    }

    public static void PopulateListView (ListView listView, LinkedList<double> setA, LinkedList<double> setB){
        try {
            listView.ItemsSource = null;
            listView.Items.Clear();

            using var enumA = setA.GetEnumerator();
            using var enumB = setB.GetEnumerator();

            bool hasA = enumA.MoveNext();
            bool hasB = enumB.MoveNext();

            while (hasA || hasB){
                double? a = hasA ? enumA.Current : null;
                double? b = hasB ? enumB.Current : null;

                listView.Items.Add(new SensorData { SensorA = a, SensorB = b });

                hasA = hasA && enumA.MoveNext();
                hasB = hasB && enumB.MoveNext();
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Unable to update the display, something has gone wrong!", ex, "PopulateListView error");
        }
    }
}