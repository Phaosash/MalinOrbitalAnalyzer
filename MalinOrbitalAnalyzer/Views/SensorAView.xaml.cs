using MalinOrbitalAnalyzer.ViewModels;
using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.Views;

public partial class SensorAView : UserControl {
    public SensorAView (){
        InitializeComponent();
        DataContext = new SensorAViewModel();
    }
}