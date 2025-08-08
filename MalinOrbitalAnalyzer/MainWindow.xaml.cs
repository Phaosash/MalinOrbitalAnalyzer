using MOABackend;
using System.Data;
using System.Windows;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {    
    private readonly LibraryManager _libraryManager = new();

    public MainWindow (){
        InitializeComponent();
    }

    private void LoadSensorData_Click (object sender, RoutedEventArgs e){
        _libraryManager.LoadData(SigmaValue.Value ?? 0.0, MuValue.Value ?? 0.0);
        ShowAllSensorData();
    }

    private void UpdateListBoxA (LinkedList<double> list){
        ListBoxSensorA.ItemsSource = list;
    }

    private void UpdateListBoxB (LinkedList<double> list){
        ListBoxSensorB.ItemsSource = list;
    }

    private void ShowAllSensorData (){
        ClearDisplays();

        LinkedList<double> listA = _libraryManager.ReturnSensorA();
        LinkedList<double> listB = _libraryManager.ReturnSensorB();

        PopulateListView(listA, listB);
        UpdateListBoxA(listA);
        UpdateListBoxB(listB);
    }

    private void ClearDisplays (){
        CombinedSensorListView.Items.Clear();
        ListBoxSensorA.Items.Clear();
        ListBoxSensorB.Items.Clear();
    }

    private void PopulateListView (LinkedList<double> listA, LinkedList<double> listB){
        var table = new DataTable();
        table.Columns.Add("SensorA", typeof(double));
        table.Columns.Add("SensorB", typeof(double));

        var enumA = listA.GetEnumerator();
        var enumB = listB.GetEnumerator();

        bool hasA = enumA.MoveNext();
        bool hasB = enumB.MoveNext();

        while (hasA || hasB){
            var a = hasA ? enumA.Current : (double?)null;
            var b = hasB ? enumB.Current : (double?)null;
            table.Rows.Add(a, b);

            hasA = hasA && enumA.MoveNext();
            hasB = hasB && enumB.MoveNext();
        }

        CombinedSensorListView.ItemsSource = table.DefaultView;
    }

    private void IterativeSearchA_Click (object sender, EventArgs e){

    }

    private void IterativeSearchB_Click (object sender, EventArgs e){ 
    
    }

    private void RecursiveSearchA_Click (object sender, EventArgs e){ 
    
    }

    private void RecursiveSearchB_Click (object sender, EventArgs e){ 
    
    }

    private void SelectionSortA_Click (object sender, EventArgs e){ 
    
    }

    private void SelectionSortB_Click (object sender, EventArgs e){ 
    
    }
}