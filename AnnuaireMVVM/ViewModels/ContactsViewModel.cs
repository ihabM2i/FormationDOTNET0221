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
        public string Search { get; set; }

        public string Mail { get; set; }

        public ObservableCollection<Email> Emails { get; set; }

        public ICommand ConfirmCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand MailCommand { get; set; }

        public ObservableCollection<Contact> Contacts { get; set; }

        public ContactsViewModel()
        {
            Contact = new Contact();
            ConfirmCommand = new RelayCommand(ActionConfirmCommand);
            DeleteCommand = new RelayCommand(ActionDeleteCommand);
            SearchCommand = new RelayCommand(ActionSearchCommand);
            MailCommand = new RelayCommand(ActionMailCommand);
            Contacts = new ObservableCollection<Contact>(Contact.GetContacts());
            Emails = new ObservableCollection<Email>();
        }

        public void ActionConfirmCommand()
        {
            if(Contact.Id == 0 && Contact.Save())
            {
                Contacts.Add(Contact);
                //Enregistrer les emails
                Contact.Mails.ForEach(m => m.Save(Contact.Id));
                MessageBox.Show("Contact ajouté");
                Contact = new Contact();
                Emails = new ObservableCollection<Email>();
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

        private void ActionSearchCommand()
        {
            Contacts = new ObservableCollection<Contact>(Contact.SearchContacts(Search));
            RaisePropertyChanged("Contacts");
        }

        private void ActionMailCommand()
        {
            if(Mail != null)
            {
                Email e = new Email() { Mail = Mail };
                Contact.Mails.Add(e);
                Emails.Add(e);
                Mail = "";
                RaisePropertyChanged("Mail");
            }
        }

        private void RaiseAllChanged()
        {
            RaisePropertyChanged("Nom");
            RaisePropertyChanged("Prenom");
            RaisePropertyChanged("Telephone");
            RaisePropertyChanged("Emails");
        }
    }
}
