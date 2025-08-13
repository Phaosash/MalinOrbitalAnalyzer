using MalinOrbitalAnalyzer.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {      
    public MainWindow (){
        InitializeComponent();
        DataContext = new MainWindowViewModel();
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
}