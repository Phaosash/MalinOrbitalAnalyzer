using MOABackend;
using System.Windows;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {    
    private readonly LibraryManager _libraryManager = new();

    public MainWindow (){
        InitializeComponent();
    }

    private void LoadSensorData_Click (object sender, RoutedEventArgs e){
        _libraryManager.LoadData(SigmaValue.Value ?? 0.0, MuValue.Value ?? 0.0);

        UpdateListBoxA();
        UpdateListBoxB();
    }

    private void UpdateListBoxA (){
        ListBoxSensorA.Items.Clear();

        ListBoxSensorA.ItemsSource = _libraryManager.ReturnSensorA();
    }

    private void UpdateListBoxB (){
        ListBoxSensorB.Items.Clear();

        ListBoxSensorB.ItemsSource = _libraryManager.ReturnSensorB();
    }
}