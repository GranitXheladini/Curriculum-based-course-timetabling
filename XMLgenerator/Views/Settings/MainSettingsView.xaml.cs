using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XMLgenerator.Views.Settings
{
    /// <summary>
    /// Interaction logic for MainSettingsView.xaml
    /// </summary>
    public partial class MainSettingsView : Page
    {
        public MainSettingsView()
        {
            InitializeComponent();
        }
        private void EnableAllButtons()
        {
            btnRoom.IsEnabled = true;
            btnCurricula.IsEnabled = true;
            btnConstraint.IsEnabled = true;
            btnDescriptor.IsEnabled = true;
            btnImportExport.IsEnabled = true;
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            EnableAllButtons();
            appFrame.Navigate(new Rooms.MainRoomsView());
            btnRoom.IsEnabled = false;
        }

        private void btnCurricula_Click(object sender, RoutedEventArgs e)
        {
            EnableAllButtons();
            appFrame.Navigate(new Curricula.MainCurriculaView());
            btnCurricula.IsEnabled = false;
        }

        private void btnConstraint_Click(object sender, RoutedEventArgs e)
        {
            EnableAllButtons();
            appFrame.Navigate(new Constraint.MainConstraintOptionView());
            btnConstraint.IsEnabled = false;
        }

        private void btnDescriptor_Click(object sender, RoutedEventArgs e)
        {
            EnableAllButtons();
            appFrame.Navigate(new DesciptorView());
            btnDescriptor.IsEnabled = false;
        }

        private void btnImportExport_Click(object sender, RoutedEventArgs e)
        {
            EnableAllButtons();
            appFrame.Navigate(new Settings.ImortExport.ImportExportView());
            btnImportExport.IsEnabled = false;
        }
    }
}
