using MalinOrbitalAnalyzer.ViewModels;
using System.Windows;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {      
    public MainWindow (){
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}