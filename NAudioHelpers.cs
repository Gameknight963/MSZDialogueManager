using NAudio.Vorbis;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSZDialougeManager
{
    public class NAudioHelpers
    {
        public static void PlayAudio(string file, ref IWavePlayer waveOut, ref WaveStream audioStream)
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file)) return;

            StopAudio(ref waveOut, ref audioStream);

            if (Path.GetExtension(file).ToLower() == ".ogg")
                audioStream = new VorbisWaveReader(file);
            else
                audioStream = new AudioFileReader(file);

            waveOut = new WaveOutEvent();
            waveOut.Init(audioStream);
            waveOut.Play();
        }
        public static void StopAudio(ref IWavePlayer waveOut, ref WaveStream audioStream)
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            waveOut = null;

            audioStream?.Dispose();
            audioStream = null;
        }
    }
}
