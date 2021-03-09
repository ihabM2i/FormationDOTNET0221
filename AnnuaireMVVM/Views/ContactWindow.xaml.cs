using Annuaire.Classes;
using AnnuaireMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnnuaireMVVM.Views
{
    /// <summary>
    /// Logique d'interaction pour ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public ContactWindow()
        {
            InitializeComponent();
        }

        public ContactWindow(Contact contact) : this()
        {
            DataContext = new ContactViewModel(contact);
        }

        public ContactWindow(Contact contact, ContactsWindow mainWindow) : this()
        {
            DataContext = new ContactViewModel(contact, mainWindow);
        }
    }
}
