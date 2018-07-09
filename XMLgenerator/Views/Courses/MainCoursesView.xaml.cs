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
using XMLgenerator.Data.Model;
using XMLgenerator.Connections;
using XMLgenerator.Connections.Database;

namespace XMLgenerator.Views.Courses
{
    /// <summary>
    /// Interaction logic for MainCoursesView.xaml
    /// </summary>
    public partial class MainCoursesView : Page
    {
        public MainCoursesView()
        {
            InitializeComponent();
        }

        XMLgeneratorConnection xmlCon;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtID.Text = GenerateId();
            xmlCon = new XMLgeneratorConnection();
            xmlCon.OpenConnection();
            cmpTeacher.ItemsSource = xmlCon.ReadTeachers();
            lbCourses.ItemsSource = xmlCon.ReadCourse();
        }

        private string GenerateId()
        {
            string actualID = Properties.Settings.Default.CourseId;
            int nextID = Convert.ToInt32(actualID) + 1;
            actualID = nextID.ToString();
            actualID = "c" + actualID.PadLeft(4, '0');
            return actualID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CourseWithName courseWithName = new CourseWithName();

            courseWithName.course.id = txtID.Text;
            courseWithName.course.lectures = txtLectures.Text;
            courseWithName.course.min_days = txtMinDays.Text;
            courseWithName.course.students = txtStudents.Text;
            Teacher t = cmpTeacher.SelectedValue as Teacher;
            courseWithName.course.teacher = t.id;
            if (radYes.IsChecked == true)
            {
                courseWithName.course.double_lectures = "yes";
            }
            else
            {
                courseWithName.course.double_lectures = "no";
            }
            courseWithName.name = txtCourseName.Text;

            string messageResult;

            if (xmlCon.InsertCourse(courseWithName, out messageResult) == true)
            {
                txtLectures.Text = "";
                txtMinDays.Text = "";
                txtStudents.Text = "";
                cmpTeacher.SelectedValue = "";
                txtCourseName.Text = "";
                radYes.IsChecked = false;
                radNo.IsChecked = false;
                Properties.Settings.Default.CourseId = txtID.Text.Remove(0, 1);
                Properties.Settings.Default.Save();
                txtID.Text = GenerateId();
                lbCourses.ItemsSource = null;
                lbCourses.ItemsSource = xmlCon.ReadCourse();
            }


            //  Teacher t = cmpTeacher.SelectedValue as Teacher;
            // MessageBox.Show(t.id);


        }

    }
}
