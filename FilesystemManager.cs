using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MSZDialougeManager
{
    public class FilesystemManager
    {
        public static string BaseDir => AppDomain.CurrentDomain.BaseDirectory;
        public static string DataPath => Path.Combine(BaseDir, "Data");
        public static string Templete => Path.Combine(BaseDir, "templeteNodes.json");

        public static readonly string ext = "mszdlg";

        public static void SaveProj(string path, DialogueForest forest)
        {
            string actualExt = System.IO.Path.GetExtension(path).TrimStart('.').ToLower();
            if (actualExt != ext) path = System.IO.Path.ChangeExtension(path, ext);

            string json = JsonConvert.SerializeObject(new DialogueForest { nodes = forest.nodes, gameVersion = forest.gameVersion }, Formatting.Indented);
            File.WriteAllText(Path.Combine(DataPath, "nodes.json"), json);
            ZipFile.CreateFromDirectory(DataPath, path);
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
