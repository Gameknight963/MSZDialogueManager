using System.Speech.Synthesis;
using MZDO;

namespace MSZDialougeManager
{
    public static class TTSManager
    {
        private static SpeechSynthesizer synth = new();

        static TTSManager()
        {
            synth.Rate = 0;
            synth.Volume = 100;
        }

        public static void GenerateAudio(NodeRef nodeRef, string outputFolder, string? voice = "Microsoft David Desktop")
        {
            if (voice == null) return; 

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            string filePath = Path.Combine(outputFolder, $"{nodeRef.TreeIndex}_{nodeRef.Node.id}.wav");

            synth.SelectVoice(voice);
            synth.SetOutputToWaveFile(filePath);
            synth.Speak(nodeRef.Node.dialogueText);
        }


        public static async Task PlayText(string text, string voice = "Microsoft David Desktop")
        {
            synth.SelectVoice(voice);
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak(text);
        }

        public static List<string> GetVoices()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            List<string> voices = new List<string>();
            foreach (InstalledVoice? v in synth.GetInstalledVoices())
                voices.Add(v.VoiceInfo.Name);

            return voices;
        }
    }
}
