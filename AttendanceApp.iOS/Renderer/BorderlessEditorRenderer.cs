using System;
using System.ComponentModel;
using AttendanceApp.CustomControls;
using AttendanceApp.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace AttendanceApp.iOS.Renderer
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        public static void Init() { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {


                base.OnElementPropertyChanged(sender, e);

                Control.Layer.BorderWidth = 0;
            }
            catch (Exception)
            {
            }
        }
    }
}
