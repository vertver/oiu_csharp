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
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
using Un4seen.Bass.AddOn.Wma;
using Un4seen.Bass.AddOn.Vst;
using Un4seen.BassWasapi;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.AddOn.Tags;

namespace oiu_wpf_csharp
{
    



    public delegate void UpdatePeakMeterCallback();
    public static class BassUses
    {
        // Все комменты, описывающие конкретные классы будут такими
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

        #region Инициализация и освобождение ресурсов
        private static bool InitBass(int hz)
        {
        

            // already create a mixer


            if (!InitDefaultDevice)

                InitDefaultDevice = Bass.BASS_Init(-1, SampleRate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            //После инициализации сразу же ебашим все дллки
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_aac.dll"));   // Мпег-4
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("basswma.dll"));    // ВМАшки
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bassflac.dll"));   // Флаки
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("basswv.dll"));     // Вавка
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("basswasapi.dll")); // Это если MVE подкачает
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bassopus.dll"));   // Любимый наш опус, для стриминга
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bassmix.dll"));    // Библиотека-микшер. Не заменим для DAW
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("basshls.dll"));    // Стриминговая PHP библиотека
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bassenc.dll"));    // Нужен считай для всего
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bassdsd.dll"));    // Аудиофильский однобитный формат, вообще хз как работать с ним
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("basscd.dll"));     // Или же обычное чтение с болванок
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bassalac.dll"));   // ALAC (эпловский флак)
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_ac3.dll"));   // Dolby Digital 
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_ape.dll"));   // Мартышка, ебанная хуишка (лосслесс)
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_fx.dll"));    // Позволяет использовать DSP и эффекты
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_mpc.dll"));   // Музпак
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_ofr.dll"));   // Фришный лосслесс формат 
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_spx.dll"));   // Кодэк для сжатия голоса
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_tta.dll"));   // Лосслесс кодэк (сука, сколько же их)
            BassPluginsHandles.Add(Bass.BASS_PluginLoad("bass_vst.dll"));   // На него молимся
            return InitDefaultDevice;

            
        }


        public static void Free()
        {
            Bass.BASS_Stop();
            for (int i = 0; i < BassPluginsHandles.Count; i++)
                Bass.BASS_PluginFree(BassPluginsHandles[i]);
            Bass.BASS_Free();
        }
        #endregion

        
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

        public static int Getfft(int stream)
        {
            string filepath = "";
            int handle = Bass.BASS_StreamCreateFile(filepath, 0, 0, BASSFlag.BASS_SAMPLE_FLOAT);
            float[] buffer = new float[256];
            int getFFT = Bass.BASS_ChannelGetData(handle, buffer, (int)BASSData.BASS_DATA_FFT256);
            return getFFT;
        }



        // RMS dB meter



        






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
            {   // Кнопка "play", напрямую взаимодействует с уровнем громкости. (А, ну теперь ясно почему этот говнокод не рабо
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
            InitializeComponent();
        }

        private static void InitializeComponent()
        {
            //throw new NotImplementedException();
        }


        internal static void SetVolumeToStream(int stream, double value)
        {
            //throw new NotImplementedException();
        }
        
        // Шо пацаны VST 
        static readonly List<int> BassPluginsHandles = new List<int>(); //Хранит хэндлы всех загружаемых плагинов



        /*private void UpdatePeakMeterDisplay(object sender, EventArgs e)
        {
            _intHandle = Bass.BASS_Init(-1, SampleRate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            _plm = new DSP_PeakLevelMeter(_recHandle, 1);
            _plm.CalcRMS = true;
            _plm.Notification += new EventHandler(UpdatePeakMeterDisplay);
            DSP_PeakLevelMeter _plm;
            this.progressBarRecL.Value = _plm.LevelL;
            this.progressBarRecR.Value = _plm.LevelR;
            this.labelRMS.Text = String.Format("RMS: {0:#00.0} dB - AVG: {1:#00.0} dB - Peak: {2:#00.0} dB",
                        _plm.RMS_dBV,
                        _plm.AVG_dBV,
                        Math.Max(_plm.PeakHoldLevelL_dBV, _plm.PeakHoldLevelR_dBV));
        }
        */


        /*public static int BASS_VST_ChannelSetDSP(
        int chan,
        string dllFile,
        BASSVSTDsp flags,
        int priority
                                                );*/





    }

  


}
// TODO: Единственное что я заметил при работе с библиотекой Bass.dll - она намного сложнее.
// Для инициализации нужно использовать более сложные методы. Зато есть и плюсы (более лучший отклик)
