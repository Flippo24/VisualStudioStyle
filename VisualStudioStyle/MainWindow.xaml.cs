using AgentIDE.Core;
using AvalonDock.Layout.Serialization;
using CefSharp.WinForms;
using MaterialDesignColors;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Themes;
using MaterialDesignThemes.Wpf;
using ModernWpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgentIDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MaterialWindow
    {
        public static ChromiumWebBrowser Browser { get; set; }
        public PathControler _PathControler;
        System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();
        MaterialDesignThemes.Wpf.PaletteHelper paletteHelper = new MaterialDesignThemes.Wpf.PaletteHelper();
        public MainWindow()
        {
            InitializeComponent();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            dockManager.Theme = new AvalonDock.Themes.Vs2013DarkTheme();
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Dark);
            paletteHelper.SetTheme(theme);
        }


        private void MaterialWindow_Initialized(object sender, EventArgs e)
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockManager);
            if (File.Exists("Layout.xml"))
            {
                using (var stream = new StreamReader("Layout.xml"))
                {
                    serializer.Deserialize(stream);
                }
            }
        }

        private void MaterialWindow_Closed(object sender, EventArgs e)
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockManager);
            using (var stream = new StreamWriter("Layout.xml"))
            {
                serializer.Serialize(stream);
            }
        }


        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            _PathControler.NewProject();
        }

        private void ToggleDarkMode_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToggleDarkMode.IsChecked)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                dockManager.Theme = new AvalonDock.Themes.Vs2013DarkTheme();
                ITheme theme = paletteHelper.GetTheme();
                theme.SetBaseTheme(Theme.Dark);
                paletteHelper.SetTheme(theme);
                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#2d2d30");
                GridToolBar.Background = brush;
            }
            else
            {
                dockManager.Theme = new AvalonDock.Themes.Vs2013LightTheme();
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                ITheme theme = paletteHelper.GetTheme();
                theme.SetBaseTheme(Theme.Light);
                paletteHelper.SetTheme(theme);
                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#eeeef2");
                GridToolBar.Background = brush;
            }
        }
    }
}
