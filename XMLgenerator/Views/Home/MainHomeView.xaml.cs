using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using XMLgenerator.Data.Model.Graphic;
using XMLgenerator.Connections.Database.Visualization;
using System.Windows.Media.Effects;

namespace XMLgenerator.Views.Home
{
    /// <summary>
    /// Interaction logic for MainHomeView.xaml
    /// </summary>
    public partial class MainHomeView : Page
    {
        Button btnHomeMain;
        public MainHomeView(Button _btnHomeMain)
        {
            InitializeComponent();
            btnHomeMain = _btnHomeMain;
        }
        VisualizationConnection vsCon = new VisualizationConnection();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<VisualizationModel> observableCollection = new ObservableCollection<VisualizationModel>();

            vsCon.OpenConnection();
            lblRoom.Content = vsCon.CountRooms();
            lblCourse.Content = vsCon.CountCourses();
            lblCurricula.Content = vsCon.CountCurricula();
            lblConstraint.Content = vsCon.CountConstraint();

            lblNumberOfRooms.Content = "Number of rooms : " + lblRoom.Content;
            lblNumberOfBuilding.Content = "Number of buildings : " + vsCon.CountRoomBuildings();
            dataRooms.ItemsSource = vsCon.ReadRoom().room;

            lblNumberOfCourse.Content = "Number of courses : " + lblCourse.Content;
            LoadCoursesChart();

            observableCollection.Add(new VisualizationModel() { Category = "Rooms", Number = Convert.ToInt32(lblRoom.Content) });
            observableCollection.Add(new VisualizationModel() { Category = "Course", Number = Convert.ToInt32(lblCourse.Content) });
            observableCollection.Add(new VisualizationModel() { Category = "Curricula", Number = Convert.ToInt32(lblCurricula.Content) });
            observableCollection.Add(new VisualizationModel() { Category = "Constraints", Number = Convert.ToInt32(lblConstraint.Content) });

            mainChart.DataContext = new Data.Model.Graphic.VisualizationGraph(observableCollection);
        }

        private void mainChart_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //VisualizationModel visualizationModel = mainChart.SelectedItem as VisualizationModel;
            //MessageBox.Show(visualizationModel.Category);
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            gridRoom.Effect = new DropShadowEffect() { BlurRadius = 10, Direction = 270, ShadowDepth = 4 };

        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            gridRoom.Effect = new DropShadowEffect() { BlurRadius = 4, Direction = 0, ShadowDepth = 0 };
        }

        private void gridCourse_MouseEnter(object sender, MouseEventArgs e)
        {
            gridCourse.Effect = new DropShadowEffect() { BlurRadius = 10, Direction = 270, ShadowDepth = 4 };
        }

        private void gridCourse_MouseLeave(object sender, MouseEventArgs e)
        {
            gridCourse.Effect = new DropShadowEffect() { BlurRadius = 4, Direction = 0, ShadowDepth = 0 };
        }
        private void gridCurricula_MouseEnter(object sender, MouseEventArgs e)
        {
            gridCurricula.Effect = new DropShadowEffect() { BlurRadius = 10, Direction = 270, ShadowDepth = 4 };
        }

        private void gridCurricula_MouseLeave(object sender, MouseEventArgs e)
        {
            gridCurricula.Effect = new DropShadowEffect() { BlurRadius = 4, Direction = 0, ShadowDepth = 0 };
        }
        private void gridConstraint_MouseEnter(object sender, MouseEventArgs e)
        {
            gridConstraint.Effect = new DropShadowEffect() { BlurRadius = 10, Direction = 270, ShadowDepth = 4 };
        }

        private void gridConstraint_MouseLeave(object sender, MouseEventArgs e)
        {
            gridConstraint.Effect = new DropShadowEffect() { BlurRadius = 4, Direction = 0, ShadowDepth = 0 };
        }

        private void EnableAllButtons()
        {
            btnRoom.IsEnabled = true;
            btnCourse.IsEnabled = true;


            subGridCourse.Visibility = Visibility.Collapsed;
            subGridRoom.Visibility = Visibility.Collapsed;
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            EnableAllButtons();
            subGridRoom.Visibility = Visibility.Visible;
            btnRoom.IsEnabled = false;
        }

        private void btnCourse_Click(object sender, RoutedEventArgs e)
        {
            EnableAllButtons();
            subGridCourse.Visibility = Visibility.Visible;

            btnCourse.IsEnabled = false;
        }

        public void LoadCoursesChart()
       {
            ObservableCollection<VisualizationModel> observableCollection = new ObservableCollection<VisualizationModel>();
            List<string> listOfTeachers = vsCon.ReadTeacherFromCourse();
            foreach (var item in listOfTeachers)
            {
                observableCollection.Add(new VisualizationModel() { Category = item, Number = vsCon.CountCoursesOfTeacher(item) });
            }
            charCourse.DataContext =new  Data.Model.Graphic.VisualizationGraph(observableCollection);
        }

        private void btnSubRoomsMenu_Click(object sender, RoutedEventArgs e)
        {
            btnHomeMain.IsEnabled = true;
            this.NavigationService.Navigate(new Views.Rooms.MainRoomsView());
        }

        private void btnSubConstraintsMenu_Click(object sender, RoutedEventArgs e)
        {
            btnHomeMain.IsEnabled = true;
            this.NavigationService.Navigate(new Views.Constraint.MainConstraintOptionView());
        }
    }
}
