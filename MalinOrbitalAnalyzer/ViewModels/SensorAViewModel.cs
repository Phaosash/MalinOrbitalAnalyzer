using CommunityToolkit.Mvvm.ComponentModel;
using MalinOrbitalAnalyzer.Models;
using MOABackend;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace MalinOrbitalAnalyzer.ViewModels;

internal partial class SensorAViewModel : ObservableObject {
    private readonly SensorManagerA _sensorManager = new();

    public ObservableCollection<double> ListBoxItems { get; } = [];

    public void StartLoadingData (double average, double deviation){
        _sensorManager.SetSensorA(average, deviation);

        DisplaySensorData();
    }

    private void DisplaySensorData (){
        var list = _sensorManager.GetSensorA();

        if (list.Count == 0){
            MessageBox.Show("Unable to find a matching ListBox");
            return;
        }

        ListBoxItems.Clear();

        foreach (var item in list) {
            ListBoxItems.Add(item);
        }
    }
}