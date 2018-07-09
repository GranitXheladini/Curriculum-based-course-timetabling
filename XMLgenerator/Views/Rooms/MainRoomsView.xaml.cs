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

namespace XMLgenerator.Views.Rooms
{
    /// <summary>
    /// Interaction logic for MainRoomsView.xaml
    /// </summary>
    public partial class MainRoomsView : Page
    {
        public MainRoomsView()
        {
            InitializeComponent();
        }

        private string GenerateId()
        {
            string actualID = Properties.Settings.Default.RoomId;
            int nextID = Convert.ToInt32(actualID) + 1;
            actualID = nextID.ToString();
            actualID = "r" + actualID.PadLeft(2, '0');
            return actualID;
        }
        XMLgeneratorConnection xmlCon;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            xmlCon = new XMLgeneratorConnection();
            xmlCon.OpenConnection();
            txtID.Text = GenerateId();
           
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string message;
            Room room = new Room() { id = txtID.Text, size = txtSize.Text, building = txtBuilding.Text };
            if(xmlCon.InsertRoom(room, out message)==true)
            {
                Properties.Settings.Default.RoomId = txtID.Text.Remove(0,1);
                Properties.Settings.Default.Save();
                txtID.Text = GenerateId();
                txtSize.Text = "";
                txtBuilding.Text = "";
            }
            
        }

        private void txtBuilding_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtSize.Text !="" && txtBuilding.Text!="")
            {
                btnSave.IsEnabled = true;
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }
    }
}
