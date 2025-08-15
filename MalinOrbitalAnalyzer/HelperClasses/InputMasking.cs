using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MalinOrbitalAnalyzer.HelperClasses;

internal static class InputMasking {
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(InputMasking),
            new PropertyMetadata(false, OnIsEnabledChanged));

    public static bool GetIsEnabled (DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);

    public static void SetIsEnabled (DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

    private static void OnIsEnabledChanged (DependencyObject d, DependencyPropertyChangedEventArgs e){
        if (d is TextBox textBox){
            if ((bool)e.NewValue){
                textBox.PreviewTextInput += MaskNumericInput;
                DataObject.AddPastingHandler(textBox, MaskNumericPaste);
            } else {
                textBox.PreviewTextInput -= MaskNumericInput;
                DataObject.RemovePastingHandler(textBox, MaskNumericPaste);
            }
        }
    }

    private static void MaskNumericInput (object sender, TextCompositionEventArgs e){
        e.Handled = !IsTextNumeric(e.Text);
    }

    private static void MaskNumericPaste (object sender, DataObjectPastingEventArgs e){
        if (e.DataObject.GetDataPresent(typeof(string))){
            string input = (string)e.DataObject.GetData(typeof(string));
            if (!IsTextNumeric(input))
                e.CancelCommand();
        } else {
            e.CancelCommand();
        }
    }

    private static bool IsTextNumeric (string input){
        return input.All(c => char.IsDigit(c) || char.IsControl(c));
    }
}