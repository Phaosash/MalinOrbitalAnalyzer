using MalinOrbitalAnalyzer.DataModels;
using MalinOrbitalAnalyzer.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {      
    private readonly MainDisplay _mainDisplay;
    
    //  This constructor initializes the main window by calling InitializeComponent(),
    //  which sets up the UI elements defined in XAML. After that, it creates an instance
    //  of MainDisplay and assigns it to the _mainDisplay field.
    //  The MainDisplay is initialized with an output element, which is created by the
    //  CreateOutputElement() method. This setup is part of the window's initialization process,
    //  setting up the core display functionality.
    public MainWindow (){
        InitializeComponent();
        _mainDisplay = new MainDisplay(CreateOutputElement());
    }

    //  Programming requirements 4.14
    //  This method handles the TextCompositionEventArgs when text is being entered into a control,
    //  such as a TextBox. It checks if the input (e.Text) is numeric by calling the TextIsNumeric method.
    //  If the input is not numeric, it sets e.Handled = true, which prevents the non-numeric character
    //  from being entered. This ensures that only numeric values are allowed in the input field.
    private void MaskNumericInput (object sender, TextCompositionEventArgs e){
        e.Handled = !TextIsNumeric(e.Text);
    }

    //  Programming requirements 4.14
    //  This method handles paste operations for a control, ensuring that only numeric input can be pasted.
    //  It first checks if the pasted data is of type string by calling e.DataObject.GetDataPresent(typeof(string)).
    //  If the data is a string, it retrieves the content and checks if it is numeric using the TextIsNumeric method.
    //  If the input is numeric, it cancels the paste operation with e.CancelCommand(), preventing non-numeric content
    //  from being pasted. If the data is not a string, the paste operation is also canceled.
    //  This ensures that only numeric values can be pasted into the control.
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
    //  This method checks if the given input string is numeric.
    //  It first ensures the input is not null or empty.
    //  If the string starts with a minus sign (-), indicating a negative number, it removes the sign for further checks.
    //  Then, it verifies that all characters in the string are either digits or control characters (like backspace or tab)
    //  using Char.IsDigit and Char.IsControl. If all characters meet this condition, the method returns true, indicating
    //  the input is numeric. Otherwise, it returns `false`. This method effectively handles both positive and negative
    //  numeric inputs.
    private static bool TextIsNumeric (string input){
        if (string.IsNullOrEmpty(input)){
            return false;
        }

        if (input[0] == '-'){
            input = input[1..];
        }
        
        return input.All(c => Char.IsDigit(c) || Char.IsControl(c));
    }

    //  This method is an event handler for a button click.
    //  When the button is clicked, it triggers the method to load sensor data by calling the LoadApplicationData method of the
    //  _mainDisplay object. It passes the values of SigmaValueInput and MuValueInput (which are presumably NumericUpDown or
    //  similar controls) as parameters. If either value is null, it defaults to 0.0. This method is used to load and display
    //  the sensor data based on the provided sigma and mu values.
    private void LoadSensorData_Click (object sender, RoutedEventArgs e){
        _mainDisplay.LoadApplicationData(SigmaValueInput.Value ?? 0.0, MuValueInput.Value ?? 0.0);
    }

    //  Programming requirements 4.11
    //  This method is an event handler triggered when the button for performing an iterative search on Sensor A is clicked.
    //  It first checks if there is data available in DataDisplayA by verifying the item count. If data exists, it tries
    //  to parse the value entered in the DataSetTargetInptA text box as an integer. If the input is a valid whole number,
    //  it calls the InitialiseIterativeSearch method of _mainDisplay with the parsed value. If the input is invalid,
    //  it displays a warning message asking the user to enter a valid number. If no data is loaded, it shows a message
    //  prompting the user to load data before searching.
    private void IterativeSearchA_Click (object sender, RoutedEventArgs e){
        if (DataDisplayA.Items.Count > 0){
            if (int.TryParse(DataSetTargetInptA.Text, out int value)){
                _mainDisplay.InitialiseIterativeSearch(true, value);
            } else {
                MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a search.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    //  This method is similar to IterativeSearchA_Click, but it performs the iterative search on Sensor B instead.
    //  When the button is clicked, it first checks if there is data in DataDisplayB. If data exists, it attempts to
    //  parse the input from DataSetTargetInptB as an integer. If the input is valid, it calls the InitialiseIterativeSearch
    //  method of _mainDisplay with the parsed value for Sensor B. If the input is invalid, a warning message prompts the
    //  user to enter a valid whole number. If no data is available in DataDisplayB, the method shows a message asking the user
    //  to load data first.
    private void IterativeSearchB_Click (object sender, RoutedEventArgs e){
        if (DataDisplayB.Items.Count > 0){
            if (int.TryParse(DataSetTargetInptB.Text, out int value)){
                _mainDisplay.InitialiseIterativeSearch(false, value);
            } else {
                MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a search.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    //  This method is triggered when the button for performing a recursive search on Sensor A is clicked.
    //  It first checks if there is data in DataDisplayA. If data is present, it attempts to parse the value entered in the
    //  DataSetTargetInptA text box as an integer. If the input is a valid number, it calls the InitialiseRecursiveSearch method
    //  of _mainDisplay with the parsed value for Sensor A. If the input is invalid, it shows a warning message prompting the
    //  user to enter a valid whole number. If no data is available in DataDisplayA, the method displays a message asking the user
    //  to load data before performing the search.
    private void RecursiveSearchA_Click (object sender, RoutedEventArgs e){
        if (DataDisplayA.Items.Count > 0){
            if (int.TryParse(DataSetTargetInptA.Text, out int value)){
                _mainDisplay.InitialiseRecursiveSearch(true, value);
            } else {
                MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a search.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    //  This method is similar to RecursiveSearchA_Click, but it handles the recursive search for Sensor B.
    //  When the button is clicked, it first checks if there is data in DataDisplayB. If data exists, it attempts to parse the value from
    //  the DataSetTargetInptB text box as an integer. If the input is valid, it calls the InitialiseRecursiveSearch method of _mainDisplay
    //  with the parsed value for Sensor B. If the input is not a valid whole number, it displays a warning message. If no data is available
    //  in DataDisplayB, it prompts the user to load data before performing the search.
    private void RecursiveSearchB_Click (object sender, RoutedEventArgs e){
        if (DataDisplayB.Items.Count > 0){
            if (int.TryParse(DataSetTargetInptB.Text, out int value)){
                _mainDisplay.InitialiseRecursiveSearch(false, value);
            } else {
                MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } else {
            MessageBox.Show("Please load some data before attempting to perform a search.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    //  This method is triggered when the button for performing a selection sort on Sensor A is clicked.
    //  It first checks if there is data available in DataDisplayA. If data exists, it calls the InitialiseSelectionSort
    //  method of _mainDisplay to perform the sort on Sensor A. If no data is loaded, it displays a warning message asking
    //  the user to load data before attempting to perform the sort.
    private void SelectionSortA_Click (object sender, RoutedEventArgs e){
        if (DataDisplayA.Items.Count > 0){
            _mainDisplay.InitialiseSelectionSort(true);
        } else {
            MessageBox.Show("Please load some data before attempting to perform a sort.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    //  This method is triggered when the button for performing a selection sort on Sensor B is clicked.
    //  It checks if there is data available in DataDisplayB.
    //  If data is present, it calls the InitialiseSelectionSort method of _mainDisplay to perform the sort
    //  on Sensor B. If no data is loaded, it shows a warning message prompting the user to load data before
    //  attempting the sort.
    private void SelectionSortB_Click (object sender, RoutedEventArgs e){
        if (DataDisplayB.Items.Count > 0){
            _mainDisplay.InitialiseSelectionSort(false);
        } else {
            MessageBox.Show("Please load some data before attempting to perform a sort.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    //  This method is triggered when the button for performing an insertion sort on Sensor A is clicked.
    //  It first checks if there is data in DataDisplayA. If data exists, it calls the InitialiseInsertionSort
    //  method of _mainDisplay to perform the insertion sort on Sensor A. If no data is loaded, it displays a
    //  warning message asking the user to load data before attempting the sort.
    private void InsertionSortA_Click (object sender, RoutedEventArgs e){
        if (DataDisplayA.Items.Count > 0){
            _mainDisplay.InitialiseInsertionSort(true);
        } else {
            MessageBox.Show("Please load some data before attempting to perform a sort.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    //  This method is triggered when the button for performing an insertion sort on Sensor B is clicked.
    //  It checks if there is data available in DataDisplayB. If data exists, it calls the InitialiseInsertionSort method of
    //  _mainDisplay to perform the insertion sort on Sensor B. If no data is loaded, it displays a warning message asking
    //  the user to load data before attempting the sort.
    private void InsertionSortB_Click (object sender, RoutedEventArgs e){
        if (DataDisplayB.Items.Count > 0){
            _mainDisplay.InitialiseInsertionSort(false);
        } else {
            MessageBox.Show("Please load some data before attempting to perform a sort.", "Data Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  This method creates and initializes an OutputElements object.
    //  It assigns various UI elements (likely controls for displaying data, times, and outputs) to the properties
    //  of the OutputElements instance. These include controls for displaying times for different sorting and search
    //  algorithms (IterativeTimeA, RecursiveTimeA, etc.), as well as the list boxes or displays for the two sensor
    //  data sets (DisplayBoxA and DisplayBoxB). Once all the elements are set, it returns the populated OutputElements
    //  object. This method centralizes the setup of UI elements for displaying output related to the sensor data.
    private OutputElements CreateOutputElement (){
        OutputElements output = new();

        output.CombinedListView = CombinedSensorListViewDisplay;

        output.IterativeTimeA = DisplayIterativeTimeA;
        output.RecursiveTimeA = DisplayRecursiveTimeA;
        output.SelectionTimeA = DisplaySelecttionTimeA;
        output.InsertionTimeA = DisplayInsertionTimeA;
        output.DisplayBoxA = DataDisplayA;

        output.IterativeTimeB = DisplayIterativeTimeB;
        output.RecursiveTimeB = DisplayRecursiveTimeB;
        output.SelectionTimeB = DisplaySelectionTimeB;
        output.InsertionTimeB = DisplayInsertionTimeB;
        output.DisplayBoxB = DataDisplayB;

        return output;
    }
}