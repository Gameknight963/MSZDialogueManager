using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSZDialougeManager
{
    public class Config
    {
        public static string BaseDir => AppDomain.CurrentDomain.BaseDirectory;
        public static string DataPath => Path.Combine(BaseDir, "Data");
    }
}
