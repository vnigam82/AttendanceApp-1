using System;
using Android.Content;
using AttendanceApp.CustomControls;
using AttendanceApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace AttendanceApp.Droid.Renderer
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        public static void Init() { }
        public BorderlessEditorRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                try
                {
                    Control.Background = null;
                    var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                    layoutParams.SetMargins(0, 0, 0, 0);
                    LayoutParameters = layoutParams;
                    Control.LayoutParameters = layoutParams;
                    Control.SetPadding(0, 0, 0, 0);
                    SetPadding(0, 0, 0, 0);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
