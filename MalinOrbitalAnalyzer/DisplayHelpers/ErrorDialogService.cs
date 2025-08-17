using ErrorLogging;
using System.Windows;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal static class ErrorDialogService {
    public static void ShowError (string userMessage, Exception ex, string logMessage){
        LoggingHandler.Instance.LogError(logMessage, ex);
        MessageBox.Show(userMessage, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public static void ShowInfo (string message){
        MessageBox.Show(message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void ShowWarning (string message){
        MessageBox.Show(message, "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}