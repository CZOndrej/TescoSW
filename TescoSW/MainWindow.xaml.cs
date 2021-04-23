using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace TescoSW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Manager _manager;
        public MainWindow()
        {
            InitializeComponent();
            _manager = new Manager();
            allCars_dg.ItemsSource = _manager.Cars;
            allCars_dg.AutoGenerateColumns = false;
            ws_dg.ItemsSource = _manager.WeekendSales;
            ws_dg.AutoGenerateColumns = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                path_tb.Text = dlg.FileName;
            }
        }

        private void Load_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(path_tb.Text))
            {
                try
                {   
                    _manager.LoadXml(path_tb.Text);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }    
            }
            else
            {
                MessageBox.Show("Nevybrali jste žádný soubor", "Chybějící cesta k souboru", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }






    }
}
