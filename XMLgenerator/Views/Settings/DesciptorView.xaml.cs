using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for DesciptorView.xaml
    /// </summary>
    public partial class DesciptorView : Page
    {
        public DesciptorView()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Instance.gxh"))
            {
                File.Delete("Instance.gxh");
            }
            StreamWriter streamWriter = new StreamWriter("Instance.gxh");
            string filecontent = txtInstanceName.Text + "#";
            filecontent = filecontent + txtDays.Text + "#";
            filecontent = filecontent + txtPeriods_Per_Day.Text + "#";
            filecontent = filecontent + txtDaily_Lectures_min.Text + "#";
            filecontent = filecontent + txtDaily_Lectures_max.Text;
            streamWriter.Write(filecontent);
            streamWriter.Close();
            MessageBox.Show("Instance has been saved", "Successful");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Instance.gxh"))
            {
                try
                {
                    string filecontent = File.ReadAllText("Instance.gxh");
                    string[] content = filecontent.Split('#');
                    txtInstanceName.Text = content[0];
                    txtDays.Text = content[1];
                    txtPeriods_Per_Day.Text = content[2];
                    txtDaily_Lectures_min.Text = content[3];
                    txtDaily_Lectures_max.Text = content[4];
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
