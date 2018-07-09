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

namespace XMLgenerator.Views.Constraint
{
    /// <summary>
    /// Interaction logic for MainConstraintOptionView.xaml
    /// </summary>
    public partial class MainConstraintOptionView : Page
    {
        public MainConstraintOptionView()
        {
            InitializeComponent();
        }

        private void btnPeriod_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Constraint.MainConstraintPeriodView());
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Constraint.MainConstraintRoomView());
        }
    }
}
