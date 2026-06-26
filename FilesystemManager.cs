using MSZDialougeManager.Properties;
using MZDO.Shared;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Diagnostics.CodeAnalysis;

namespace MSZDialougeManager
{
    public static class FilesystemManager
    {
        public static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string DataPath = Path.Combine(BaseDir, "Data");
        public static readonly string SpeakPath = Path.Combine(DataPath, "Speak");
        public static readonly string Template = Path.Combine(BaseDir, "templateNodes.json");

        /// <summary>
        /// Don't use this path if you don't currently have a nodes.json in Data/
        /// </summary>
        public static readonly string NodesJsonPath = Path.Combine(DataPath, "nodes.json");

        /// <summary>
        /// The custom dialouge extension (without the dot)
        /// </summary>
        public const string ext = "mszdlg";

        public static bool IsFileLoaded { get; private set; } = false;

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
            Directory.CreateDirectory(SpeakPath);
            ZipFile.ExtractToDirectory(path, DataPath);
            string json = File.ReadAllText(NodesJsonPath);
            DialoguePack? pack = JsonConvert.DeserializeObject<DialoguePack>(json);
            if (pack == null) return null;
            if (pack.PackFormat > PacksInfo.PacksFormatVersion)
            {
                if (MessageBox.Show($"This pack (v{pack.PackFormat}) has a greater version than that of the editor (v{PacksInfo.PacksFormatVersion}).\n" +
                    $"Load it anyway?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return null;
            }
            if (pack.PackFormat == 2)
            {
                if (MessageBox.Show("Migrate v2 pack to v3?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return null;
                pack.PackFormat = PacksInfo.PacksFormatVersion;
                CreateDefaultChirps();
            }
            IsFileLoaded = true;
            return pack;
        }

        public static DialoguePack CreateTemplete()
        {
            if (Directory.Exists(DataPath)) Directory.Delete(DataPath, true);
            CreateDefaultChirps(); // createdefaultchirps creates SpeakPath
            DialoguePack pack = JsonConvert.DeserializeObject<DialoguePack>(File.ReadAllText(Template))!;
            pack.PackFormat = PacksInfo.PacksFormatVersion;
            IsFileLoaded = true;
            return pack;
        }

        static void CreateDefaultChirps()
        {
            Directory.CreateDirectory(SpeakPath);
            UnmanagedMemoryStream stream = Resources.MitaSpeak;
            using FileStream fileStream = new(Path.Combine(SpeakPath, "Kind.wav"), FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            UnmanagedMemoryStream stream2 = Resources.KiriSpeak;
            using FileStream fileStream2 = new(Path.Combine(SpeakPath, "Kiri.wav"), FileMode.Create, FileAccess.Write);
            stream2.CopyTo(fileStream2);
        }

        public static bool DoesSpeakerChirpExist(string speakerName)
        {
            return Directory.GetFiles(SpeakPath, $"{speakerName}.*").Length > 0;
        }

        public static string? GetSpeakerChirp(string speakerName)
        {
            string[] files = Directory.GetFiles(SpeakPath, $"{speakerName}.*");
            if (files.Length == 0) return null;
            return files[0];
        }

        public static void SetSpeakerChirp(string speakerName, string sourceFilePath)
        {
            DeleteSpeakerChirp(speakerName);
            string destination = Path.Combine(SpeakPath, $"{speakerName}{Path.GetExtension(sourceFilePath)}");
            File.Copy(sourceFilePath, destination, true);
        }

        public static bool DeleteSpeakerChirp(string speakerName)
        {
            string[] existingFiles = Directory.GetFiles(SpeakPath, $"{speakerName}.*");
            if (existingFiles.Length == 0) return false;
            foreach (string file in existingFiles) File.Delete(file);
            return true;
        }

        public static bool TryGetSpeakerChirp(string speakerName, [NotNullWhen(true)] out string? path)
        {
            string[] files = Directory.GetFiles(SpeakPath, $"{speakerName}.*");
            if (files.Length == 0)
            {
                path = null;
                return false;
            }
            path = files[0];
            return true;
        }

        public static void AddNodeAudio(int treeIndex, int nodeId, string audioPath)
        {
            RemoveNodeAudio(treeIndex, nodeId);
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

        public static bool TryGetNodeAudioPath(int treeIndex, int nodeId, [NotNullWhen(true)] out string? path)
        {
            path = null;
            if (!Directory.Exists(DataPath)) return false;
            string[] files = Directory.GetFiles(DataPath, $"{treeIndex}_{nodeId}.*");
            path = files.Length > 0 ? files[0] : null;
            return files.Length > 0;
        }
    }
}
