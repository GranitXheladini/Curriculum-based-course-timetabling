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
using XMLgenerator.Connections.Database;
using XMLgenerator.Data.Model;
using XMLgenerator.Data.Model.ImportExport;
using XMLgenerator.Engine.File;

namespace XMLgenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnableMenu()
        {
            btnCourses.IsEnabled = true;
            btnXMLgenerator.IsEnabled = true;
            btnHome.IsEnabled = true;
            btnSettings.IsEnabled = true;
            btnTeachers.IsEnabled = true;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            EnableMenu();
            btnHome.IsEnabled = false;

            Views.Home.MainHomeView mainHomeView = new Views.Home.MainHomeView(btnHome);
            frameApp.Navigate(mainHomeView);
        }

        private void btnTeachers_Click(object sender, RoutedEventArgs e)
        {
            EnableMenu();
            btnTeachers.IsEnabled = false;

            Views.Teachers.MainTeachersView mainTeachersView = new Views.Teachers.MainTeachersView();
            frameApp.Navigate(mainTeachersView);
        }

        private void btnCourses_Click(object sender, RoutedEventArgs e)
        {
            EnableMenu();
            btnCourses.IsEnabled = false;

            Views.Courses.MainCoursesView mainCoursesView = new Views.Courses.MainCoursesView();
            frameApp.Navigate(mainCoursesView);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            EnableMenu();
            btnSettings.IsEnabled = false;
            frameApp.Navigate(new Views.Settings.MainSettingsView());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnHome_Click(null, null);
       
        }

        private void btnXMLgenerator_Click(object sender, RoutedEventArgs e)
        {
            EnableMenu();
            btnXMLgenerator.IsEnabled = false;
            frameApp.Navigate(new Views.XMLgenerate.MainXMLgenerateView());
        }

    }
}
