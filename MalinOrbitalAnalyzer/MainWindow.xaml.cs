using MalinOrbitalAnalyzer.DataModels;
using MalinOrbitalAnalyzer.DisplayHelpers;
using MOABackend;
using System.Windows;
using System.Windows.Input;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {      
    private readonly LibraryManager _libraryManager;

    public MainWindow (){
        InitializeComponent();
        _libraryManager = new LibraryManager();
    }

    //  Programming requirements 4.14
    //  This method prevents non-numeric text from being entered by marking the input as handled if it fails a numeric check.
    //  It relies on the TextIsNumeric method to validate the input.
    private void MaskNumericInput (object sender, TextCompositionEventArgs e){
        e.Handled = !TextIsNumeric(e.Text);
    }

    //  Programming requirements 4.14
    //  This method blocks pasted content if it is not numeric by canceling the paste command unless the input passes a numeric check.
    //  It ensures only valid numeric strings are allowed during paste operations.
    private void MaskNumericPaste (object sender, DataObjectPastingEventArgs e){
        if (e.DataObject.GetDataPresent(typeof(string))){
            string input = (string)e.DataObject.GetData(typeof(string));

            if (TextIsNumeric(input)){
                e.CancelCommand();
            }
        } else {
            e.CancelCommand();
        }
    }

    //  Programming requirements 4.14
    //  This method checks whether a string represents a valid numeric input, allowing an optional leading minus sign and ensuring all
    //  remaining characters are digits or control characters. It returns `false` for null or empty strings.
    private static bool TextIsNumeric (string input){
        if (string.IsNullOrEmpty(input)){
            return false;
        }

        if (input[0] == '-'){
            input = input[1..];
        }
        
        return input.All(c => Char.IsDigit(c) || Char.IsControl(c));
    }

    //  This method generates sensor data using input values, then updates the UI components with the data if both sensor datasets are populated.
    //  If data generation fails or results are incomplete, it shows a warning or error dialog accordingly.
    private void LoadSensorData_Click (object sender, RoutedEventArgs e){
        try {
            _libraryManager.CreateSensorData(SigmaValueInput.Value ?? 0.0, MuValueInput.Value ?? 0.0);

            if (_libraryManager.ReturnSensorACount() > 0 && _libraryManager.ReturnSensorBCount() > 0){
                DisplayRenderer.DisplayListBoxData(DataDisplayA, _libraryManager.GetSensorDataA());
                DisplayRenderer.DisplayListBoxData(DataDisplayB, _libraryManager.GetSensorDataB());
                DisplayRenderer.PopulateListView(CombinedSensorListViewDisplay, _libraryManager.GetSensorDataA(), _libraryManager.GetSensorDataB());
            } else {
                ErrorDialogService.ShowWarning("Something went wrong, unable to load the sensor data. Please try again");
            }
        } catch (Exception ex){
            ErrorDialogService.ShowError("Failed to load application data!", ex, "Error");
        }
    }

    //  Programming requirements 4.11
    //  This method initiates an iterative search on sensor A’s data if it’s loaded, validating user input and timing the search execution.
    //  It then displays the search time, highlights the found index, or shows appropriate warnings or errors based on the search result or any exceptions.
    private void IterativeSearchA_Click (object sender, RoutedEventArgs e){
        if (DataDisplayA.Items.Count > 0){
            try {
            if (int.TryParse(DataSetTargetInptA.Text, out int value)){
                long timeTaken = ActionTimer.TimeAction(() => _libraryManager.PerformIterativeSearchA(value), out int resultIndex);

                DisplayIterativeTimeA.Text = timeTaken.ToString() + " Ticks";

                if (resultIndex == SearchFailureCodes.CRITICAL_SEARCH_FAILURE_CODE){
                    ErrorDialogService.ShowWarning("An unexpected error occurred while attempting to sort the data. Please try again or contact support if the issue persists.");
                } else if (resultIndex == SearchFailureCodes.DATA_NOT_SORTED_ERROR_CODE){
                    ErrorDialogService.ShowWarning($"Unable to complete the search on sensor A. Please sort the data first.");
                } else {
                    DisplayRenderer.HighlightIndices(DataDisplayA, resultIndex);
                }
            } else {
                MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            } catch (Exception ex){
                ErrorDialogService.ShowError("Failed to initialise the iterative search for Sensor A.", ex, "Error");
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a search.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    //  This method performs an iterative search on sensor B’s data after validating input and ensuring data is loaded, timing the search and updating the UI with the
    //  result or relevant warnings. It handles errors by displaying appropriate messages if the search fails or exceptions occur.
    private void IterativeSearchB_Click (object sender, RoutedEventArgs e){
        if (DataDisplayB.Items.Count > 0){
            try {
                if (int.TryParse(DataSetTargetInptB.Text, out int value)){
                    long timeTaken = ActionTimer.TimeAction(() => _libraryManager.PerformIterativeSearchB(value), out int resultIndex);

                    DisplayIterativeTimeB.Text = timeTaken.ToString() + " Ticks";

                    if (resultIndex == SearchFailureCodes.CRITICAL_SEARCH_FAILURE_CODE){
                        ErrorDialogService.ShowWarning("An unexpected error occurred while attempting to sort the data. Please try again or contact support if the issue persists.");
                    } else if (resultIndex == SearchFailureCodes.DATA_NOT_SORTED_ERROR_CODE){
                        ErrorDialogService.ShowWarning($"Unable to complete the search on sensor B. Please sort the data first.");
                    } else {
                        DisplayRenderer.HighlightIndices(DataDisplayB, resultIndex);
                    }
                } else {
                    MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            } catch (Exception ex){
                ErrorDialogService.ShowError("Failed to initialise the iterative search for Sensor B.", ex, "Error");
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a search.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    //  This method validates user input and performs a recursive search on sensor A’s loaded data, timing the operation and updating the UI with the result or appropriate warnings.
    //  It also handles errors by displaying messages if the search fails or exceptions occur.
    private void RecursiveSearchA_Click (object sender, RoutedEventArgs e){
        if (DataDisplayA.Items.Count > 0){
            try {
                if (int.TryParse(DataSetTargetInptA.Text, out int value)){
                    long timeTaken = ActionTimer.TimeAction(() => _libraryManager.PerformRecursiveSearchA(value), out int resultIndex);

                    DisplayRecursiveTimeA.Text = timeTaken.ToString() + " Ticks";

                    if (resultIndex == SearchFailureCodes.CRITICAL_SEARCH_FAILURE_CODE){
                        ErrorDialogService.ShowWarning("An unexpected error occurred while attempting to sort the data. Please try again or contact support if the issue persists.");
                    } else if (resultIndex == SearchFailureCodes.DATA_NOT_SORTED_ERROR_CODE){
                        ErrorDialogService.ShowWarning($"Unable to complete the search on sensor A. Please sort the data first.");
                    } else {
                        DisplayRenderer.HighlightIndices(DataDisplayA, resultIndex);
                    }
                } else {
                    MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            } catch (Exception ex){
                ErrorDialogService.ShowError("Failed to initialise the recursive search for Sensor A.", ex, "Error");
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a search.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    //  This method checks for loaded sensor B data and validates the input before performing a timed recursive search, updating the UI with the results or showing warnings
    //  if the search fails or data isn’t sorted. It also handles exceptions by displaying an error message.
    private void RecursiveSearchB_Click (object sender, RoutedEventArgs e){
        if (DataDisplayB.Items.Count > 0){
            if (int.TryParse(DataSetTargetInptB.Text, out int value)){
                try {
                    long timeTaken = ActionTimer.TimeAction(() => _libraryManager.PerformRecursiveSearchB(value), out int resultIndex);

                    DisplayRecursiveTimeA.Text = timeTaken.ToString() + " Ticks";

                    if (resultIndex == SearchFailureCodes.CRITICAL_SEARCH_FAILURE_CODE){
                        ErrorDialogService.ShowWarning("An unexpected error occurred while attempting to sort the data. Please try again or contact support if the issue persists.");
                    } else if (resultIndex == SearchFailureCodes.DATA_NOT_SORTED_ERROR_CODE){
                        ErrorDialogService.ShowWarning($"Unable to complete the search on sensor B. Please sort the data first.");
                    } else {
                        DisplayRenderer.HighlightIndices(DataDisplayB, resultIndex);
                    }
                } catch (Exception ex){
                    ErrorDialogService.ShowError("Failed to initialise the recursive search for Sensor B.", ex, "Error");
                }
            } else {
                MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a search.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    //  This method performs a selection sort on sensor A’s loaded data, timing the operation and updating the display with the sorted results.
    //  If no data is loaded or an error occurs, it shows an appropriate warning or error message.
    private void SelectionSortA_Click (object sender, RoutedEventArgs e){
        if (DataDisplayA.Items.Count > 0){
            try {
                long timeTaken = ActionTimer.TimeAction(() => _libraryManager.PerformSelectionSortA());

                DisplaySelecttionTimeA.Text = timeTaken.ToString() + "ms";
                DisplayRenderer.DisplayListBoxData(DataDisplayA, _libraryManager.GetSensorDataA());
            } catch (Exception ex){
                ErrorDialogService.ShowError("Failed to initialise the selection sort for Sensor A.", ex, "Error");
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a sort.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    //  This method times and performs a selection sort on sensor B’s loaded data, then updates the display with the sorted list.
    //  It shows warnings if no data is loaded and error messages if the sorting process fails.
    private void SelectionSortB_Click (object sender, RoutedEventArgs e){
        if (DataDisplayB.Items.Count > 0){
            try {
                long timeTaken = ActionTimer.TimeAction(() => _libraryManager.PerformSelectionSortB());

                DisplaySelectionTimeB.Text = timeTaken.ToString() + "ms";

                DisplayRenderer.DisplayListBoxData(DataDisplayB, _libraryManager.GetSensorDataB());
            } catch (Exception ex){
                ErrorDialogService.ShowError("Failed to initialise the selection sort for Sensor B.", ex, "Error");
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a sort.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    //  This method times and executes an insertion sort on sensor A’s loaded data, updating the UI with the sorted results.
    //  It displays warnings if no data is loaded and shows error messages if the sorting process encounters an issue.
    private void InsertionSortA_Click (object sender, RoutedEventArgs e){
        if (DataDisplayA.Items.Count > 0){
            try {
                long timeTaken = ActionTimer.TimeAction(() => _libraryManager.PerformInsertionSortA());

                DisplayInsertionTimeA.Text = timeTaken.ToString() + "ms";

                DisplayRenderer.DisplayListBoxData(DataDisplayA, _libraryManager.GetSensorDataA());
            } catch (Exception ex){
                ErrorDialogService.ShowError("Failed to initialise the insertion sort for Sensor A.", ex, "Error");
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a sort.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    //  This method times and performs an insertion sort on sensor B’s loaded data, then updates the UI with the sorted results.
    //  It shows warnings if no data is loaded and error messages if sorting fails.
    private void InsertionSortB_Click (object sender, RoutedEventArgs e){
        if (DataDisplayB.Items.Count > 0){
            try {
                long timeTaken = ActionTimer.TimeAction(() => _libraryManager.PerformInsertionSortB());

                DisplayInsertionTimeB.Text = timeTaken.ToString() + "ms";

                DisplayRenderer.DisplayListBoxData(DataDisplayB, _libraryManager.GetSensorDataB());
            } catch (Exception ex){
                ErrorDialogService.ShowError("Failed to initialise the insertion sort for Sensor B.", ex, "Error");
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a sort.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}