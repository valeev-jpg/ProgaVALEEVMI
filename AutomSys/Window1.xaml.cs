using AutomSys.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using System.Windows.Shapes;

namespace AutomSys
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow _mainWindow;
        public Window1(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Popolnenie_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var automate in _mainWindow.Automates)
            {
                automate.FillItems();
                automate.FixAutomate2();
            }
            _mainWindow.Grid.Items.Refresh();
            Popolnenie.IsEnabled = false;
        }
    }
    
}
