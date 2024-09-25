using System;
using System.IO;
using System.Windows.Forms;

namespace AdeptiScanner_GI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                Directory.CreateDirectory(Database.appDir);
                Directory.CreateDirectory(Path.Join(Database.appDir, "tessdata"));
                Directory.CreateDirectory(Path.Join(Database.appDir, "images"));
                Directory.CreateDirectory(Path.Join(Database.appDir, "Scan_Results"));
                Database.GenerateFilters();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ScannerForm());
            }
            catch (Exception e)
            {
                MessageBox.Show("AdeptiScanner has crashed, check instructions and/or ask developer for help" + Environment.NewLine + Environment.NewLine +
                   "Exact error:" + Environment.NewLine + e.ToString(),
                   "AdeptiScanner crashed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }


    }
}
