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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace oiu_wpf_csharp
{
    public static class BassUses
    {
        /// <summary>
        /// The count of sample rate
        /// </summary>
        private static int SampleRate = 44100;
        /// <summary>
        /// Library Initialization Status
        /// </summary>
        public static bool InitDefaultDevice;
        /// <summary>
        /// Speaker channels for sound signal
        /// </summary>
        public static int Stream;

        /// <summary>
        /// Volume level
        /// </summary>
        public static int Volume = 100;

        /// <summary>
        /// Initialization of Bass.dll
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        private static bool InitBass(int hz)
        {
            if (!InitDefaultDevice)
                InitDefaultDevice = Bass.BASS_Init(-1, SampleRate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            return InitDefaultDevice;

        }

        /// <summary>
        /// Get marker of position 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static int GetPosOfStream(int stream)
        {
            long pos = Bass.BASS_ChannelGetPosition(stream);
            int posSec = (int)Bass.BASS_ChannelBytes2Seconds(stream, pos);
            return posSec;
        }
        /// <summary>
        /// Channel time (seconds)
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static int GetTimeOfStream(int stream)
        {
            long TimeBytes = Bass.BASS_ChannelGetLength(stream);
            double Time = Bass.BASS_ChannelBytes2Seconds(stream, TimeBytes);
            return (int)Time;

        }
        /// <summary>
        /// Play method
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="vol"></param>
        /// 

           
            public static void SetPosOfScroll(int stream, int pos)
        {
            Bass.BASS_ChannelSetPosition(stream, (double)pos);
        }
       
        public static void Play(string filename, int vol)
        {
            Stop();
            if (InitBass(SampleRate))
            {
                Stream = Bass.BASS_StreamCreateFile(filename, 0, 0, BASSFlag.BASS_DEFAULT);
                if (Stream != 0)
                {
                    Volume = vol;
                    Bass.BASS_ChannelSetAttribute(Stream, BASSAttribute.BASS_ATTRIB_VOL ,Volume / 100);
                    Bass.BASS_ChannelPlay(Stream, false);
                }

            }
        }

        /// <summary>
        /// Stop audio method
        /// </summary>
        public static void Stop()
        {
            Bass.BASS_ChannelStop(Stream);
            Bass.BASS_StreamFree(Stream);
        }
        /// <summary>
        /// Volume setup level
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="vol"></param>
        public static void SetVolumeToStream(int stream, int vol)
        {
            Volume = vol;
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100f);
        }

        internal static void SetPosOfScroll(int stream, double value)
        {
            //throw new NotImplementedException();
        }

        internal static void SetVolumeToStream(int stream, double value)
        {
            //throw new NotImplementedException();
        }
    }

}
// TODO: Единственное что я заметил при работе с библиотекой Bass.dll - она намного сложнее.
// Для инициализации нужно использовать более сложные методы. Зато есть и плюсы (более лучший отклик)
