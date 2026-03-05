using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSZDialougeManager
{
    public class FilesystemManager
    {
        public static string BaseDir => AppDomain.CurrentDomain.BaseDirectory;
        public static string DataPath => Path.Combine(BaseDir, "Data");

        public bool DoesNodeAudioExist(DialogueNodeDTO node)
        {
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
                return false;
            }
            string[] files = Directory.GetFiles(
                DataPath,
                node.id.ToString()
            );
            return (files.Contains(node.id.ToString()));
        }
    }
}
