using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            field = value;
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event EventHandler IsBusyChanged;
        public event EventHandler IsValidChanged;

        readonly List<string> errors = new List<string>();


        public BaseViewModel()
        {

        }
        public bool IsValid
        {
            get { return errors.Count == 0; }
        }
        protected List<string> Errors
        {
            get { return errors; }
        }
        public virtual string Error
        {
            get
            {
                return errors.Aggregate(new StringBuilder(), (b, s) => b.AppendLine(s)).ToString().Trim();
            }
        }
        bool isBusy = true;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;

                    OnPropertyChanged("IsBusy");
                    OnPropertyChanged("IsAvailable");
                }
            }
        }
        public bool IsAvailable
        {
            get { return !IsBusy; }

        }
        public void DisplayAlert(string title, string message)
        {
            string[] values = { title, message };
            MessagingCenter.Send<BaseViewModel, string[]>(this, "DisplayAlert", values);
        }
        protected virtual void OnIsBusyChanged()
        {
            var ev = IsBusyChanged;
            if (ev != null)
            {
                ev(this, EventArgs.Empty);
            }
        }
        protected void SetProperty<U>(
            ref U backingStore, U value,
            string propertyName,
            Action onChanged = null,
            Action<U> onChanging = null)
        {
            if (EqualityComparer<U>.Default.Equals(backingStore, value))
                return;

            if (onChanging != null)
                onChanging(value);



            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }
        public virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
