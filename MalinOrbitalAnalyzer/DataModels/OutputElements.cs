using System.Windows.Controls;

namespace MalinOrbitalAnalyzer.Models;

//  This struct defines several nullable UI control properties, including TextBox and ListBox,
//  related to displaying and tracking times for iterative, recursive, selection, and insertion
//  processes. It includes separate controls for two categories, "A" and "B," potentially for
//  different data sets or stages in an operation.
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