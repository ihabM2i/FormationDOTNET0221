using SuiteCoursWPF.ViewModels;
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

namespace SuiteCoursWPF.Views
{
    /// <summary>
    /// Logique d'interaction pour PersonneWindow.xaml
    /// </summary>
    public partial class PersonneWindow : Window
    {
        public PersonneWindow()
        {
            InitializeComponent();
            PersonneViewModel viewModel = new PersonneViewModel();
            DataContext = viewModel;
        }
    }
}
