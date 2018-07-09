using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using XMLgenerator.Data.Model.ImportExport;
using XMLgenerator.Engine.File;

namespace XMLgenerator.Views.Settings.ImortExport
{
    /// <summary>
    /// Interaction logic for ImportExportView.xaml
    /// </summary>
    public partial class ImportExportView : Page
    {
        public ImportExportView()
        {
            InitializeComponent();
        }
        DispatcherTimer importExportTimer = new DispatcherTimer();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbFunctions.Items.Add("Import");
            cbFunctions.Items.Add("Export");
            importExportTimer.Interval = new TimeSpan(0, 0, 1);
            importExportTimer.Tick += ImportExportTimer_Tick;
            importExportTimer.Stop();
        }

        int k = 0;
        Engine.ImportExport.Database ImportExportEngine = new Engine.ImportExport.Database();
        bool dropedTables = false;
        bool newTables = false;
        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            if (cbFunctions.SelectedValue == "Import")
            {
                cbFunctions.IsEnabled = false;
                progress.IsIndeterminate = true;
                importExportTimer.Start();
                btnExecute.IsEnabled = false;
            }
            else if (cbFunctions.SelectedValue == "Export")
            {
                cbFunctions.IsEnabled = false;
                progress.IsIndeterminate = true;
                importExportTimer.Start();
                btnExecute.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Select function", "Fatal error");
            }
        }
        private void ImportExportTimer_Tick(object sender, EventArgs e)
        {
            if (k == 0)
            {
                listContent.Items.Add(cbFunctions.Text + " has been started");
            }
            else if (k == 1)
            {
                listContent.Items.Add("Exporting data");
                if (cbFunctions.SelectedValue != "Import")
                {
                    SaveActualDataOfDatabase();

                }
            }
            else if (k == 3)
            {
                listContent.Items.Add("Exporting data has finished");
            }
            else if (k == 4)
            {
                listContent.Items.Add("Deleting Tables");
                dropedTables = ImportExportEngine.DropTables();

            }
            else if (k == 6)
            {
                if (dropedTables == true)
                {
                    listContent.Items.Add("Tables are deleted");
                    listContent.Items.Add("Creating new Tables");
                    newTables = ImportExportEngine.CreateTables();
                }
            }
            else if (k == 8)
            {
                if (newTables == true)
                {
                    listContent.Items.Add("Tables are created");
                    Properties.Settings.Default.TeacherId = "000";
                    Properties.Settings.Default.CourseId = "0000";
                    Properties.Settings.Default.RoomId = "00";
                    Properties.Settings.Default.CurriculaId = "000";
                    Properties.Settings.Default.Save();
                }
            }
            else if (k == 9)
            {
                if (cbFunctions.SelectedValue == "Export")
                {
                    listContent.Items.Add("Export is Finished");
                    progress.IsIndeterminate = false;
                    progress.Value = 100;
                    importExportTimer.Stop();
                }
                else if (cbFunctions.SelectedValue == "Import")
                {
                    ImportExportEngine.ImportAllData();
                    listContent.Items.Add("Import is Finished");
                    progress.IsIndeterminate = false;
                    progress.Value = 100;
                    importExportTimer.Stop();
                }
            }
            else if (true)
            {

            }
            k++;
        }


        private void SaveActualDataOfDatabase()
        {
            DatabaseData db = ImportExportEngine.TableToFile();
            ImportExportFileGenerator f = new ImportExportFileGenerator(db);
            f.Generate();
        }
    }
}
