using NAudio.Vorbis;
using NAudio.Wave;

namespace MSZDialougeManager
{
    public static class NAudioHelpers
    {
        private static readonly Dictionary<string, CachedAudio> cache = new Dictionary<string, CachedAudio>();

        private class CachedAudio
        {
            public required byte[] PcmData;
            public required WaveFormat Format;
            public DateTime FileWriteTimeUtc;
        }

        public static void PlayAudio(string file, ref IWavePlayer? waveOut, ref WaveStream? audioStream)
        {
            ArgumentNullException.ThrowIfNull(file);
            if (!File.Exists(file))
                throw new FileNotFoundException($"{file}: no such file", file);

            StopAudio(ref waveOut, ref audioStream);

            CachedAudio cached = GetOrDecode(file);

            audioStream = new RawSourceWaveStream(new MemoryStream(cached.PcmData), cached.Format);
            waveOut = new WaveOutEvent();
            waveOut.Init(audioStream);
            waveOut.Play();
        }

        public static void StopAudio(ref IWavePlayer? waveOut, ref WaveStream? audioStream)
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            waveOut = null;

            audioStream?.Dispose();
            audioStream = null;
        }

        public static void PreloadAll(IEnumerable<string> files)
        {
            foreach (string file in files)
            {
                if (!string.IsNullOrEmpty(file) && File.Exists(file))
                    GetOrDecode(file);
            }
        }

        private static CachedAudio GetOrDecode(string file)
        {
            DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(file);

            if (cache.TryGetValue(file, out CachedAudio? existing) && existing.FileWriteTimeUtc == lastWriteTimeUtc)
            {
                return existing;
            }

            WaveStream reader = Path.GetExtension(file).Equals(".ogg", StringComparison.OrdinalIgnoreCase)
                ? new VorbisWaveReader(file)
                : new AudioFileReader(file);

            byte[] pcmData;
            WaveFormat format;

            using (reader)
            {
                format = reader.WaveFormat;
                using MemoryStream memoryStream = new();
                reader.CopyTo(memoryStream);
                pcmData = memoryStream.ToArray();
            }

            CachedAudio result = new()
            {
                PcmData = pcmData,
                Format = format,
                FileWriteTimeUtc = lastWriteTimeUtc
            };

            cache[file] = result;
            return result;
        }
    }
}
