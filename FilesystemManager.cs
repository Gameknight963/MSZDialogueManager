using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MSZDialougeManager
{
    public class FilesystemManager
    {
        public static string BaseDir => AppDomain.CurrentDomain.BaseDirectory;
        public static string DataPath => Path.Combine(BaseDir, "Data");
        public static string Templete => Path.Combine(BaseDir, "templeteNodes.json");

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
