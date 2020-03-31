using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AttendanceApp.CustomViews
{
    public partial class ConfigurationView : ContentView
    {
        public uint AnimationDuration { get; set; }
        public ConfigurationView()
        {
            InitializeComponent();
            IsVisible = false;
            AnimationDuration = 150;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                if (toggleediturl.Checked)
                {
                    txtUrl.IsEnabled = true;
                    txtUrl.Text = txtUrl.Placeholder;
                    txtUrl.FloatingPlaceholderEnabled = true;
                    txtUrl.Placeholder = Resx.AppResources.enterUrl;
                }
                else
                {
                    txtUrl.IsEnabled = false;

                }
            };
            toggleediturl.GestureRecognizers.Add(tapGestureRecognizer);

        }
        #region IsOpen
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create("IsOpen", typeof(bool), typeof(ConfigurationView), false, propertyChanged: IsOpenChanged);
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void IsOpenChanged(BindableObject bindable, object oldValue, object newValue)
        {
            bool isOpen;

            if (bindable != null && newValue != null)
            {
                var control = (ConfigurationView)bindable;
                isOpen = (bool)newValue;

                if (control.IsOpen == false)
                {
                    control.Close();
                }
                else
                {
                    control.Open();
                }
            }
        }

#endregion

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Close();
        }

        public async void Close()
        {
            await this.TranslateTo(0, 50, AnimationDuration);
            IsOpen = IsVisible = false;
        }

        public async void Open()
        {
            IsVisible = true;
            await this.TranslateTo(0, 0, AnimationDuration);
        }
        
        
    }
}
