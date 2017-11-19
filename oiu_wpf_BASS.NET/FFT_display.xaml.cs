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
using System.Windows.Shapes;
using Un4seen.Bass.Misc;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;

namespace oiu_wpf_csharp
{
    /// <summary>
    /// Логика взаимодействия для FFT_display.xaml
    /// </summary>
    public partial class FFT_display : Window
    {
        public FFT_display()
        {
            Bass.LoadMe();
            BassNet.Registration("matheuslps@yahoo.com.br", "2X223282334337");  // Yes, you can use this code
            Uri iconUri = new Uri(uriString: "../oiu.ico", uriKind: UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            InitializeComponent();
        }

        //private int stream;
        float[] buffer = new float[2048];
        float[] avg = new float[16];
        public bool CreateSpectrum(
        int channel,
        System.Drawing.Graphics g,
        Rectangle clipRectangle,
        Color color1,
        Color color2,
        Color background,
        bool linear,
        bool fullSpectrum,
        bool highQuality)
        {

            bool ffts = true;
            return ffts;

        }

        private int _StreamFX = 0;
        private string _FileName = String.Empty;
        private int _TickCounter = 0;
        private int _20mslength = 0;
        private float[] _rmsData;     // our global data buffer used at RMS

        private void SimpleFX_Load(object sender, System.EventArgs e)
        {
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_SPEAKERS, IntPtr.Zero))
            {
                // all ok
                // load BASS_FX
                //BassUses.Play(_stream1, BassUses.Volume);

            }
            else
                MessageBox.Show(this, "Bass_Init error!");
        }



        private double RMS(int channel, out int peakL, out int peakR)
        {
            double sum = 0f;
            float maxL = 0f;
            float maxR = 0f;
            int length = _20mslength;
            int l4 = length / 4; // the number of 32-bit floats required (since length is in bytes!)

            // increase our data buffer as needed
            if (_rmsData == null || _rmsData.Length < l4)
                _rmsData = new float[l4];

            try
            {
                length = Bass.BASS_ChannelGetData(channel, _rmsData, length);
                l4 = length / 4; // the number of 32-bit floats received

                for (int a = 0; a < l4; a++)
                {
                    sum += _rmsData[a] * _rmsData[a]; // sum the squares
                                                      // decide on L/R channel
                    if (a % 2 == 0)
                    {
                        // L channel
                        if (_rmsData[a] > maxL)
                            maxL = _rmsData[a];
                    }
                    else
                    {
                        // R channel
                        if (_rmsData[a] > maxR)
                            maxR = _rmsData[a];
                    }
                }
            }
            catch { }

            peakL = (int)Math.Round(32768f * maxL);
            if (peakL > 32768)
                peakL = 32768;
            peakR = (int)Math.Round(32768f * maxR);
            if (peakR > 32768)
                peakR = 32768;

            return Math.Sqrt(sum / (l4 / 2));  // l4/2, since we use 2 channels!
        }



        private void timerUpdate_Tick(object sender, System.EventArgs e)
        {
            // here we gather info about the stream, when it is playing...
            if (Bass.BASS_ChannelIsActive(_StreamFX) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                // the stream is still playing...
                
            }
            else
            {
                // the stream is NOT playing anymore...
                this.progressBarPeakLeft.Value = 0;
                this.progressBarPeakRight.Value = 0;
                return;
            }

            // display the level bars
            int peakL = 0;
            int peakR = 0;
            double rms = RMS(_StreamFX, out peakL, out peakR);
            this.progressBarPeakLeft.Value = peakL;
            this.progressBarPeakRight.Value = peakR;

            // from here on, the stream is for sure playing...
            _TickCounter++;
            if (_TickCounter == 4)
            {
                // display the position every 200ms (since timer is 50ms)
                _TickCounter = 0;
                long len = Bass.BASS_ChannelGetLength(_StreamFX); // length in bytes
                long pos = Bass.BASS_ChannelGetPosition(_StreamFX); // position in bytes
                double totaltime = Bass.BASS_ChannelBytes2Seconds(_StreamFX, len); // the total time length
                double elapsedtime = Bass.BASS_ChannelBytes2Seconds(_StreamFX, pos); // the elapsed time length
                double remainingtime = totaltime - elapsedtime;
                this.labelTime.Content = String.Format("Elapsed: {0} - Total: {1} - Remain: {2}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"), Utils.FixTimespan(remainingtime, "MMSS"));
                this.Content = String.Format("CPU: {0:0.00}%", Bass.BASS_GetCPU());
                //this.labelRMSValue.Text = Utils.LevelToDB(rms, 1d).ToString("0.0");
            }
        }


      

    }

}
