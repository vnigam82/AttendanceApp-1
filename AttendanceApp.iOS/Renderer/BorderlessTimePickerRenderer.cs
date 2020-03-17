﻿using System;
using System.ComponentModel;
using AttendanceApp.CustomControls;
using AttendanceApp.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessTimePicker), typeof(BorderlessTimePickerRenderer))]
namespace AttendanceApp.iOS.Renderer
{
    public class BorderlessTimePickerRenderer : TimePickerRenderer
    {
        public static void Init() { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {


                base.OnElementPropertyChanged(sender, e);

                var element = (BorderlessTimePicker)this.Element;

                if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                {
                    var downarrow = UIImage.FromBundle(element.Image);
                    Control.Layer.BorderWidth = 0;
                    Control.BorderStyle = UITextBorderStyle.None;

                    Control.RightViewMode = UITextFieldViewMode.Always;
                    Control.BackgroundColor = UIColor.White;
                    Control.BorderStyle = UITextBorderStyle.None;

                    Control.RightView = new UIImageView(downarrow);

                }

                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
