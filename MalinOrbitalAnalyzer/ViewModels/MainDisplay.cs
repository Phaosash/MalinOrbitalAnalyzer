using MalinOrbitalAnalyzer.DataModels;
using MalinOrbitalAnalyzer.DisplayHelpers;
using MOABackend;
using System.Windows;

namespace MalinOrbitalAnalyzer.ViewModels;

internal class MainDisplay (OutputElements DisplayElements){
    private readonly OutputElements _displayElements = DisplayElements;
    private readonly LibraryManager _libraryManager = new();

    public void LoadApplicationData (double sigma, double mu){
        _libraryManager.CreateSensorData(sigma, mu);
        ShowAllSensorData();
    }

    private void ShowAllSensorData (){
        var dataSetA = _libraryManager.ReturnSensorA() ?? new LinkedList<double>();
        var dataSetB = _libraryManager.ReturnSensorB() ?? new LinkedList<double>();

        DisplayRenderer.PopulateListView(_displayElements.CombinedListView!, dataSetA, dataSetB);
        DisplayRenderer.DisplayListBoxData(_displayElements.DisplayBoxA!, dataSetA);
        DisplayRenderer.DisplayListBoxData(_displayElements.DisplayBoxB!, dataSetB);
    }

    public void InitialiseSelectionSort (bool isDataSetA){
        long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunSelectionSort(isDataSetA), out bool success);
        DisplaySortTime(isDataSetA, timeTaken + "ms", isSelectionSort: true);

        if (success){
            DisplaySortedData(isDataSetA);
        } else {
            ErrorDialogService.ShowInfo($"Failed to complete the requested sort on {(isDataSetA ? "Sensor A" : "Sensor B")}");
        }
    }

    public void InitialiseInsertionSort (bool isDataSetA){
        long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunInsertionSort(isDataSetA), out bool success);
        DisplaySortTime(isDataSetA, timeTaken + "ms", isSelectionSort: false);

        if (success){
            DisplaySortedData(isDataSetA);
        } else {
            ErrorDialogService.ShowInfo($"Failed to complete the requested sort on {(isDataSetA ? "Sensor A" : "Sensor B")}");
        }
    }

    public void InitialiseIterativeSearch (bool isDataSetA, int inputedValue){
        if (!CanSearch(isDataSetA)) return;

        long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunIterativeSearch(isDataSetA, inputedValue), out int resultIndex);
        DisplaySearchTime(isDataSetA, timeTaken + " ticks", isRecursive: false);

        if (resultIndex == -999){
            ErrorDialogService.ShowWarning($"Unable to complete the search on {(isDataSetA ? "Sensor A" : "Sensor B")}. Please sort the data first.");
        } else {
            //  TODO: Highlight cells in the ListBox
            MessageBox.Show("Value found at: " + resultIndex);
        }
    }

    public void InitialiseRecursiveSearch (bool isDataSetA, int inputedValue){
        if (!CanSearch(isDataSetA)) return;

        long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunRecursiveSearch(isDataSetA, inputedValue), out int resultIndex);
        DisplaySearchTime(isDataSetA, timeTaken + " ticks", isRecursive: true);

        if (resultIndex == -999){
            ErrorDialogService.ShowWarning($"Unable to complete the search on {(isDataSetA ? "Sensor A" : "Sensor B")}. Please sort the data first.");
        } else {
            //  TODO: Highlight cells in the ListBox
            MessageBox.Show("Value found at: " + resultIndex);
        }
    }

    private void DisplaySortedData (bool isDataSetA){
        var data = isDataSetA ? _libraryManager.ReturnSensorA() : _libraryManager.ReturnSensorB();
        var listBox = isDataSetA ? _displayElements.DisplayBoxA : _displayElements.DisplayBoxB;

        if (data != null && listBox != null){
            DisplayRenderer.DisplayListBoxData(listBox, data);
        }
    }

    private void DisplaySortTime (bool isDataSetA, string time, bool isSelectionSort){
        if (isSelectionSort){
            if (isDataSetA){
                _displayElements.SelectionTimeA!.Text = time;
            } else {
                _displayElements.SelectionTimeB!.Text = time;
            }
        } else {
            if (isDataSetA){
                _displayElements.InsertionTimeA!.Text = time;
            } else { 
                _displayElements.InsertionTimeB!.Text = time;
            }
        }
    }

    private void DisplaySearchTime (bool isDataSetA, string time, bool isRecursive){
        if (isRecursive){
            if (isDataSetA){
                _displayElements.RecursiveTimeA!.Text = time;
            } else {
                _displayElements.RecursiveTimeB!.Text = time;
            }
        } else {
            if (isDataSetA){
                _displayElements.IterativeTimeA!.Text = time;
            } else { _displayElements.IterativeTimeB!.Text = time;
            }
        }
    }

    private bool CanSearch (bool isDataSetA){
        int count = isDataSetA ? _displayElements.DisplayBoxA?.Items.Count ?? 0 : _displayElements.DisplayBoxB?.Items.Count ?? 0;

        if (count == 0){
            ErrorDialogService.ShowWarning("Unable to run the search. Please load some data first.");
            return false;
        }

        return true;
    }
}