using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RazorSwap
{
    public partial class Swap : Form
    {
        public string RGRoot { get; set; } = "C:\\PositionerRunTimeFiles";
        public string DBName { get; set; } = "RGST";
        public string MainDBPath { get { return $"{RGRoot}\\{DBName}"; } }
        public string IniFileName { get { return $"{MainDBPath}\\Config.ini"; } }

        public string BackDBPath { get { return $"{MainDBPath}.BACK"; } }
        public string RodDBPath { get { return $"{RGRoot}\\ROD"; } }
        public string UnistrutDBPath { get { return $"{RGRoot}\\UNISTRUT"; } }
        public string PipeDBPath { get { return $"{RGRoot}\\PIPE"; } }

        public string EXE { get { return $"C:\\Program Files (x86)\\{DBName}\\RGST.exe"; } }

        public Swap()
        {
            InitializeComponent();

            if (!Directory.Exists(MainDBPath))
            {
                MessageBox.Show("Could not find " + MainDBPath, "Perhaps installed as RGST2?.");
            }

            if (!File.Exists(IniFileName))
            {
                MessageBox.Show("Could not find " + IniFileName, "Razor Config.ini not found.");
                return;
            }
            
            cbRod.Checked = false;
            cbPipe.Checked = false;
            cbUnitstrut.Checked = false;

            if (File.Exists($"{MainDBPath}\\rod.txt"))
            {
                cbRod.Checked = true;
            }
            if (File.Exists($"{MainDBPath}\\pipe.txt"))
            {
                cbPipe.Checked = true;
            }
            if (File.Exists($"{MainDBPath}\\unistrut.txt"))
            {
                cbUnitstrut.Checked = true;
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

        static bool CopyDirectory(string sourceDir, string destinationDir, string fnameToMake = "", List<string>? exclude = null, bool recursive = false)
        {
            Directory.CreateDirectory(destinationDir);

            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
            {
                return false;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            try
            {
                Directory.CreateDirectory(destinationDir);
                if (!string.IsNullOrEmpty(fnameToMake))
                {
                    if (!File.Exists(destinationDir + "\\" + fnameToMake))
                    {
                        File.CreateText(destinationDir + "\\" + fnameToMake).Close();
                        if (!File.Exists(destinationDir + "\\" + fnameToMake))
                        {
                            MessageBox.Show("Could not create " + destinationDir + "\\" + fnameToMake, "Error creating file");
                            return false;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Could not create " + destinationDir, "Error creating directory");
                return false;
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                var lower = file.Name.ToLower();
                if (exclude != null && exclude.Contains(lower))
                {
                    continue;
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
                    CopyDirectory(subDir.FullName, newDestinationDir, "", exclude, true);
                }
            }

            return true;
        }

        /// <summary>
        /// Create the different PIPE, ROD and UNISTRUT directories
        /// Based on the check boxes selected, move the RGST directory back to PIPE, ROD or UNISTRUT.
        /// If we don't move the RGST directory, we will rename it to RGST.OLD
        /// If we can't rename the RGST directory, we will return false
        /// Return false on any critical error.
        /// </summary>
        private bool ResetRGST()
        {
            if (!Directory.Exists(PipeDBPath))
            {
                if (!CopyDirectory(MainDBPath, PipeDBPath, $"pipe.txt"))    // Create ROD DB
                {
                    return false;
                }
            }

            if (!Directory.Exists(RodDBPath))
            {
                if (!CopyDirectory(MainDBPath, RodDBPath, $"rod.txt"))    // Create ROD DB
                {
                    return false;
                }
            }

            if (!Directory.Exists(UnistrutDBPath))
            {
                if (!CopyDirectory(MainDBPath, UnistrutDBPath, $"unistrut.txt"))    // Create Unistrut DB
                {
                    return false;
                }
            }

            if (File.Exists($"{MainDBPath}\\unistrut.txt") && !Directory.Exists(UnistrutDBPath))
            {
                if (cbUnitstrut.Checked == false)
                {
                    try
                    {
                        Directory.Move(MainDBPath, UnistrutDBPath);
                        return true;
                    }
                    catch
                    {
                        MessageBox.Show("Close all programs and try again.", "Error moving RGST back to UNISTRUT");
                        return false;
                    }
                }
            }

            if (File.Exists($"{MainDBPath}\\pipe.txt") && !Directory.Exists(PipeDBPath))
            {
                if (cbPipe.Checked == false)
                {
                    try
                    {
                        Directory.Move(MainDBPath, PipeDBPath);
                        return true;
                    }
                    catch
                    {
                        MessageBox.Show("Close all programs and try again.", "Error moving RGST back to PIPE");
                        return false;
                    }
                }
            }

            if (File.Exists($"{MainDBPath}\\rod.txt") && !Directory.Exists(RodDBPath))
            {
                if (cbRod.Checked == false)
                {
                    try
                    {
                        Directory.Move(MainDBPath, RodDBPath);
                        return true;
                    }
                    catch
                    {
                        MessageBox.Show("Close all programs and try again.", "Error moving RGST back to ROD");
                        return false;
                    }
                }
            }

            if (Directory.Exists(MainDBPath))
            {
                try
                {
                    if (Directory.Exists(BackDBPath))
                    {
                        Random r = new Random();
                        int n = r.Next();
                        Directory.Move(BackDBPath, $"{BackDBPath}{n}");
                    }
                    Directory.Move(MainDBPath, BackDBPath);
                }
                catch
                {
                }
            }

            if (Directory.Exists(MainDBPath))
            {
                MessageBox.Show("Could not rename " + MainDBPath, "Error renaming RGST to RGST.OLD");
                return false;
            }

            return true;
        }

        private void OnApplyClick(object sender, EventArgs e)
        {           

            if (!cbRod.Checked && !cbPipe.Checked && !cbUnitstrut.Checked)
            {
                MessageBox.Show("Please select at least one option.", "No database selected");
                return;
            }

            var (success, rgEXE) = KillRGST();
            if (!success)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            if (!ResetRGST())
            {
                Cursor.Current = Cursors.Default;
                return;
            }

            try
            {
                if (cbPipe.Checked)
                {
                    if (File.Exists($"{MainDBPath}\\pipe.txt"))
                    {
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    Directory.Move(PipeDBPath, MainDBPath);
                    Wait();
                }
            }
            catch
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Close all programs and try again. Could not rename " + PipeDBPath, "Error renaming PIPE to RGST");
                return;
            }

            try
            {
                if (cbRod.Checked)
                {
                    if (File.Exists($"{MainDBPath}\\rod.txt"))
                    {
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    Directory.Move(RodDBPath, MainDBPath);
                    Wait();
                }
            }
            catch
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Close all programs and try again. Could not rename " + RodDBPath, "Error renaming ROD to RGST");
                return;
            }

            try
            {
                if (cbUnitstrut.Checked)
                {
                    if (File.Exists($"{MainDBPath}\\unistrut.txt"))
                    {
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    Directory.Move(UnistrutDBPath, MainDBPath);
                    Wait();
                }
            }
            catch
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Close all programs and try again. Could not rename " + UnistrutDBPath, "Error renaming UNISTRUIT to RGST");
                return;
            }

            StartRGST(rgEXE);
            this.Close();
        }

        bool _callback = false;
        private void cbPipe_CheckedChanged(object sender, EventArgs e)
        {
            if (_callback)
            {
                return;
            }
            if (cbPipe.Checked)
            {
                _callback = true;
                cbRod.Checked = false;
                cbUnitstrut.Checked = false;
                _callback = false;
            }
        }

        private void cbRod_CheckedChanged(object sender, EventArgs e)
        {
            if (_callback)
            {
                return;
            }
            if (cbRod.Checked)
            {
                _callback = true;
                cbPipe.Checked = false;
                cbUnitstrut.Checked = false;
                _callback = false;
            }
        }

        private void cbUnitstrut_CheckedChanged(object sender, EventArgs e)
        {
            if (_callback)
            {
                return;
            }
            if (cbUnitstrut.Checked)
            {
                _callback = true;
                cbRod.Checked = false;
                cbPipe.Checked = false;
                _callback = false;
            }
        }
    }
}