using System;
using System.IO;
using System.Windows.Forms;

namespace LawOfficeApp
{
    static class Program
    {
        public static string DataFolder;
        public static string DbPath;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Data folder in user's Documents
            DataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LawOfficeData");
            if (!Directory.Exists(DataFolder)) Directory.CreateDirectory(DataFolder);

            DbPath = Path.Combine(DataFolder, "LawOfficeData.db");

            // Initialize DB (creates file & tables if needed)
            Database.Initialize(DbPath);

            Application.Run(new MainForm());
        }
    }
}
