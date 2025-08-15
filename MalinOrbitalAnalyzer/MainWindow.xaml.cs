using MalinOrbitalAnalyzer.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace MalinOrbitalAnalyzer;

public partial class MainWindow : Window {      
    public MainWindow (){
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}