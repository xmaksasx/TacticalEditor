using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TacticalEditor.Helpers;
using TacticalEditor.Models;

namespace TacticalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MapHelper _mapHelper;
        private CoordinateHelper _coordinateHelper;
        private VisualObjectHelper _visualObjectHelper;
        private ProcessingLoop _processingLoop;
        private MeasureHelper _measureHelper;
        private MenuStates _menuState;
        private MieaPacket _mieaPacket;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _mieaPacket = new MieaPacket();
            _mapHelper = new MapHelper(Grd, ScrollViewer, 5);
            _processingLoop = new ProcessingLoop();
            _visualObjectHelper = new VisualObjectHelper(PlotterVisualObject);
            _coordinateHelper = new CoordinateHelper();
            _measureHelper = new MeasureHelper();

            EventsHelper.DebugNumberEvent += EventsHelperOnDebugNumberEvent;
            ScrollViewer.ScrollToVerticalOffset(2300);
            ScrollViewer.ScrollToHorizontalOffset(4130);
        }

        private void EventsHelperOnDebugNumberEvent(double e)
        {
          Dispatcher.Invoke(()=> Psi.Content = e);
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Grd_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(Grd);
            if (_menuState == MenuStates.NavigationPoint)
                _visualObjectHelper.AddVisualPpm(point);
        }

        private void Grd_MouseMove(object sender, MouseEventArgs e)
        {
            var point = Mouse.GetPosition(Grd);
        }

        private void Mode_OnChecked(object sender, RoutedEventArgs e)
        {
            var rb = (RadioButton) sender;

            switch (rb.Tag?.ToString())
            {
                case "NavigationPoint":
                    _menuState = MenuStates.NavigationPoint;
                    EventsHelper.OnMenuStatusEvent(_menuState);
                    break;
                case "Edit":
                    _menuState = MenuStates.Edit;
                    EventsHelper.OnMenuStatusEvent(_menuState);
                    break;
                case "Measure":
                    _menuState = MenuStates.Measure;
                    EventsHelper.OnMenuStatusEvent(_menuState);
                    break;
                    
            }
        }

        private void OpenRoute_OnClick(object sender, RoutedEventArgs e)
        { 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".rut";
            dlg.Filter = "Route Files (*.rut)|*.rut";
            bool? result = dlg.ShowDialog();
            if(result == true)
            {
                string filename = dlg.FileName;
                _visualObjectHelper.OpenRoute(filename);
            }
            EventsHelper.OnMenuStatusEvent(_menuState);
        }

        private void SaveRoute_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".rut"; 
            dlg.Filter = "Route Files (.rut)|*.rut"; 
            bool? result = dlg.ShowDialog();
            if(result == true)
            {
                string filename = dlg.FileName;
                _visualObjectHelper.SaveRoute(filename);
            }
        }

        private void ClearRoute_OnClick(object sender, RoutedEventArgs e)
        {
            _visualObjectHelper.Clear();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            _processingLoop.Destroy();
            Application.Current.Shutdown();
        }

        private void RouteCube_OnClick(object sender, RoutedEventArgs e)
        {
            _visualObjectHelper.AddDebugPm();
        }
    }

    enum MenuStates
    {
        NavigationPoint,
        Measure,
        Edit
    }

}
