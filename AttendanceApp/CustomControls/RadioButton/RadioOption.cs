using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AttendanceApp.CustomControls.RadioButton
{
    public class RadioOption : INotifyPropertyChanged
    {

        public string Title { get; set; }
        public string ImageName { get; set; }
        public int HappynessCode { get; set; }
        public int id { get; set; }
        private bool _isSelected { get; set; }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value != _isSelected)
                {
                    this._isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //public RadioOption(int id,string title, bool isSelected = false)
        //{
        //    this.id = id;
        //    this.Title = title;
        //    this.IsSelected = isSelected;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
