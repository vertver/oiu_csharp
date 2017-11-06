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
using System.Windows.Threading;
using System.Windows.Forms;
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
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using Microsoft.Win32;
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;
using Un4seen.Bass;

namespace oiu_wpf_csharp
{


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        /// <summary>
        /// Главное окно 
        /// </summary>
        public MainWindow()
        {
            Bass.LoadMe();
            this.MinHeight = 540;
            this.MinWidth = 960;
            Uri iconUri = new Uri(uriString: "../oiu.ico", uriKind: UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            InitializeComponent();
        }

        public partial class OpenFileDialogSample : Window
        {



            private void MenuItem_Click2(object sender, RoutedEventArgs e)
            {
                // Бесполезный пункт меню, нормально толком не работает
            }
        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            // open a filedialog with option ".wav"
             var ofd = new OpenFileDialog();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "MP3 files (.mp3)|*.mp3";
            Nullable<bool> result = dlg.ShowDialog();
            if (dlg.ShowDialog() == false) { return; }
            Vars.Files.Add(dlg.FileName);
            playlist.Items.Add(Vars.GetFileName(dlg.FileName));



        }


        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {

            var sfd = new SaveFileDialog();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "MP3 files (.mp3)|*.mp3";
            Nullable<bool> result = dlg.ShowDialog();
            if (dlg.ShowDialog() == false) { return; }

            // Давайте создадим условие, при котором если поле равно нулю, то мы его шлём нахуй.
            // Очень полезный сейвдиалог (прям сука очень)
        }

        // Ой крч в пизду мне на англе комменты писать, буду так


        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close(); // И ради этого создавать приватный воид?
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "MP3 files (.mp3)|*.mp3";
            Nullable<bool> result = dlg.ShowDialog();
            if (dlg.ShowDialog() == false) { return; }
            Vars.Files.Add(dlg.FileName);
            playlist.Items.Add(Vars.GetFileName(dlg.FileName));
      
        }

        private DispatcherTimer timer1 = null;
        private void Button_Click_1(object sender, RoutedEventArgs e) // Play
        {
            if ((playlist.Items.Count != 0) && (playlist.SelectedIndex != -1))
            {
                string current = Vars.Files[playlist.SelectedIndex];
                BassUses.Play(current, BassUses.Volume);
                label1.Content = TimeSpan.FromSeconds(BassUses.GetPosOfStream(BassUses.Stream)).ToString();
                label2.Content = TimeSpan.FromSeconds(BassUses.GetTimeOfStream(BassUses.Stream)).ToString();
                slTime.Maximum = BassUses.GetTimeOfStream(BassUses.Stream);
                slTime.Value = BassUses.GetPosOfStream(BassUses.Stream);
                timer1 = new DispatcherTimer();
                timer1.Tick += new EventHandler(timer_Tick);
                timer1.Interval = new TimeSpan(0,0,1);
                timer1.Start();

            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            label1.Content = TimeSpan.FromSeconds(BassUses.GetPosOfStream(BassUses.Stream)).ToString();
            slTime.Value = BassUses.GetPosOfStream(BassUses.Stream);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Stop
        {
            BassUses.Stop();
            if (timer1 == null) { return; }
            timer1.IsEnabled = false;
            slTime.Value = 0;
            label1.Content = "00:00:00";
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // Volume Level
        {
            BassUses.SetVolumeToStream(BassUses.Stream, slVol.Value);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void slTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        => BassUses.SetPosOfScroll(BassUses.Stream, slTime.Value);

    }
}

        
    

