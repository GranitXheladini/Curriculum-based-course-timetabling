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

namespace XMLgenerator.Views.Constraint
{
    /// <summary>
    /// Interaction logic for MainConstraintRoomView.xaml
    /// </summary>
    public partial class MainConstraintRoomView : Page
    {
        public MainConstraintRoomView()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Constraint.MainConstraintOptionView());
        }
        int roomCounter = 0;
        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            cbCourse.IsEnabled = false;

            Label l = new Label() { Content = "Room " + Convert.ToInt32(roomCounter + 1), FontSize = 12, FontWeight = FontWeights.Thin, Margin = new Thickness(10, 10, 10, 0) };
            spRoom.Children.Add(l);
            ComboBox cmb = new ComboBox() { Tag = roomCounter, Margin = new Thickness(10, 0, 10, 10) };
            cmb.DropDownClosed += Cmb_DropDownClosed;
            cmb.ItemsSource = xmlCon.ReadRoom();
            spRoom.Children.Add(cmb);
            listOfSelectedRoom.Add(false);
            listRooms.Add(new Room1());
            roomCounter++;
        }
        List<bool> listOfSelectedRoom = new List<bool>();
        List<Room1> listRooms = new List<Room1>();
        Constraints constraints = new Constraints();
        private void Cmb_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb_temp = sender as ComboBox;

            if (cmb_temp.SelectedIndex != -1)
            {
                listRooms[Convert.ToInt32(cmb_temp.Tag)].@ref = cmb_temp.SelectedItem.ToString();
                listOfSelectedRoom[Convert.ToInt32(cmb_temp.Tag)] = true;
            }
        }

        XMLgeneratorConnection xmlCon;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            xmlCon = new XMLgeneratorConnection();
            xmlCon.OpenConnection();

            cbCourse.ItemsSource = xmlCon.ReadCourse(xmlCon.ReadCourse());

            constraints.constraint.Add(new Data.Model.Constraint());
            constraints.constraint[0].type = "room";

        }

        private void cbCourse_DropDownClosed(object sender, EventArgs e)
        {
            if (cbCourse.SelectedIndex != -1)
            {
                btnAddRoom.IsEnabled = true;
                constraints.constraint[0].course = cbCourse.SelectedItem.ToString();
            }
            else
            {
                btnAddRoom.IsEnabled = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            if (ValidateDataInComboBoxs() == true)
            {
                constraints.constraint[0].room = listRooms;
                if(xmlCon.InsertConstraints(constraints, out msg)==true)
                {
                    this.NavigationService.Navigate(new Constraint.MainConstraintOptionView());
                }
            }
        }
        private bool ValidateDataInComboBoxs()
        {
            bool rez = true;

            foreach (var item in listOfSelectedRoom)
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
