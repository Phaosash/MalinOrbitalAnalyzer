using MalinOrbitalAnalyzer.Logging;
using MalinOrbitalAnalyzer.Models;
using Microsoft.Extensions.Logging;
using MOABackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal class MainDisplay (OutputElements DisplayElements){
    private OutputElements _displayElements = DisplayElements;
    private readonly LibraryManager _libraryManager = new();

    //  Programming requirements 4.3
    public void ShowAllSensorData (){
        LinkedList<double> dataSetA = _libraryManager.ReturnSensorA() ?? new LinkedList<double>();
        LinkedList<double> dataSetB = _libraryManager.ReturnSensorB() ?? new LinkedList<double>();

        PopulateListView(dataSetA, dataSetB);
    }

    private void PopulateListView (LinkedList<double> setA, LinkedList<double> setB){
        try {
            _displayElements.CombinedListView!.Items.Clear();

            using var enumA = setA.GetEnumerator();
            using var enumB = setB.GetEnumerator();

            bool hasA = enumA.MoveNext();
            bool hasB = enumB.MoveNext();

            while (hasA || hasB){
                double? a = hasA ? enumA.Current : (double?)null;
                double? b = hasB ? enumB.Current : (double?)null;

                _displayElements.CombinedListView.Items.Add(new SensorData {
                    SensorA = a,
                    SensorB = b
                });

                hasA = hasA && enumA.MoveNext();
                hasB = hasB && enumB.MoveNext();
            }
        } catch (Exception ex){ 
            CentralizedErrorLogger.Instance.LogError("Problem populating the Combined ListView", ex);
            MessageBox.Show("Unable to update the display, something has gone wrong!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}