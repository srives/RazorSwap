using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RazorSwap
{
    public partial class Swap : Form
    {
        public string RGRoot { get; set; } = "C:\\PositionerRunTimeFiles";
        public string DBName { get; set; } = "RGST";
        public string MainDBPath { get { return $"{RGRoot}\\{DBName}"; } }
        public string TempDBPath { get { return $"{MainDBPath}.bak"; } }
        public string IniFileName { get { return $"{MainDBPath}\\Config.ini"; } }
        public string SwapDBPath { get { return $"{RGRoot}\\SWAP"; } }
        public string EXE { get { return $"C:\\Program Files (x86)\\{DBName}\\RGST.exe"; } }

        public Swap()
        {
            InitializeComponent();
            if (!File.Exists(IniFileName))
            {
                MessageBox.Show("Could not find " + IniFileName, "Razor Config.ini not found.");
                return;
            }
        }

        private (bool, string) KillRGST()
        {
            var success = true;
            string fname = string.Empty;
            try
            {
                foreach (Process process in Process.GetProcessesByName("RGST"))
                {
                    if (process != null)
                    {
                        fname = process?.MainModule?.FileName ?? string.Empty;
                        if (process != null)
                        {
                            process?.Kill();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Could not kill RGST. Close it first, then try again.", "Error killing RGST");
                success = false;
            }
            return (success, fname);
        }

        private void Wait()
        {
            Thread.Yield();
            Thread.Sleep(2000);
            Thread.Yield();
        }
        private void StartRGST(string exeName)
        {
            Wait();
            // I found we stop and start RGST too fast, so give it a couple seconds to stop before restarting
            if (exeName == string.Empty)
            {
                exeName = EXE;
            }
            var startingDir = Path.GetDirectoryName(exeName);
            ProcessStartInfo startInfo = new(exeName);
            startInfo.WorkingDirectory = startingDir;
            Process.Start(startInfo);
        }

        static bool CopyDirectory(string sourceDir, string destinationDir, List<string>? exclude = null, bool recursive=false)
        {
            Directory.CreateDirectory(destinationDir);

            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
            {
                return false;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destinationDir);
            foreach (FileInfo file in dir.GetFiles())
            {
                var lower = file.Name.ToLower();
                if (exclude != null && exclude.Contains(lower))
                {
                    continue;
                }

                if (!lower.EndsWith("mdb"))
                {
                    continue; // only backup/restore MDB files
                }
                
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true); // overwrite mode
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, exclude, true);
                }
            }

            return true;
        }

        private void FixDBNameErrors()
        {
            if (!Directory.Exists(MainDBPath))
            {
                if (Directory.Exists(TempDBPath))
                {
                    try
                    {
                        Directory.Move(TempDBPath, MainDBPath);
                    }
                    catch
                    {
                        
                        MessageBox.Show("Close all programs and use file explorer to rename " + TempDBPath + " to " + MainDBPath, "Error renaming RGST.bak to RGST");
                    }
                }
            }

            if (Directory.Exists(TempDBPath) && !Directory.Exists(SwapDBPath))
            {
                try
                {
                    Directory.Move(TempDBPath, SwapDBPath);
                }
                catch
                {
                    MessageBox.Show("Close all programs and use file explorer to rename " + TempDBPath + " to " + SwapDBPath, "Error renaming RGST.bak to SWAP");
                }
            }
        }

        private void OnApplyClick(object sender, EventArgs e)
        {
            if (!File.Exists(IniFileName)) return;
            var (success, rgEXE) = KillRGST();
            if (!success)
            {
                return;
            }

            FixDBNameErrors();

            if (!Directory.Exists(SwapDBPath))
            {
                CopyDirectory(MainDBPath, SwapDBPath);    // copy RazorGage DB to swap dir
            }

            Cursor.Current = Cursors.WaitCursor;

            // Rename RGST to RGST.bak
            try
            {
                Directory.Move(MainDBPath, TempDBPath);
                Wait();
            }
            catch
            {
                MessageBox.Show("Close all programs and try again. Could not rename " + MainDBPath, "Error renaming RGST to RGST.bak");
                return;
            }

            // Rename SWAP to RGST
            bool error = false;
            try
            {
                Directory.Move(SwapDBPath, MainDBPath);
            }
            catch
            {
                error = true;
            }

            if (error)
            {
                try
                {
                    Directory.Move(TempDBPath, MainDBPath);
                }
                catch
                {
                }
                MessageBox.Show("Close all programs and try again. Could not rename " + SwapDBPath, "Error renaming SWAP to RGST");
                return;
            }

            // Rename RGST.bak to SWAP
            try
            {
                Directory.Move(TempDBPath, SwapDBPath);
            }
            catch
            {
                MessageBox.Show("Close all programs and try again. Could not rename " + TempDBPath, "Error renaming RGST.bak to SWAP");
                return;
            }

            StartRGST(rgEXE);
            this.Close();
        }
    }
}