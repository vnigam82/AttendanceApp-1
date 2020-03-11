using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApp.Helpers
{
    public static class ClickHelper
    {
        static bool clicked = false;
        public static bool IsConcurrentClick(int ms = 500)
        {
            if (clicked)
                return true;
            clicked = true;
            System.Threading.Tasks.Task.Delay(ms).ContinueWith(x => clicked = false);
            return false;
        }
    }
}
