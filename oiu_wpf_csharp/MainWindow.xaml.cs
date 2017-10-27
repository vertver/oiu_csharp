using System;
using System.IO;
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
using Microsoft.Win32;


namespace oiu_wpf_csharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.MaxHeight = 540;
            this.MaxWidth = 960;
            this.MinHeight = 540;
            this.MinWidth = 960;
            InitializeComponent();
        }

        public new bool IsMouseOver { //For checking a mouse focus
            get; 
            


        } 


        public partial class OpenFileDialogSample : Window
        {
            public OpenFileDialogSample()
            {

            }
            private void MenuItem_Click(object sender, RoutedEventArgs e)
            {

            }
            private void MenuItem_Click2(object sender, RoutedEventArgs e)
            {

            }
        }
    }
}
