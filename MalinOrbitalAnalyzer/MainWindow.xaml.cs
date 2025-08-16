using MalinOrbitalAnalyzer.DisplayHelpers;
using MalinOrbitalAnalyzer.Models;
using MalinOrbitalAnalyzer.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {      
    private MainDisplay _mainDisplay;
    
    public MainWindow (){
        InitializeComponent();
        DataContext = new MainWindowViewModel();

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
            if (!TextIsNumeric(input)) e.CancelCommand();
        } else {
            e.CancelCommand();
        }
    }

    //  Programming requirements 4.14
    private static bool TextIsNumeric (string input){
        return input.All(c => Char.IsDigit(c) || Char.IsControl(c));
    }

    private OutputElements CreateOutputElement (){
        OutputElements output = new();

        output.CombinedListView = CombinedSensorListView;

        output.IterativeTimeA = IterativeTimeA;
        output.RecursiveTimeA = RecursiveTimeA;
        output.SelectionTimeA = SelectionTimeA;
        output.InsertionTimeA = InsertionTimeA;
        output.DisplayBoxA = ListBoxSensorA;

        output.IterativeTimeB = IterativeTimeB;
        output.RecursiveTimeB = RecursiveTimeB;
        output.SelectionTimeB = SelectionTimeB;
        output.InsertionTimeB = InsertionTimeB;
        output.DisplayBoxB = ListBoxSensorB;

        return output;
    }
}