using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AttendanceApp.CustomControls;
using AttendanceApp.iOS.Renderer;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPickerNew), typeof(CustomPickerRenderer))]
namespace AttendanceApp.iOS.Renderer
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = (CustomPickerNew)this.Element;

            if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                var downarrow = UIImage.FromBundle(element.Image);
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.BackgroundColor = UIColor.White;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.RightView = new UIImageView(downarrow);
            }
        }
    }
}