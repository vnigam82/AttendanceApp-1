using System;
namespace AttendanceApp.Dependency
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
