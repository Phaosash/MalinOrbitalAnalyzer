using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MalinOrbitalAnalyzer.Models;
using MOABackend;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;

namespace MalinOrbitalAnalyzer.ViewModels;

internal partial class MainWindowViewModel : ViewModelBase {
    private readonly LibraryManager _libraryManager = new();
    
    [ObservableProperty] private double _sigmaValue = 10.0;
    [ObservableProperty] private double _muValue = 50;
    [ObservableProperty] private readonly int _searchTargetA;
    [ObservableProperty] private readonly int _searchTargetB;
    [ObservableProperty] private string? _selectionTimeA;
    [ObservableProperty] private string? _selectionTimeB;
    [ObservableProperty] private string? _insertionTimeA;
    [ObservableProperty] private string? _insertionTimeB;
    [ObservableProperty] private string? _iterativeSearchTimeA;
    [ObservableProperty] private string? _iterativeSearchTimeB;
    [ObservableProperty] private string? _recursiveSearchTimeA;
    [ObservableProperty] private string? _recursiveSearchTimeB;

    public ObservableCollection<double> ListBoxSensorAItems { get; } = [];
    public ObservableCollection<double> ListBoxSensorBItems { get; } = [];
    public ObservableCollection<SensorData> SensorDataList { get; } = [];
    
    //  Programming requirements 4.4
    [RelayCommand]
    private void LoadSensorData (){
        _libraryManager.LoadData(SigmaValue, MuValue);
        ShowAllSensorData();
    }

    //  Programmig requirements 4.3
    private void ShowAllSensorData (){
        var listA = _libraryManager.ReturnSensorA() ?? new LinkedList<double>();
        var listB = _libraryManager.ReturnSensorB() ?? new LinkedList<double>();
        
        PopulateListView(listA, listB);

        DisplayListboxData(listA, "ListBoxSensorA");
        DisplayListboxData(listB, "ListBoxSensorB");
    }

    private void PopulateListView (LinkedList<double> listA, LinkedList<double> listB){
        SensorDataList.Clear();

        using var enumA = listA.GetEnumerator();
        using var enumB = listB.GetEnumerator();

        bool hasA = enumA.MoveNext();
        bool hasB = enumB.MoveNext();

        while (hasA || hasB){
            double? a = hasA ? enumA.Current : (double?)null;
            double? b = hasB ? enumB.Current : (double?)null;

            SensorDataList.Add(new SensorData {
                SensorA = a,
                SensorB = b
            });

            hasA = hasA && enumA.MoveNext();
            hasB = hasB && enumB.MoveNext();
        }
    }

    //  Programming requirements 4.6
    private void DisplayListboxData (LinkedList<double> linkedList, string listBoxName){
        var listBoxMap = new Dictionary<string, ObservableCollection<double>>() {
            { "ListBoxSensorA", ListBoxSensorAItems },
            { "ListBoxSensorB", ListBoxSensorBItems }
        };

        if (!listBoxMap.TryGetValue(listBoxName, out var target)){
            MessageBox.Show("Unable to find a matching ListBox Name");
            return;
        }

        target.Clear();

        foreach (var item in linkedList){
            target.Add(item);
        }
    }

    //  Programming requirements 4.12
    [RelayCommand]
    private void SelectionSortSensorA (){
        Stopwatch sw = Stopwatch.StartNew();
        var success = _libraryManager.RunSelectionSort(true);
        sw.Stop();

        SelectionTimeA = sw.ElapsedMilliseconds + "ms";

        if (success){
            MessageBox.Show("Successfully completed the selection sort on Sensor A");
            DisplayListboxData(_libraryManager.ReturnSensorA(), "ListBoxSensorA");
        } else {
            MessageBox.Show("Failed to complete the selection sort on Sensor A");
        }
    }

    //  Programming requirements 4.12
    [RelayCommand]
    private void SelectionSortSensorB (){
        Stopwatch stopwatch = Stopwatch.StartNew();
        var success = _libraryManager.RunSelectionSort(false);
        stopwatch.Stop();

        SelectionTimeB = stopwatch.ElapsedMilliseconds + "ms";

        if (success){
            MessageBox.Show("Successfully completed the selection sort on Sensor B");
            DisplayListboxData(_libraryManager.ReturnSensorB(), "ListBoxSensorB");
        } else {
            MessageBox.Show("Failed to complete the selection sort on Sensor B");
        }
    }

    //  Programming requirements 4.12
    [RelayCommand]
    private void InsertionSortSensorA (){
        Stopwatch startwatch = Stopwatch.StartNew();
        var success = _libraryManager.RunInsertionSort(true);
        startwatch.Stop();

        InsertionTimeA = startwatch.ElapsedMilliseconds + "ms";

        if (success){
            MessageBox.Show("Successfully completed the insertion sort on Sensor A");
            DisplayListboxData(_libraryManager.ReturnSensorA(), "ListBoxSensorA");
        } else {
            MessageBox.Show("Failed to complete the insertion sort on Sensor A");
        }
    }

    //  Programming requirements 4.12
    [RelayCommand]
    private void InsertionSortSensorB (){
        Stopwatch stopwatch = Stopwatch.StartNew();
        var success = _libraryManager.RunInsertionSort(false);
        stopwatch.Stop();

        InsertionTimeB = stopwatch.ElapsedMilliseconds + "ms";

        if (success){
            MessageBox.Show("Successfully completed the insertion sort on Sensor B");
            DisplayListboxData(_libraryManager.ReturnSensorB(), "ListBoxSensorB");
        } else {
            MessageBox.Show("Failed to complete the insertion sort on Sensor B");
        }
    }

    //  Programming requirements 4.11
    [RelayCommand]
    private void IterativeSearchSensorA (){

    }

    //  Programming requirements 4.11
    [RelayCommand]
    private void IterativeSearchSensorB (){

    }

    //  Programming requirements 4.11
    [RelayCommand]
    private void RecursiveSearchSensorA (){

    }

    //  Programming requirements 4.11
    [RelayCommand]
    private void RecursiveSearchSensorB (){

    }
}