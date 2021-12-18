using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinanceManager.Core
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (_disposed || !Disposing)
            {
                return;
            }
            _disposed = true;
            // Disposing Logic
        }

        protected virtual void OnPropertyChange([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChange(name);
            return true;
        }

        //~ViewModelBase()
        //{
        //    Dispose(false);
        //}
    }
}
