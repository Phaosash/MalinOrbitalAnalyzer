using MalinOrbitalAnalyzer.DataModels;
using MalinOrbitalAnalyzer.DisplayHelpers;
using MOABackend;

namespace MalinOrbitalAnalyzer.ViewModels;

internal class MainDisplay (OutputElements DisplayElements){
    private readonly OutputElements _displayElements = DisplayElements;
    private readonly LibraryManager _libraryManager = new();

    //  This method attempts to load sensor data by calling _libraryManager.CreateSensorData
    //  with the provided sigma and mu values. If the operation is successful (indicated by
    //  the success variable), it calls ShowAllSensorData() to display the data.
    //  If the data creation fails, it shows a warning message to the user, asking them to try again.
    //  If an exception occurs during this process, an error message is shown using ErrorDialogService.ShowError,
    //  providing details about the failure. This method ensures that any issues with loading the data are properly
    //  handled and communicated to the user.
    public void LoadApplicationData (double sigma, double mu){
        try {
            var success = _libraryManager.CreateSensorData(sigma, mu);
        
            if (success){
                ShowAllSensorData();
            } else {
                ErrorDialogService.ShowWarning("Something went wrong, unable to load the sensor data. Please try again");
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to load application data!", ex, "Error");
        }
    }

    //  This method retrieves two sets of sensor data (dataSetA and dataSetB) from _libraryManager,
    //  one for each sensor (true and false flags). If no data is found, it initializes the datasets
    //  as empty LinkedList<double> objects. The method then uses the DisplayRenderer to populate a
    //  ListView and two ListBox controls with the retrieved data. If any error occurs during this
    //  process, an error dialog is displayed, providing the exception details. This method ensures
    //  that the sensor data is correctly displayed in the UI while handling any issues gracefully.
    private void ShowAllSensorData (){
        try {
            var dataSetA = _libraryManager.GetSensorData(true) ?? new LinkedList<double>();
            var dataSetB = _libraryManager.GetSensorData(false) ?? new LinkedList<double>();

            DisplayRenderer.PopulateListView(_displayElements.CombinedListView!, dataSetA, dataSetB);
            DisplayRenderer.DisplayListBoxData(_displayElements.DisplayBoxA!, dataSetA);
            DisplayRenderer.DisplayListBoxData(_displayElements.DisplayBoxB!, dataSetB);
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to load Show all sensor data", ex, "Error");
        }
    }

    //  This method initiates a selection sort operation on either Sensor A or Sensor B, based on the
    //  isDataSetA flag. It first times the sorting operation using the ActionTimer.TimeAction method,
    //  which logs the time taken to execute the sorting function from _libraryManager.RunSort(isDataSetA).
    //  The elapsed time in milliseconds is displayed via DisplaySortTime. If the sorting operation is
    //  successful (success is true), the sorted data is shown using DisplaySortedData. If the sorting fails,
    //  an informational message is shown to the user, indicating which sensor dataset failed to sort. In case
    //  of an exception during the sorting process, an error dialog is displayed with the exception details,
    //  ensuring any issues are logged and communicated to the user.
    public void InitialiseSelectionSort (bool isDataSetA){
        try {
            long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunSort(isDataSetA), out bool success);
            DisplaySortTime(isDataSetA, timeTaken + "ms", isSelectionSort: true);

            if (success){
                DisplaySortedData(isDataSetA);
            } else {
                ErrorDialogService.ShowInfo($"Failed to complete the requested sort on {(isDataSetA ? "Sensor A" : "Sensor B")}");
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to initialise the selection sort.", ex, "Error");
        }
    }

    //  This method works similarly to InitialiseSelectionSort but performs an insertion sort on the selected dataset
    //  (either Sensor A or Sensor B, depending on the isDataSetA flag).
    //  It uses the ActionTimer.TimeAction method to measure the time taken for the sorting operation, which is executed
    //  via _libraryManager.RunSort(isDataSetA). The elapsed time in milliseconds is displayed using the DisplaySortTime
    //  method, where the isSelectionSort flag is set to `false` to indicate that this is an insertion sort. If the
    //  sorting operation succeeds (success is true), the sorted data is shown with DisplaySortedData.
    //  If the operation fails, an informational message is displayed to notify the user.
    //  If any exception occurs during the process, an error dialog is shown with the exception details,
    //  ensuring proper error handling and user communication.
    public void InitialiseInsertionSort (bool isDataSetA){
        try {
            long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunSort(isDataSetA), out bool success);
            DisplaySortTime(isDataSetA, timeTaken + "ms", isSelectionSort: false);

            if (success){
                DisplaySortedData(isDataSetA);
            } else {
                ErrorDialogService.ShowInfo($"Failed to complete the requested sort on {(isDataSetA ? "Sensor A" : "Sensor B")}");
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to initialise the insertion sort.", ex, "Error");
        }
    }

    //  This method performs an iterative search on either Sensor A or Sensor B, based on the isDataSetA flag,
    //  using a provided inputedValue. It checks if the search can proceed, then times the operation. If the
    //  search fails due to data issues, it displays appropriate warnings, such as needing to sort the data first
    //  or encountering a critical error.
    //  If the search is successful, it shows the result. The method also ensures that any exceptions are caught
    //  and reported to the user with an error dialog.
    public void InitialiseIterativeSearch (bool isDataSetA, int inputedValue){
        try {
            if (!CanSearch(isDataSetA)){
                return;
            }

            const int CRITICAL_SEARCH_FAILURE_CODE = -666;
            const int DATA_NOT_SORTED_ERROR_CODE = -999;

            long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunIterativeSearch(isDataSetA, inputedValue), out int resultIndex);
            DisplaySearchTime(isDataSetA, timeTaken + " ticks", isRecursive: false);

            if (resultIndex == CRITICAL_SEARCH_FAILURE_CODE){
                ErrorDialogService.ShowWarning("An unexpected error occurred while attempting to sort the data. Please try again or contact support if the issue persists.");
            } else if (resultIndex == DATA_NOT_SORTED_ERROR_CODE){
                ErrorDialogService.ShowWarning($"Unable to complete the search on {(isDataSetA ? "Sensor A" : "Sensor B")}. Please sort the data first.");
            } else {
                DisplayValues(isDataSetA, resultIndex);
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to initialise the iterative search.", ex, "Error");
        }
    }

    //  This method initiates a recursive search on either Sensor A or Sensor B using the provided inputedValue.
    //  It checks if the search can proceed before timing the search operation. If the search fails due to issues
    //  like unsorted data or a critical error, it displays appropriate warnings. If successful, it displays the
    //  search results. The elapsed time is displayed in ticks, and the search is marked as recursive.
    //  The method ensures any exceptions are handled and shown to the user with an error message.
    public void InitialiseRecursiveSearch (bool isDataSetA, int inputedValue){
        try {
            if (!CanSearch(isDataSetA)){
                return;
            }

            const int CRITICAL_SEARCH_FAILURE_CODE = -666;
            const int DATA_NOT_SORTED_ERROR_CODE = -999;

            long timeTaken = ActionTimer.TimeAction(() => _libraryManager.RunRecursiveSearch(isDataSetA, inputedValue), out int resultIndex);
            DisplaySearchTime(isDataSetA, timeTaken + " ticks", isRecursive: true);

            int test = resultIndex;

            if (resultIndex == CRITICAL_SEARCH_FAILURE_CODE){
                ErrorDialogService.ShowWarning("An unexpected error occurred while attempting to sort the data. Please try again or contact support if the issue persists.");
            } else if (resultIndex == DATA_NOT_SORTED_ERROR_CODE){
                ErrorDialogService.ShowWarning($"Unable to complete the search on {(isDataSetA ? "Sensor A" : "Sensor B")}. Please sort the data first.");
            } else {
                DisplayValues(isDataSetA, resultIndex);
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to initialise the recursive search.", ex, "Error");
        }
    }

    //  This method highlights a specific index in either Sensor A or Sensor B's ListBox, depending on the isSetA flag.
    //  It calls DisplayRenderer.HighlightIndices to update the display by highlighting the item at the given index.
    //  If an error occurs during this process, an error dialog is shown, notifying the user of the failure.
    private void DisplayValues (bool isSetA, int index){
        try {
            if (isSetA){
                DisplayRenderer.HighlightIndices(_displayElements.DisplayBoxA!, index);
            } else {
                DisplayRenderer.HighlightIndices(_displayElements.DisplayBoxB!, index);
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to initialise displaying the highlighted indeces.", ex, "Error");
        }
    }

    //  This method retrieves the sorted sensor data for either Sensor A or Sensor B, based on the isDataSetA flag.
    //  It then updates the corresponding ListBox with the sorted data using DisplayRenderer.DisplayListBoxData.
    //  If the data or the ListBox is null, no update occurs.
    //  Any errors encountered during this process are caught and displayed to the user through an error dialog.
    private void DisplaySortedData (bool isDataSetA){
        try {
            var data = isDataSetA ? _libraryManager.GetSensorData(true) : _libraryManager.GetSensorData(false);
            var listBox = isDataSetA ? _displayElements.DisplayBoxA : _displayElements.DisplayBoxB;

            if (data != null && listBox != null){
                DisplayRenderer.DisplayListBoxData(listBox, data);
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to update displaying the sorted data.", ex, "Error");
        }
    }

    //  This method updates the UI with the time it took to perform a sort, either using selection sort or insertion sort.
    //  It takes in the isDataSetA flag to determine which dataset the time corresponds to, and the isSelectionSort flag
    //  to distinguish between selection and insertion sorts. Based on these flags, it updates the appropriate text field
    //  (either SelectionTimeA, SelectionTimeB, InsertionTimeA, or InsertionTimeB) with the provided time.
    //  If any errors occur during this process, they are caught and an error dialog is shown to the user.
    private void DisplaySortTime (bool isDataSetA, string time, bool isSelectionSort){
        try {
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
        } catch (Exception ex){ 
            ErrorDialogService.ShowError("Failed to display the sort time.", ex, "Error");
        }
    }

    //  This method updates the UI with the time taken for a search operation, either iterative or recursive.
    //  It uses the isDataSetA flag to determine which dataset (Sensor A or Sensor B) the time is for, and the
    //  isRecursive flag to choose between displaying the time for a recursive or iterative search.
    //  Based on these flags, it updates the corresponding text field (either RecursiveTimeA, RecursiveTimeB,
    //  IterativeTimeA, or IterativeTimeB) with the given time.
    //  If an error occurs during this process, it is caught, and an error dialog is displayed to the user.
    private void DisplaySearchTime (bool isDataSetA, string time, bool isRecursive){
        try {
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
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to display the search time.", ex, "Error");
        }
    }

    //  This method checks if there is data available in the appropriate ListBox (either DisplayBoxA or DisplayBoxB)
    //  before performing a search. It retrieves the count of items in the selected ListBox and, if the count is zero,
    //  shows a warning message prompting the user to load data first. If an error occurs during this check, an error
    //  dialog is displayed. The method returns true if data is available and false otherwise.
    private bool CanSearch (bool isDataSetA){
        try {
            int count = isDataSetA ? _displayElements.DisplayBoxA?.Items.Count ?? 0 : _displayElements.DisplayBoxB?.Items.Count ?? 0;

            if (count == 0){
                ErrorDialogService.ShowWarning("Unable to run the search. Please load some data first.");
                return false;
            }

            return true;
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to determine if the search could be completed.", ex, "Error");
            return false;
        }
    }
}