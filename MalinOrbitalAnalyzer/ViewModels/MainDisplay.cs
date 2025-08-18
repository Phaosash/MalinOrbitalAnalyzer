using MalinOrbitalAnalyzer.DataModels;
using MalinOrbitalAnalyzer.DisplayHelpers;
using MOABackend;

namespace MalinOrbitalAnalyzer.ViewModels;

internal class MainDisplay (OutputElements DisplayElements){
    private readonly OutputElements _displayElements = DisplayElements;
    private readonly LibraryManager _libraryManager = new();

    public void LoadApplicationData (double sigma, double mu){
        var success = _libraryManager.CreateSensorData(sigma, mu);
        
        if (success){
            ShowAllSensorData();
        } else {
            ErrorDialogService.ShowWarning("Something went wrong, unable to load the sensor data. Please try again");
        }
    }

    private void ShowAllSensorData (){
        var dataSetA = _libraryManager.GetSensorData(true) ?? new LinkedList<double>();
        var dataSetB = _libraryManager.GetSensorData(false) ?? new LinkedList<double>();

        DisplayRenderer.PopulateListView(_displayElements.CombinedListView!, dataSetA, dataSetB);
        DisplayRenderer.DisplayListBoxData(_displayElements.DisplayBoxA!, dataSetA);
        DisplayRenderer.DisplayListBoxData(_displayElements.DisplayBoxB!, dataSetB);
    }

    public void InitialiseSelectionSort (bool isDataSetA){
        long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunSort(isDataSetA), out bool success);
        DisplaySortTime(isDataSetA, timeTaken + "ms", isSelectionSort: true);

        if (success){
            DisplaySortedData(isDataSetA);
        } else {
            ErrorDialogService.ShowInfo($"Failed to complete the requested sort on {(isDataSetA ? "Sensor A" : "Sensor B")}");
        }
    }

    public void InitialiseInsertionSort (bool isDataSetA){
        long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunSort(isDataSetA), out bool success);
        DisplaySortTime(isDataSetA, timeTaken + "ms", isSelectionSort: false);

        if (success){
            DisplaySortedData(isDataSetA);
        } else {
            ErrorDialogService.ShowInfo($"Failed to complete the requested sort on {(isDataSetA ? "Sensor A" : "Sensor B")}");
        }
    }

    public void InitialiseIterativeSearch (bool isDataSetA, int inputedValue){
        if (!CanSearch(isDataSetA)){
            return;
        }

        const int CRITICAL_SEARCH_FAILURE_CODE = -666;
        const int DATA_NOT_SORTED_ERROR_CODE = -999;

        long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunSearch(isDataSetA, inputedValue), out int resultIndex);
        DisplaySearchTime(isDataSetA, timeTaken + " ticks", isRecursive: false);

        int testValue = resultIndex;

        if (resultIndex == CRITICAL_SEARCH_FAILURE_CODE){
            ErrorDialogService.ShowWarning("An unexpected error occurred while attempting to sort the data. Please try again or contact support if the issue persists.");
            return;
        }

        if (resultIndex == DATA_NOT_SORTED_ERROR_CODE){
            ErrorDialogService.ShowWarning($"Unable to complete the search on {(isDataSetA ? "Sensor A" : "Sensor B")}. Please sort the data first.");
        }

        if (isDataSetA){
            DisplayRenderer.HighlightIndices(_displayElements.DisplayBoxA!, resultIndex);
        } else {
            DisplayRenderer.HighlightIndices(_displayElements.DisplayBoxB!, resultIndex);
        }
    }

    public void InitialiseRecursiveSearch (bool isDataSetA, int inputedValue){
        if (!CanSearch(isDataSetA)){
            return;
        }

        const int CRITICAL_SEARCH_FAILURE_CODE = -666;
        const int DATA_NOT_SORTED_ERROR_CODE = -999;

        long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunSearch(isDataSetA, inputedValue), out int resultIndex);
        DisplaySearchTime(isDataSetA, timeTaken + " ticks", isRecursive: true);

        if (resultIndex == CRITICAL_SEARCH_FAILURE_CODE){
            ErrorDialogService.ShowWarning("An unexpected error occurred while attempting to sort the data. Please try again or contact support if the issue persists.");
            return;
        }

        if (resultIndex == DATA_NOT_SORTED_ERROR_CODE){
            ErrorDialogService.ShowWarning($"Unable to complete the search on {(isDataSetA ? "Sensor A" : "Sensor B")}. Please sort the data first.");
        } else {
            if (isDataSetA){
                DisplayRenderer.HighlightIndices(_displayElements.DisplayBoxA!, resultIndex);
            } else {
                DisplayRenderer.HighlightIndices(_displayElements.DisplayBoxB!, resultIndex);
            }
        }
    }

    private void DisplaySortedData (bool isDataSetA){
        var data = isDataSetA ? _libraryManager.GetSensorData(true) : _libraryManager.GetSensorData(false);
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