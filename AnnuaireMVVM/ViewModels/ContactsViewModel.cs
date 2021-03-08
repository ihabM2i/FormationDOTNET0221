using Annuaire.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AnnuaireMVVM.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {
        private Contact contact;

        public Contact Contact { get => contact; set { contact = value; if(value != null) RaiseAllChanged(); } }

        public string Nom { get => Contact.Nom; set { Contact.Nom = value; RaisePropertyChanged(); } }
        public string Prenom { get => Contact.Prenom; set { Contact.Prenom = value; RaisePropertyChanged();} }
        public string Telephone { get => Contact.Telephone; set { Contact.Telephone = value; RaisePropertyChanged(); } }

        public ICommand ConfirmCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ObservableCollection<Contact> Contacts { get; set; }

        public ContactsViewModel()
        {
            Contact = new Contact();
            ConfirmCommand = new RelayCommand(ActionConfirmCommand);
            DeleteCommand = new RelayCommand(ActionDeleteCommand);
            Contacts = new ObservableCollection<Contact>(Contact.GetContacts());
        }

        public void ActionConfirmCommand()
        {
            if(Contact.Id == 0 && Contact.Save())
            {
                Contacts.Add(Contact);
                MessageBox.Show("Contact ajouté");
                Contact = new Contact();
                RaiseAllChanged();
            }
            else if(contact.Id > 0 && Contact.Update())
            {
                MessageBox.Show("Mise à jour du contact effectuée");
                Contact = new Contact();
                RaiseAllChanged();
            }
            else
            {
                MessageBox.Show("Erreur");
            }
        }

        public void ActionDeleteCommand()
        {
            if(Contact.Id > 0)
            {
                if (Contact.Delete())
                {
                    Contacts.Remove(Contact);
                    Contact = new Contact();
                }
            }
        }


        private void RaiseAllChanged()
        {
            RaisePropertyChanged("Nom");
            RaisePropertyChanged("Prenom");
            RaisePropertyChanged("Telephone");
        }
    }
}
