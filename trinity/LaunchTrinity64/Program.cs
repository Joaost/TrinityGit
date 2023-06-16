using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchTrinity64
{
    class Program
    {

        private struct Version : IComparable<Version>
        {
            int[] version;
            public string Value;
            public Version(string name)
            {
                var parts = name.Split('.');
                version = new int[parts.Length];
                for(var i = 0; i < parts.Length; i++)
                {
                    if (int.TryParse(parts[i], out int val))
                        version[i] = val;
                }
                Value = name;
            }

            public int CompareTo(Version other)
            {
                if (other.version == null)
                    return 1;
                if (version == null)
                    return -1;
                var count = Math.Min(other.version.Length, version.Length);
                for(var i = 0; i < count; i++)
                {
                    var comp = version[i].CompareTo(other.version[i]);
                    if (comp != 0)
                        return comp;
                }
                return 0;
            }
        }
        static void Main(string[] args)
        {
            var dirs = Directory.GetDirectories(".\\", "Trinity64.*", SearchOption.TopDirectoryOnly);
            Version highestVersion = default(Version);
            foreach (var dir in dirs)
            {
                var version = new Version(dir);
                if (highestVersion.CompareTo(version) < 0)
                {
                    highestVersion = version;
                }
            }
            if(File.Exists(Path.Combine(highestVersion.Value, "Trinity.exe")))
                Process.Start(Path.Combine(highestVersion.Value, "Trinity.exe"), "noloader");
        }
    }
}
