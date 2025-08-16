using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.Models;

internal struct OutputElements {
    public ListView? CombinedListView { get; set; }
    public TextBox? IterativeTimeA { get; set; }
    public TextBox? RecursiveTimeA { get; set; }
    public TextBox? SelectionTimeA { get; set; }
    public TextBox? InsertionTimeA { get; set; }
    public ListBox? DisplayBoxA { get; set; }
    public TextBox? IterativeTimeB { get; set; }
    public TextBox? RecursiveTimeB { get; set; }
    public TextBox? SelectionTimeB { get; set; }
    public TextBox? InsertionTimeB { get; set; }
    public ListBox? DisplayBoxB { get; set; }
}