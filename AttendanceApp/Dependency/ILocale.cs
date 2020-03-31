using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApp.Dependency
{
    public interface ILocale
    {
        string GetCurrent();

        void SetLocale();
    }
}
