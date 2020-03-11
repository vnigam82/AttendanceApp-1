﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AttendanceApp.CustomControls
{
    public class CustomPickerNew : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPickerNew), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
