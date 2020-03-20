using System;
using System.IO;
using AttendanceApp.Dependency;
using AttendanceApp.iOS.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace AttendanceApp.iOS.Dependency
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}
