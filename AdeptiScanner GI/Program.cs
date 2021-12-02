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
            Directory.CreateDirectory(Database.appDir);
            Directory.CreateDirectory(Database.appDir + @"\tessdata");
            Directory.CreateDirectory(Database.appDir + @"\images");
            Database.GenerateFilters();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ScannerForm());
        }


    }
}
