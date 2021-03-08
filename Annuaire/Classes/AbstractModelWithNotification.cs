using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Annuaire.Classes
{
    public abstract class AbstractModelWithNotification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected void RaisePropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
