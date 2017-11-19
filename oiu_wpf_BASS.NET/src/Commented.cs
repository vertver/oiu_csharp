using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oiu_wpf_csharp.src
{
    class Commented
    {
        /*private double RMS(int channel, out int peakL, out int peakR)
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
        */

        /*     double sum = 0f;
                // float maxL = 0f;
                // float maxR = 0f;
                // int length = _20mslength;
                // int l4 = length / 4; // the number of 32-bit floats required (since length is in bytes!)

                 // increase our data buffer as needed
                // if (_rmsData == null || _rmsData.Length < l4)
                //     _rmsData = new float[l4];

                // try
                // {
                //     length = Bass.BASS_ChannelGetData(channel, _rmsData, length);
                //     l4 = length / 4; // the number of 32-bit floats received
                //
                //     for (int a = 0; a < l4; a++)
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

                 Math.Sqrt(sum / (l4 / 2));  // l4/2, since we use 2 channels!

                 */

        /* if (Bass.BASS_ChannelIsActive(BassUses.Stream) == BASSActive.BASS_ACTIVE_PLAYING)
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
           double rms = RMS(BassUses.Stream, out peakL, out peakR);
           this.progressBarPeakLeft.Value = peakL;
           this.progressBarPeakRight.Value = peakR;

           // from here on, the stream is for sure playing...
           _TickCounter++;
           if (_TickCounter == 4)
           {
               // display the position every 200ms (since timer is 50ms)
               _TickCounter = 0;
               long len = Bass.BASS_ChannelGetLength(BassUses.Stream); // length in bytes
               long pos = Bass.BASS_ChannelGetPosition(BassUses.Stream); // position in bytes
               double totaltime = Bass.BASS_ChannelBytes2Seconds(BassUses.Stream, len); // the total time length
               double elapsedtime = Bass.BASS_ChannelBytes2Seconds(BassUses.Stream, pos); // the elapsed time length
               double remainingtime = totaltime - elapsedtime;
               //this.Content = String.Format("CPU: {0:0.00}%", Bass.BASS_GetCPU());
               //this.labelRMSValue.Text = Utils.LevelToDB(rms, 1d).ToString("0.0");
           }
           */

        /*private int _StreamFX = 0;
        private string _FileName = String.Empty;
        private int _TickCounter = 0;
        private int _20mslength = 0;
        private float[] _rmsData;     // our global data buffer used at RMS

        private void SimpleFX_Load(object sender, System.EventArgs e)
        {
            if (timer1 != null)
            {
                // all ok
                // load BASS_FX
                //BassUses.Play(_stream1, BassUses.Volume);

            }
            else
                MessageBox.Show(this, "Bass_Init error!");
        }
        */


    }
}
