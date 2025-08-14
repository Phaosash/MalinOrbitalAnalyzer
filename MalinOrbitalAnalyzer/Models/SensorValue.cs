using CommunityToolkit.Mvvm.ComponentModel;

namespace MalinOrbitalAnalyzer.Models;

internal partial class SensorValue : ObservableObject {
    [ObservableProperty] private int value;
    [ObservableProperty] private bool isHighlighted;
}