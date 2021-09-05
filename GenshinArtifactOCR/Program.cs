using System;
using System.IO;
using System.Windows.Forms;

namespace GenshinArtifactOCR
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
            Directory.CreateDirectory(Database.appDir + @"\filterdata");
            Database.GenerateFilters();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GenshinArtifactOCR());
        }


    }
}
