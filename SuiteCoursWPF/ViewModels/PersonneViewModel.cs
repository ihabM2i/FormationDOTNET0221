using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SuiteCoursWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SuiteCoursWPF.ViewModels
{
    public class PersonneViewModel : ViewModelBase //  implementation interface IPropertyChanged par ViewModelBase de MVVMLightLibs
    {
        //private string nom;
        //private string prenom;

        private Personne personne;

        //public event PropertyChangedEventHandler PropertyChanged;

        public string Nom
        {
            get => personne.Nom;
            set
            {
                personne.Nom = value;
                //RaisePropertyChange("Personne");
                RaisePropertyChanged();
                RaisePropertyChanged("Personne");
            }
        }
        public string Prenom
        {
            get => personne.Prenom;
            set
            {
                personne.Prenom = value;
                //RaisePropertyChange("Personne");
                RaisePropertyChanged();
                RaisePropertyChanged("Personne");
            }
        }

        public Personne Personne { get => personne; }

        private ObservableCollection<Personne> personnes;

        public ICommand ValidCommand { get; set; }
        public ObservableCollection<Personne> Personnes { get => personnes; set => personnes = value; }

        public PersonneViewModel()
        {
            personne = new Personne();
            Personnes = new ObservableCollection<Personne>() { new Personne { Nom = "tata", Prenom ="toto"} };
            ValidCommand = new RelayCommand(ActionClickValidButton);
        }

        //private void RaisePropertyChange(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}


        private void ActionClickValidButton()
        {
            Personnes.Add(personne);
            personne = new Personne();
            RaiseAllChanged();
            MessageBox.Show("Validation");
        }

        private void RaiseAllChanged()
        {
            RaisePropertyChanged("Personne");
            RaisePropertyChanged("Nom");
            RaisePropertyChanged("Prenom");
        }
    }
}
