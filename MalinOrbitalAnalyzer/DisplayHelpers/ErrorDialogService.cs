using ErrorLoggingLibrary;
using System.Windows;

namespace MalinOrbitalAnalyzer.DisplayHelpers;

internal static class ErrorDialogService {
    //  This method logs an error and displays an error message to the user.
    //  It first uses the LoggingHandler.Instance.LogError method to log the
    //  exception (`ex`) with a custom logMessage. Then, it shows a message
    //  box to the user with the specified userMessage, displaying it as an
    //  error alert with an "OK" button and an error icon. This ensures that
    //  both the error is logged and the user is notified in a clear,
    //  visible way.
    public static void ShowError (string userMessage, Exception ex, string logMessage){
        LoggingHandler.Instance.LogError(logMessage, ex);
        MessageBox.Show(userMessage, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    //  This method displays an informational message to the user. It takes a string message
    //  as a parameter and shows it in a message box with the title "Alert". The message box
    //  includes an "OK" button and an information icon to clearly indicate that the message
    //  is informational. This method is useful for notifying users of general information
    //  or updates.
    public static void ShowInfo (string message){
        MessageBox.Show(message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    //  This method displays a warning message to the user. It takes a string message as a
    //  parameter and shows it in a message box with the title "Alert". The message box includes
    //  an "OK" button and a warning icon, indicating that the message is intended to alert the
    //  user to a potential issue or caution. This method is useful for drawing attention to
    //  non-critical but important warnings.
    public static void ShowWarning (string message){
        MessageBox.Show(message, "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}