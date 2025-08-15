using MalinOrbitalAnalyzer.ViewModels;
using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.Views;

public partial class SensorBView : UserControl {
    public SensorBView (){
        InitializeComponent();
        DataContext = new SensorBViewModel();
    }
}