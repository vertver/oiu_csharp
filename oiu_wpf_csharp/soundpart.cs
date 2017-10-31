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
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using NAudio.Dsp;
using NAudio.Wave;


// Здесь пишем код для основной части воспроизведения и декодирования

namespace sound
{ }
    //Это всё - нерабочий код для плеера кнопки

    /*private OpenFileDialog openFileDialog = null;
          private NAudio.Wave.IWavePlayer waveOutDevice;
          private NAudio.Wave.BlockAlignReductionStream reductionStream = null;

          private NAudio.Wave.BlockAlignReductionStream CreateStream(OpenFileDialog fileDialog)
          {

              if (fileDialog.FileName.EndsWith(".mp3"))
              {
                  NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(fileDialog.FileName));
                  reductionStream = new NAudio.Wave.BlockAlignReductionStream(pcm);
              }
              else if (fileDialog.FileName.EndsWith(".wav"))
              {
                  NAudio.Wave.WaveStream pcm = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader(openFileDialog.FileName));
                  reductionStream = new NAudio.Wave.BlockAlignReductionStream(pcm);
              }
              else
              {
                  throw new InvalidOperationException("Unsupported");
              }

              return reductionStream;
          }*/

    /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            //waveOutDevice = new NAudio.Wave.DirectSoundOut();
            //reductionStream = CreateStream(openFileDialog);
            //waveOutDevice.Init(reductionStream);
        }*/ // А это для функции
