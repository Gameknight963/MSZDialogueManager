using Newtonsoft.Json;
using MZDO;
using System.IO.Compression;

namespace MSZDialougeManager
{
    public class FilesystemManager
    {
        public static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string DataPath = Path.Combine(BaseDir, "Data");
        public static readonly string Templete = Path.Combine(BaseDir, "templeteNodes.json");

        /// <summary>
        /// Don't use this path if you don't currently have a nodes.json in Data/
        /// </summary>
        public static readonly string NodesJsonPath = Path.Combine(DataPath, "nodes.json");

        /// <summary>
        /// The custom dialouge extension (without the dot)
        /// </summary>
        public const string ext = "mszdlg";

        public static bool IsFileLoaded { get; private set; }

        public static void SaveProj(string path, DialoguePack pack)
        {
            if (File.Exists(path)) File.Delete(path);
            string actualExt = Path.GetExtension(path).TrimStart('.').ToLower();
            if (actualExt != ext) path = Path.ChangeExtension(path, ext);
            string json = JsonConvert.SerializeObject(pack, Formatting.Indented);
            File.WriteAllText(Path.Combine(DataPath, "nodes.json"), json);
            ZipFile.CreateFromDirectory(DataPath, path);
        }

        public static void SaveJson(string path, DialoguePack pack)
        {
            string json = JsonConvert.SerializeObject(pack, Formatting.Indented);
            if (File.Exists(path)) File.Delete(path);
            File.WriteAllText(path, json);
        }

        public static DialoguePack? LoadProj(string path)
        {
            if (Directory.Exists(DataPath)) Directory.Delete(DataPath, true);
            Directory.CreateDirectory(DataPath);
            ZipFile.ExtractToDirectory(path, DataPath);
            string json = File.ReadAllText(NodesJsonPath);
            DialoguePack? pack = JsonConvert.DeserializeObject<DialoguePack>(json);
            IsFileLoaded = true;
            return pack;
        }

        public static void AddNodeAudio(int treeIndex, int nodeId, string audioPath)
        {
            string[] existingFiles = Directory.GetFiles(DataPath, $"{treeIndex}_{nodeId}.*");
            foreach (string file in existingFiles) File.Delete(file);
            string destination = Path.Combine(DataPath, $"{treeIndex}_{nodeId}{Path.GetExtension(audioPath)}");
            File.Copy(audioPath, destination);
        }

        public static void RemoveNodeAudio(int treeIndex, int nodeId)
        {
            string[] existingFiles = Directory.GetFiles(DataPath, $"{treeIndex}_{nodeId}.*");
            foreach (string file in existingFiles) File.Delete(file);
        }


        public static bool DoesNodeAudioExist(int treeIndex, int nodeId)
        {
            if (!Directory.Exists(DataPath)) return false;
            return Directory.GetFiles(DataPath, $"{treeIndex}_{nodeId}.*").Length > 0;
        }

        public static string? GetNodeAudioPath(int treeIndex, int nodeId)
        {
            if (!Directory.Exists(DataPath)) return null;
            string[] files = Directory.GetFiles(DataPath, $"{treeIndex}_{nodeId}.*");
            return files.Length > 0 ? files[0] : null;
        }
    }
}
