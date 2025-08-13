using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MalinOrbitalAnalyzer.Models;
using MOABackend;
using System.Collections.ObjectModel;

namespace MalinOrbitalAnalyzer.ViewModels;

internal partial class MainWindowViewModel : ViewModelBase {
    private readonly LibraryManager _libraryManager = new();
    
    [ObservableProperty] private double _sigmaValue = 10.0;
    [ObservableProperty] private double _muValue = 50;

    public ObservableCollection<double> ListBoxSensorAItems { get; } = [];
    public ObservableCollection<double> ListBoxSensorBItems { get; } = [];
    public ObservableCollection<SensorData> SensorDataList { get; } = [];
    
    [RelayCommand]
    private void LoadSensorData (){
        _libraryManager.LoadData(SigmaValue, MuValue);
        ShowAllSensorData();
    }

    private void ShowAllSensorData (){
        var listA = _libraryManager.ReturnSensorA();
        var listB = _libraryManager.ReturnSensorB();
        PopulateListView(listA, listB);

        DisplayListboxData(listA, "ListBoxSensorA", ListBoxSensorAItems);
        DisplayListboxData(listB, "ListBoxSensorB", ListBoxSensorBItems);
    }

    private void PopulateListView (LinkedList<double> listA, LinkedList<double> listB){
        if (LibraryManager.NumberOfNodes(listA) > 0 && LibraryManager.NumberOfNodes(listB) > 0){
            var enumA = listA.GetEnumerator();
            var enumB = listB.GetEnumerator();

            bool hasA = enumA.MoveNext();
            bool hasB = enumB.MoveNext();

            while (hasA || hasB){
                var a = hasA ? enumA.Current : (double?)null;
                var b = hasB ? enumB.Current : (double?)null;

                SensorDataList.Add (new SensorData {
                    SensorA = a,
                    SensorB = b
                });

                hasA = hasA && enumA.MoveNext();
                hasB = hasB && enumB.MoveNext();
            }
        }
    }

    private static void DisplayListboxData (LinkedList<double> linkedList, string listBoxName, ObservableCollection<double> target){
        target.Clear();

        foreach (var item in linkedList){
            target.Add(item);
        }
    }
}