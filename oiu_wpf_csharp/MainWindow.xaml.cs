/* "oiu" Version GPL Source Code
 /
 / (c) Anton Vertver, Main coder, 2017
 /
 / "oiu" Source Code is free software: you can redistribute it and/or modify for your apps and other projects
 /
 / The code can contain comments in different languages (like a Russia, English)
 /
 / Non-copyright source code
*/
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
            this.MinHeight = 540;
            this.MinWidth = 960;
            Uri iconUri = new Uri(uriString: "../oiu.ico", uriKind: UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            InitializeComponent();
        }

        public new bool IsMouseOver {      
                                                
            get { return _contentLoaded; }          //For checking a mouse focus

            
        } 


        public partial class OpenFileDialogSample : Window
        {
            

           
            private void MenuItem_Click2(object sender, RoutedEventArgs e)
            {

            }
        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            // open a filedialog with option ".wav"
            OpenFileDialog ofd = new OpenFileDialog();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".wav" ;
            dlg.Filter = "WAV files (.wav)|*.wav";
            Nullable<bool> result = dlg.ShowDialog();
        

        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".wav";
            dlg.Filter = "WAV files (.wav)|*.wav";
            Nullable<bool> result = dlg.ShowDialog();



        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
        
    

