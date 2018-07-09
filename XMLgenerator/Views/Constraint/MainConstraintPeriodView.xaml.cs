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

namespace XMLgenerator.Views.Constraint
{
    /// <summary>
    /// Interaction logic for MainConstraintPeriodView.xaml
    /// </summary>
    public partial class MainConstraintPeriodView : Page
    {
        public MainConstraintPeriodView()
        {
            InitializeComponent();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Constraint.MainConstraintOptionView());
        }
        Constraints constraints = new Constraints();
        instance instance = new instance();
        private void cbCourse_DropDownClosed(object sender, EventArgs e)
        {
            if (cbCourse.SelectedIndex != -1)
            {
                btnAddDay.IsEnabled = true;
                constraints.constraint[0].course = cbCourse.SelectedItem.ToString();
            }
            else
            {
                btnAddDay.IsEnabled = false;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateComboBox() == true)
            {
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        if (listOfDays[i][j] == true)
                        {
                            listOfTimeslots.Add(new Timeslot() { day = listDays[i], period = j.ToString() });
                        }
                    }
                }
                string msg;
                constraints.constraint[0].timeslot = listOfTimeslots;
                if (xmlCon.InsertConstraints(constraints, out msg))
                {
                    this.NavigationService.Navigate(new Views.Constraint.MainConstraintOptionView());
                }
            }
            else
            {
                MessageBox.Show("Plotesoj te gjitha ditet");
            }
        }
        XMLgeneratorConnection xmlCon;
        List<Timeslot> listOfTimeslots = new List<Timeslot>();
        List<string> listDays = new List<string>();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            xmlCon = new XMLgeneratorConnection();
            xmlCon.OpenConnection();

            cbCourse.ItemsSource = xmlCon.ReadCourse(xmlCon.ReadCourse());

            constraints.constraint.Add(new Data.Model.Constraint());
            constraints.constraint[0].type = "period";
            instance = Engine.File.InstanceReader.Read();
        }
        int k = 0;
        List<List<bool>> listOfDays = new List<List<bool>>();  // list ku cdo antar i tij eshte liste
        private void btnAddDay_Click(object sender, RoutedEventArgs e)
        {
            cbCourse.IsEnabled = false;
            Label l = new Label() { Content = "Timeslot " + (k + 1).ToString(), FontSize = 12, FontWeight = FontWeights.Thin, Margin = new Thickness(10, 10, 10, 0) };
            spDay.Children.Add(l);
            ComboBox cb = new ComboBox() { Tag = k, Margin = new Thickness(10, 0, 10, 10) };
            cb.ItemsSource = LoadDaysInComboBox();
            cb.DropDownClosed += Cb_DropDownClosed;
            spDay.Children.Add(cb);
            WrapPanel stackPanelCheckbox = new WrapPanel() { Orientation = Orientation.Horizontal };
            listOfDays.Add(new List<bool>());    //inicializimi i listes ne nivel te dites
            listDays.Add("");
            List<bool> listOfCheckboxes = new List<bool>();  //inicializimi i lister ne nivel te periodes
            for (int i = 0; i < Convert.ToInt32(instance.descriptor.periods_per_day.value); i++)
            {
                CheckBox checkBox = new CheckBox() { Tag = k.ToString() + "," + i.ToString(), Content = i, Margin = new Thickness(2) };
                checkBox.Click += CheckBox_Click;   // vetem e gjeneron eventin e checkboxit
                stackPanelCheckbox.Children.Add(checkBox);
                listOfCheckboxes.Add(false);
            }
            listOfDays[k] = listOfCheckboxes;   // dmth dita k si antar e ka listen listOfCheckboxes
            spDay.Children.Add(stackPanelCheckbox);
            k++;
        }

        private void Cb_DropDownClosed(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                listDays[Convert.ToInt32((sender as ComboBox).Tag)] = (sender as ComboBox).SelectedItem.ToString();    // e kemi vleren e day (comboboxit te zgjedhur) 
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            //  MessageBox.Show((sender as CheckBox).Tag.ToString());

            var nXm = (sender as CheckBox).Tag.ToString().Split(',');
            int n = Convert.ToInt32(nXm[0]);  //merr rreshtin e matrices dmth k - per day
            int m = Convert.ToInt32(nXm[1]);   // merr kolonen e matrices dmth i - per period(checkbox)

            if ((sender as CheckBox).IsChecked == true)
            {
                listOfDays[n][m] = true;
            }
            else
            {
                listOfDays[n][m] = false;
            }
        }
        private List<string> LoadDaysInComboBox()
        {
            List<string> rez = new List<string>();

            for (int i = 0; i < Convert.ToInt32(instance.descriptor.days.value); i++)
            {
                rez.Add(i.ToString());
            }

            return rez;
        }
        private bool ValidateComboBox()
        {
            bool rez = true;

            foreach (var item in listDays)
            {
                if (item == "")
                {
                    rez = false;
                }
            }

            return rez;
        }
    }
}
