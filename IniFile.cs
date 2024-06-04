using System.Runtime.InteropServices;
using System.Text;

namespace RazorSwap
{
    public class IniFile
    {
        private string _path = string.Empty;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string iniFileName)
        {
            if (!string.IsNullOrEmpty(iniFileName))
            {
                _path = new FileInfo(iniFileName).FullName;
            }
            Console.WriteLine($"File path {_path}");
        }

        public string Read(string key, string section = null)
        {
            var ret = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", ret, 255, _path);
            return ret.ToString();
        }

        public void Write(string key, string value, string section = null)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        public void DeleteKey(string key, string section = null)
        {
            Write(key, null, section);
        }

        public void DeleteSection(string section = null)
        {
            Write(null, null, section);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }

}
