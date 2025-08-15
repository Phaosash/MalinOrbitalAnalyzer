using MalinOrbitalAnalyzer.ViewModels;
using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.Views;

public partial class CombinedSensorView : UserControl {
    public CombinedSensorView (){
        InitializeComponent();
        DataContext = new CombinedSensorViewModel();
    }
}