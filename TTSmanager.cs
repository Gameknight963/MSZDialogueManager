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

        public static void GenerateAudio(DialogueNodeDTO node, string outputFolder)
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            string filePath = Path.Combine(outputFolder, $"{node.id}.wav");

            synth.SelectVoice("Microsoft David Desktop");
            synth.SetOutputToWaveFile(filePath);
            synth.Speak(node.dialogueText);
            synth.SetOutputToDefaultAudioDevice();
        }
    }

}
