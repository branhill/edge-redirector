using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdgeRedirector.Gui.Helpers
{
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
                return;

            storage = value;
            OnPropertyChanged(propertyName);
        }

        protected void Set<T>(T storage, T value, Action assignment, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
                return;

            assignment();
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
