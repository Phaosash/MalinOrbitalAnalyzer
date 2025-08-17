using MalinOrbitalAnalyzer.DataModels;
using MalinOrbitalAnalyzer.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {      
    private readonly MainDisplay _mainDisplay;
    
    public MainWindow (){
        InitializeComponent();
        _mainDisplay = new MainDisplay(CreateOutputElement());
    }

    //  Programming requirements 4.14
    private void MaskNumericInput (object sender, TextCompositionEventArgs e){
        e.Handled = !TextIsNumeric(e.Text);
    }

    //  Programming requirements 4.14
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
    private static bool TextIsNumeric (string input){
        if (string.IsNullOrEmpty(input)){
            return false;
        }

        if (input[0] == '-'){
            input = input[1..];
        }
        
        return input.All(c => Char.IsDigit(c) || Char.IsControl(c));
    }

    //  This method initiates the loading of the data for the application
    private void LoadSensorData_Click (object sender, RoutedEventArgs e){
        _mainDisplay.LoadApplicationData(SigmaValueInput.Value ?? 0.0, MuValueInput.Value ?? 0.0);
    }

    //  Programming requirements 4.11
    private void IterativeSearchA_Click (object sender, RoutedEventArgs e){
        if (int.TryParse(DataSetTargetInptA.Text, out int value)){
            _mainDisplay.InitialiseIterativeSearch(true, value);
        } else {
            MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    private void IterativeSearchB_Click (object sender, RoutedEventArgs e){
        if (int.TryParse(DataSetTargetInptB.Text, out int value)){
            _mainDisplay.InitialiseIterativeSearch(false, value);
        } else {
            MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    private void RecursiveSearchA_Click (object sender, RoutedEventArgs e){
        if (int.TryParse(DataSetTargetInptA.Text, out int value)){
            _mainDisplay.InitialiseRecursiveSearch(true, value);
        } else {
            MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.11
    private void RecursiveSearchB_Click (object sender, RoutedEventArgs e){
        if (int.TryParse(DataSetTargetInptB.Text, out int value)){
            _mainDisplay.InitialiseRecursiveSearch(false, value);
        } else {
            MessageBox.Show("Please enter a valid whole number to search for.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    //  Programming requirements 4.12
    private void SelectionSortA_Click (object sender, RoutedEventArgs e){
        _mainDisplay.InitialiseSelectionSort(true);
    }

    //  Programming requirements 4.12
    private void SelectionSortB_Click (object sender, RoutedEventArgs e){
        _mainDisplay.InitialiseSelectionSort(false);
    }

    //  Programming requirements 4.12
    private void InsertionSortA_Click (object sender, RoutedEventArgs e){
        _mainDisplay.InitialiseInsertionSort(true);
    }

    //  Programming requirements 4.12
    private void InsertionSortB_Click (object sender, RoutedEventArgs e){
        _mainDisplay.InitialiseInsertionSort(false);
    }

    //  This method returns a completed OutputElements object,
    //  based off the elements available in the applications UI
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