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
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using Microsoft.Win32;
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
using Un4seen.Bass.AddOn.Wma;
using Un4seen.Bass.AddOn.Vst;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.AddOn.Tags;


namespace oiu_wpf_csharp
{


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        /// <summary>
        /// Main Window
        /// </summary>
        public MainWindow()
        {
            Bass.LoadMe();
            BassNet.Registration("matheuslps@yahoo.com.br", "2X223282334337");  // Yes, you can use this code
            Uri iconUri = new Uri(uriString: "../oiu.ico", uriKind: UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            InitializeComponent();
            // already create a mixer
            
        }


       

        public partial class OpenFileDialogSample : Window
        {



            private void MenuItem_Click2(object sender, RoutedEventArgs e)
            {
                // I don't know why
            }
        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All files|*.mp3; *.m4a; *.alac; *.ogg; *.flac; *.wma; *.wav; *.dsf; *.aif; *.aiff"
            + "|MP3 files (.mp3)|*.mp3"
            + "|ALAC Files (*.alac)|*.alac"
            + "|OGG Files (*.ogg)|*.ogg"
            + "|FLAC Files (*.flac)|*.flac"
            + "|WMA Files (*.wma)|*.wma"
            + "|WAV Files (*.wav)|*.wav"
            + "|AAC Files (*.m4a)|*.m4a"
            + "|Sony 1-bit Files (*.dsf)|*.dsf"
            + "|AIFF Files (*.aif; .aiff)| *.aif; *.aiff";
            Nullable<bool> result = dlg.ShowDialog();
            //if (dlg.ShowDialog() == false) { return; }
            if (dlg.FileName == "")
            {
                //MessageBox.Show("Empty field is not permissible", "File name error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {

                Vars.Files.Add(dlg.FileName);
                playlist.Items.Add(Vars.GetFileName(dlg.FileName));
            }

            //Binding binding = new Binding();

            //binding.ElementName = "progressBarPeakLeft";
            //binding.Path = new PropertyPath("")

        }


        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {

            var sfd = new SaveFileDialog();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "MP3 files (.mp3)|*.mp3";
            Nullable<bool> result = dlg.ShowDialog();
            //if (dlg.ShowDialog() == false) { return; }
        }

        // Ой крч в пизду мне на англе комменты писать, буду так


        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close(); // It's all for a one item?
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All files|*.mp3; *.m4a; *.alac; *.ogg; *.flac; *.wma; *.wav; *.dsf; *.aif; *.aiff"
            + "|MP3 files (.mp3)|*.mp3"
            + "|ALAC Files (*.alac)|*.alac"
            + "|OGG Files (*.ogg)|*.ogg"
            + "|FLAC Files (*.flac)|*.flac"
            + "|WMA Files (*.wma)|*.wma"
            + "|WAV Files (*.wav)|*.wav"
            + "|AAC Files (*.m4a)|*.m4a"
            + "|Sony 1-bit Files (*.dsf)|*.dsf"
            + "|AIFF Files (*.aif; .aiff)| *.aif; *.aiff";
            Nullable<bool> result = dlg.ShowDialog();
            //if (dlg.ShowDialog() == false) { return; }
            if (dlg.FileName == "")
            {
                //MessageBox.Show("Empty field is not permissible", "File name error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {

                Vars.Files.Add(dlg.FileName);
                playlist.Items.Add(Vars.GetFileName(dlg.FileName));
            }
        }

        //int channel;
        //int peakL;
        //int peakR;
        
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
                timer1.Interval = new TimeSpan(0, 0, 1);
                timer1.Start();
                
            }
        }
        private int _mixer = 0;
        private SYNCPROC _mixerStallSync;
        private void timer_Tick(object sender, EventArgs e)
        {
            label1.Content = TimeSpan.FromSeconds(BassUses.GetPosOfStream(BassUses.Stream)).ToString();
            slTime.Value = BassUses.GetPosOfStream(BassUses.Stream);
            int level = Bass.BASS_ChannelGetLevel(_mixer);
           
        }

        //private bool _zoomed = false;
        //private int _zoomStart = -1;
        //private long _zoomStartBytes = -1;
        //private int _zoomEnd = -1;
        //private float _zoomDistance = 5.0f; // zoom = 5sec
        //private WaveForm _WF = null;
        // Waveform editor





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
        {
            BassUses.SetPosOfScroll(BassUses.Stream, slTime.Value);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            About newW = new About();
            newW.Show();
        }

        private void MenuItem_Click_FFTAnalysis(object sender, RoutedEventArgs e)
        {
            FFT_display fftdisp = new FFT_display();
            fftdisp.Show();
        }
        
        private void progressBarLeft_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }


        private int _30mslength = 0;
        private float[] _rmsData;     // our global data buffer used at RMS
        
        #region RMS
        private void RMS(int channel, out int peakL, out int peakR)
        {
            float maxL = 0f;
            float maxR = 0f;
            int length = _30mslength; // 30ms window already set at buttonPlay_Click
            int l4 = length / 4; // the number of 32-bit floats required (since length is in bytes!)

            // increase our data buffer as needed
            if (_rmsData == null || _rmsData.Length < l4)
                _rmsData = new float[l4];

            // Note: this is a special mechanism to deal with variable length c-arrays.
            // In fact we just pass the address (reference) to the first array element to the call.
            // However the .Net marshal operation will copy N array elements (so actually fill our float[]).
            // N is determined by the size of our managed array, in this case N=l4
            length = Bass.BASS_ChannelGetData(channel, _rmsData, length);

            l4 = length / 4; // the number of 32-bit floats received

            for (int a = 0; a < l4; a++)
            {
                float absLevel = Math.Abs(_rmsData[a]);
                // decide on L/R channel
                if (a % 2 == 0)
                {
                    // L channel
                    if (absLevel > maxL)
                        maxL = absLevel;
                }
                else
                {
                    // R channel
                    if (absLevel > maxR)
                        maxR = absLevel;
                }
            }

            // limit the maximum peak levels to +6bB = 0xFFFF = 65535
            // the peak levels will be int values, where 32767 = 0dB!
            // and a float value of 1.0 also represents 0db.
            peakL = (int)Math.Round(32767f * maxL) & 0xFFFF;
            peakR = (int)Math.Round(32767f * maxR) & 0xFFFF;
            progressBarPeakLeft.Value = peakL;
            progressBarPeakRight.Value = peakR;
        }

        // works as well, and should just demo the use of GCHandles
        

        // works as well (even if the slowest), and should just demo the use of Marshal.Copy
        

        
#endregion














    }
}

        
    

