using System;
using System.IO;
using AttendanceApp.Dependency;
using AttendanceApp.Droid.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace AttendanceApp.Droid.Dependency
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
