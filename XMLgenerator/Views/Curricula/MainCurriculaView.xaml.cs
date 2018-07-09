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

namespace XMLgenerator.Views.Curricula
{
    /// <summary>
    /// Interaction logic for MainCurriculaView.xaml
    /// </summary>
    public partial class MainCurriculaView : Page
    {
        public MainCurriculaView()
        {
            InitializeComponent();
        }
        XMLgeneratorConnection xmlCon;
        List<Curriculum> listCuriculum;

        private string GenerateId()
        {
            string actualID = Properties.Settings.Default.CurriculaId;
            int nextID = Convert.ToInt32(actualID) + 1;
            actualID = nextID.ToString();
            actualID = "q" + actualID.PadLeft(3, '0');
            return actualID;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtID.Text = GenerateId();
            courseId = 0;
            xmlCon = new XMLgeneratorConnection();
            xmlCon.OpenConnection();
            listCuriculum = new List<Curriculum>();


            listCuriculum.Add(new Curriculum() { id = txtID.Text });

        }
        int courseId;
        List<bool> isCourseAdded = new List<bool>();
        private void btnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = true;
            Label l = new Label() { Content = "Course " + Convert.ToInt32(courseId + 1), FontSize = 12, FontWeight = FontWeights.Thin, Margin = new Thickness(10, 10, 10, 0) };
            panelCourses.Children.Add(l);
            ComboBox comTemp = new ComboBox() { Tag = courseId, Margin = new Thickness(10, 0, 10, 10) };
            //   comTemp.DropDownClosed = new EventHandler(ComTemp_DropDownClosed); menyra 1
            comTemp.DropDownClosed += ComTemp_DropDownClosed;                      // menyra 2
            comTemp.ItemsSource = xmlCon.ReadCourse(xmlCon.ReadCourse());
            panelCourses.Children.Add(comTemp);
            listCuriculum[0].course.Add(new Course1());
            isCourseAdded.Add(false);
            courseId++;
            // btnAddCourse.IsEnabled = false;
        }
        private ComboBox ValidateCombo(string iZgjedhuri)
        {
            ComboBox comTemp = new ComboBox() { Tag = courseId, Margin = new Thickness(10, 0, 10, 10) };
            comTemp.ItemsSource = xmlCon.ReadCourse(xmlCon.ReadCourse());
            comTemp.Items.Remove(iZgjedhuri);
            return comTemp;
        }


        private void ComTemp_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box.SelectedIndex != -1)
            {
                isCourseAdded[Convert.ToInt32(box.Tag)] = true;
                listCuriculum[0].course[Convert.ToInt32(box.Tag)].@ref = box.SelectedItem.ToString();

            }
        }
        public List<bool> ValdimiNeLista()
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < courseId; i++)
            {
                for (int k = i+1; k < courseId; k++)
                {
                    if (listCuriculum[0].course[i].@ref == listCuriculum[0].course[k].@ref)
                    {
                        list.Add(false);
                    }
                    else
                    {
                        list.Add(true);
                    }
                }
            }

            return list;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string messg;
            if (ValidateDataInComboBoxs() == true && ValidateDataInComboBoxs2() == true)
            {
                Data.Model.Curricula curricula = new Data.Model.Curricula();
                curricula.curriculum = listCuriculum;
                if (xmlCon.InsertCurricula(curricula, out messg))
                {
                    Properties.Settings.Default.CurriculaId = txtID.Text.Remove(0, 1);
                    Properties.Settings.Default.Save();
                    this.NavigationService.Navigate(new Views.Curricula.MainCurriculaView());
                }
            }
            else
            {
                MessageBox.Show("Plotesoni te gjitha kurset", "Fatal Error");
            }
        }
        private bool ValidateDataInComboBoxs()
        {
            bool rez = true;

            foreach (var item in isCourseAdded)
            {
                if (item == false)
                {
                    rez = false;
                }
            }

            return rez;
        }
        private bool ValidateDataInComboBoxs2()
        {
            bool rez = true;

            foreach (var item in ValdimiNeLista())
            {
                if (item == false)
                {
                    rez = false;
                }
            }

            return rez;
        }
    }
}
