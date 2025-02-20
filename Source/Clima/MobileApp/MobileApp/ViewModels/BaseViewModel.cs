﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set { isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        bool isClimaPro;
        public bool IsClimaPro 
        {
            get => isClimaPro;
            set { isClimaPro = value; OnPropertyChanged(nameof(IsClimaPro)); }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
