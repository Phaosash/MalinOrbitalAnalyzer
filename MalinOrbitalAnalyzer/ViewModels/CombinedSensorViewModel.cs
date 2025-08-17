using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MalinOrbitalAnalyzer.Models;
using System.Windows;

namespace MalinOrbitalAnalyzer.ViewModels;

internal partial class CombinedSensorViewModel : ObservableObject {
    public double SigmaDataValue { get; set; }
    public double MuDataValue { get; set; }

    public CombinedSensorViewModel (){        
        SigmaDataValue = 10;
        MuDataValue = 50;
    }

    [RelayCommand]
    private void LoadSensorData (){
        MessageBox.Show("Loading data." + "\tSigma value: " + SigmaDataValue + "\tMu value: " + MuDataValue);
        SensorAViewModel sensorA = new();

        sensorA.StartLoadingData(SigmaDataValue, MuDataValue);
    }
}