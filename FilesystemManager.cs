using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace MSZDialougeManager
{
    public class FilesystemManager
    {
        public static string BaseDir => AppDomain.CurrentDomain.BaseDirectory;
        public static string DataPath => Path.Combine(BaseDir, "Data");
        public static string Templete => Path.Combine(BaseDir, "templeteNodes.json");

        /// <summary>
        /// Don't use this path if you don't currently have a nodes.json in Data/
        /// </summary>
        public static string NodesJsonPath => Path.Combine(DataPath, "nodes.json");

        /// <summary>
        /// The custom dialouge extension (without the dot)
        /// </summary>
        public static readonly string ext = "mszdlg";

        public static bool IsFileLoaded = false;

        public static void SaveProj(string path, DialogueForest forest)
        {
            if (File.Exists(path)) File.Delete(path);

            string actualExt = System.IO.Path.GetExtension(path).TrimStart('.').ToLower();
            if (actualExt != ext) path = System.IO.Path.ChangeExtension(path, ext);

            string json = JsonConvert.SerializeObject(forest, Formatting.Indented);
            File.WriteAllText(Path.Combine(DataPath, "nodes.json"), json);
            ZipFile.CreateFromDirectory(DataPath, path);
        }

        public static void SaveJson(string path, DialogueForest forest)
        {
            string json = JsonConvert.SerializeObject(forest, Formatting.Indented);
            if (File.Exists(path)) File.Delete(path);
            File.WriteAllText(path, json);
        }

        public static DialogueForest LoadProj(string path)
        {
            if (Directory.Exists(DataPath)) Directory.Delete(DataPath, true);
            Directory.CreateDirectory(DataPath);
            ZipFile.ExtractToDirectory(path, DataPath);
            string json = File.ReadAllText(Path.Combine(NodesJsonPath)); 
            DialogueForest forest = JsonConvert.DeserializeObject<DialogueForest>(json);
            IsFileLoaded = true;
            return forest;
        }

        public static DialogueForest LoadJson(string path)
        {
            string json = File.ReadAllText(path);
            DialogueForest forest = JsonConvert.DeserializeObject<DialogueForest>(json);
            IsFileLoaded = true;
            return forest;
        }

        public static void AddNodeAudio(DialogueNodeDTO node, string audioPath)
        {
            string[] existingFiles = Directory.GetFiles(FilesystemManager.DataPath, $"{node.id}.*");
            foreach (string file in existingFiles) File.Delete(file);

            string ext = Path.GetExtension(audioPath);
            string destination = Path.Combine(FilesystemManager.DataPath, $"{node.id}{ext}");
            File.Copy(audioPath, destination);
        }

        public static void RemoveNodeAudio(DialogueNodeDTO node)
        {
            string[] existingFiles = Directory.GetFiles(FilesystemManager.DataPath, $"{node.id}.*");
            foreach (string file in existingFiles) File.Delete(file);
        }


        public static bool DoesNodeAudioExist(DialogueNodeDTO node)
        {
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
                return false;
            }
            string[] files = Directory.GetFiles(
                DataPath,
                $"{node.id}.*"
            );
            return (files.Length > 0);
        }

        public static string GetNodeAudioClip(DialogueNodeDTO node)
        {
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
                return null;
            }
            string[] files = Directory.GetFiles(
                DataPath,
                $"{node.id}.*"
            );
            return (files.Length > 0 ? files[0] : null);
        }
    }
}
