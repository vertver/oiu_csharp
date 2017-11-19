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
using System.IO.Ports;

namespace AudioSpectrum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Analyzer _analyzer;
        private SerialPort _port;

        public MainWindow()
        {
            InitializeComponent();
            _analyzer = new Analyzer(PbL, PbR, Spectrum, DeviceBox);
        }

        private void BtnEnable_Click(object sender, RoutedEventArgs e)
        {
            if (BtnEnable.IsChecked == true)
            {
                BtnEnable.Content = "Disable";
                _analyzer.Enable = true;
            }
            else
            {
                _analyzer.Enable = false;
                BtnEnable.Content = "Enable";
            }
        }

        private void Comports_DropDownOpened(object sender, EventArgs e)
        {
            Comports.Items.Clear();
            var ports = SerialPort.GetPortNames();
            foreach (var port in ports) Comports.Items.Add(port);
        }

        private void CkbSerial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CkbSerial.IsChecked == true)
                {
                    Comports.IsEnabled = false;
                    _port = new SerialPort((Comports.Items[Comports.SelectedIndex] as string));
                    _port.BaudRate = 115200;
                    _port.StopBits = StopBits.One;
                    _port.Parity = Parity.None;
                    _port.DataBits = 8;
                    _port.DtrEnable = true;
                    _port.Open();
                    _analyzer.Serial = _port;
                }
                else
                {
                    Comports.IsEnabled = true;
                    _analyzer.Serial = null;
                    if (_port != null)
                    {
                        _port.Close();
                        _port.Dispose();
                        _port = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CkbDisplay_Click(object sender, RoutedEventArgs e)
        {
            _analyzer.DisplayEnable = (bool)CkbDisplay.IsChecked;
        }
    }
}
