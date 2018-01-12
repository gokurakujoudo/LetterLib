using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Navigation;
using LetterLib.Data;
using LetterLib.Utility;
using SharpHelper.Util;

namespace LetterGui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditWindow {
        public EditWindow() { InitializeComponent(); }

        private bool _needQuit = false;
        private ViewModel GetViewModel() =>this. DataContext.To<ViewModel>();

        private void SubQuit(object sender, CancelEventArgs e) {
            if (_needQuit) WordHelper.SafeQuit();
        }

        private void SubCopyFieldName(object sender, RoutedEventArgs e) {
            var btn =(Button) sender;
            var field = (FieldSlot) btn.DataContext;
            Clipboard.SetDataObject(field.GetFindString());
        }

        private void SubCopyResName(object sender, RoutedEventArgs e) {
            var btn = (Button)sender;
            var resource = (ParaResource)btn.DataContext;
            Clipboard.SetDataObject(resource.GetFindString());
        }

        private void SubSave(object sender, RoutedEventArgs e) {
            TemplateHelper.CurrenTemplate = GetViewModel().Template;
            if (MessageBox.Show("Save?", "", MessageBoxButton.YesNo, MessageBoxImage.Information) !=
                MessageBoxResult.Yes) return;
            TemplateHelper.Save(Program.fPath);
        }

        private void SubGenerate(object sender, RoutedEventArgs e) {
            _needQuit = true;
            var temp = GetViewModel().Template;
            temp.BuildFile();
        }

        private void SubLink(object sender, RequestNavigateEventArgs e) {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }

    [ValueConversion(typeof(bool), typeof(double))]
    public class ToggleToHeightConverter:IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var check = (bool) (value ?? false);
            return check ? 100 : 20;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class ToggleToVisiableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var check = (bool)(value ?? false);
            return check ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }
}
