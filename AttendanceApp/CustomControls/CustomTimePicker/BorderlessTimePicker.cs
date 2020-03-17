using System;
using Xamarin.Forms;

namespace AttendanceApp.CustomControls
{
    public class BorderlessTimePicker:TimePicker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(BorderlessTimePicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
