using System;
using Xamarin.Forms;

namespace AttendanceApp.CustomControls
{
    public class BorderlessDatePicker:DatePicker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(BorderlessDatePicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
