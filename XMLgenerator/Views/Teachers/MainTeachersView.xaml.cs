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
using XMLgenerator.Connections.Database;

namespace XMLgenerator.Views.Teachers
{
    /// <summary>
    /// Interaction logic for MainTeachersView.xaml
    /// </summary>
    public partial class MainTeachersView : Page
    {
        public MainTeachersView()
        {
            InitializeComponent();
        }
        XMLgeneratorConnection xmlCon;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtID.Text = GenerateID();
            xmlCon = new XMLgeneratorConnection();
            xmlCon.OpenConnection();
            LoadTeachersInList();
        }

        private string GenerateID()
        {
            string actualID = Properties.Settings.Default.TeacherId; // 000
            int nextID = Convert.ToInt32(actualID) + 1;
            actualID = nextID.ToString();
            actualID = "t" + actualID.PadLeft(3, '0');
            return actualID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Teacher teacher = new Teacher() { id = txtID.Text, name = txtTeacherName.Text };
            // ose menyra tjeter 
            //teacher.id = txtID.Text;
            //teacher.name = txtTeacherName.Text;
            string messageResult;
           
            if (xmlCon.InsertTeacher(teacher, out messageResult)==true)
            {
                txtTeacherName.Text = "";
                Properties.Settings.Default.TeacherId = txtID.Text.Remove(0, 1);
                Properties.Settings.Default.Save();
                txtID.Text = GenerateID();
                LoadTeachersInList();
            }

        }

        private void txtTeacherName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTeacherName.Text.Length > 0)
            {
                btnSave.IsEnabled = true;
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }

        private void LoadTeachersInList()
        {
            List<Teacher> lsTeachers = xmlCon.ReadTeachers();
            listTeachers.ItemsSource = null;
            listTeachers.ItemsSource = lsTeachers;
        }
    }
}
