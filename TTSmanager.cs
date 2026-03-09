using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.IO;

namespace MSZDialougeManager
{
    public static class TTSManager
    {
        private static SpeechSynthesizer synth = new SpeechSynthesizer();

        static TTSManager()
        {
            synth.Rate = 0;
            synth.Volume = 100;
        }

        public static void GenerateAudio(DialogueNodeDTO node, string outputFolder, string voice = "Microsoft David Desktop")
        {
            if (voice == null) return;

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            string filePath = Path.Combine(outputFolder, $"{node.id}.wav");

            using (var synth = new System.Speech.Synthesis.SpeechSynthesizer())
            {
                synth.SelectVoice(voice);
                synth.SetOutputToWaveFile(filePath);
                synth.Speak(node.dialogueText);
            }
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
            foreach (var v in synth.GetInstalledVoices())
                voices.Add(v.VoiceInfo.Name);

            return voices;
        }
    }
}
