using Annuaire.Classes;
using AnnuaireMVVM.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AnnuaireMVVM.ViewModels
{
    public class ContactViewModel
    {
        private Contact _contact;

        private ContactsWindow _mainWindow;
        public string Nom { get => _contact.Nom; }
        public string Prenom { get => _contact.Prenom; }
        public string Telephone { get => _contact.Telephone; }

        public DateTime MaDate { get; set; }
        public List<Email> Emails { get => Email.GetMails(_contact.Id); }

        public ICommand EditMainWindowCommand { get; set; }
        public ContactViewModel(Contact contact)
        {
            _contact = contact;
            EditMainWindowCommand = new RelayCommand(ActionEditMainWindow);
            MaDate = DateTime.Now;
        }

        public ContactViewModel(Contact contact, ContactsWindow mainWindow) : this(contact)
        {
            _mainWindow = mainWindow;
        }

        private void ActionEditMainWindow()
        {
            if(_mainWindow.DataContext is ContactsViewModel mainViewModel)
            {
                mainViewModel.Message = "Message de la fenetre detail";
                _mainWindow.Close();
            }
        }
    }
}
