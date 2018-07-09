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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XMLgenerator.Connections.Database;
using XMLgenerator.Data.Model;
using XMLgenerator.Engine.AddFromFile;
using XMLgenerator.Engine.File;

namespace XMLgenerator.Views.XMLgenerate
{
    /// <summary>
    /// Interaction logic for MainXMLgenerateView.xaml
    /// </summary>
    public partial class MainXMLgenerateView : Page
    {
        public MainXMLgenerateView()
        {
            InitializeComponent();
        }

        instance instance = new instance();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            XMLgeneratorReader xmlreader = new XMLgeneratorReader();
            xmlreader.OpenConnection();


            instance = InstanceReader.Read();
            instance.courses = xmlreader.ReadCourse();
            instance.rooms = xmlreader.ReadRoom();
            instance.curricula = xmlreader.ReadCurriculum();
            instance.constraints = xmlreader.GenerateAllConstraints();

            try
            {
                XmlGeneratorFiles.GenerateXml(instance);
                txtXmlContent.Text = System.IO.File.ReadAllText("instance.xml");
                ShowNiceView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowNiceView()
        {
            StackPanel instancePanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 5, ShadowDepth = 0, Color = Colors.Black },
                Background = Brushes.Gray,
                Margin = new Thickness(5, 5, 5, 0)
            };
            Label instanceName = new Label()
            {
                Content = "Instance name : " + instance.name,
                FontSize = 16,
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Normal,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            instancePanel.Children.Add(instanceName);
            panelNiceView.Children.Add(instancePanel);

            StackPanel descriptorHeaderPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 5, ShadowDepth = 0, Color = Colors.Black },
                Background = Brushes.Gray,
                Margin = new Thickness(20, 5, 5, 0)
            };
            Label descriptorHeaderName = new Label()
            {
                Content = "Descriptor",
                FontSize = 16,
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Normal,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            descriptorHeaderPanel.Children.Add(descriptorHeaderName);
            panelNiceView.Children.Add(descriptorHeaderPanel);
            Label decriptorDays = new Label()
            {
                Content = "Days : " + instance.descriptor.days.value,
                FontSize = 16,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.Black,
                Background = Brushes.White,
                FontWeight = FontWeights.Thin,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(40, 5, 5, 0)
            };
            Label decriptorPeriodsPerDay = new Label()
            {
                Content = "Periods per day : " + instance.descriptor.periods_per_day.value,
                FontSize = 16,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.Black,
                Background = Brushes.White,
                FontWeight = FontWeights.Thin,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(40, 5, 5, 0)
            };
            Label decriptorDaily = new Label()
            {
                Content = "Daily Lectures MIN : " + instance.descriptor.daily_lectures.min + ", MAX : " + instance.descriptor.daily_lectures.max,
                FontSize = 16,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.Black,
                Background = Brushes.White,
                FontWeight = FontWeights.Thin,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(40, 5, 5, 0)
            };
            panelNiceView.Children.Add(decriptorDays);
            panelNiceView.Children.Add(decriptorPeriodsPerDay);
            panelNiceView.Children.Add(decriptorDaily);

            Label courseHeader = new Label()
            {
                Content = "Courses",
                FontSize = 16,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.White,
                Background = Brushes.Gray,
                FontWeight = FontWeights.Normal,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 5, 5, 0)
            };
            panelNiceView.Children.Add(courseHeader);
            foreach (var item in instance.courses.course)
            {
                Label courseItem = new Label()
                {
                    Content = "Course ID : " + item.id + ", Teacher : " + item.teacher + ", Students : " + item.students,
                    FontSize = 16,
                    Height = 35,
                    Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                    Padding = new Thickness(10, 0, 0, 0),
                    Foreground = Brushes.Black,
                    Background = Brushes.White,
                    FontWeight = FontWeights.Thin,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(40, 5, 5, 0)
                };
                panelNiceView.Children.Add(courseItem);
            }

            Label roomHeader = new Label()
            {
                Content = "Rooms",
                FontSize = 16,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.White,
                Background = Brushes.Gray,
                FontWeight = FontWeights.Normal,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 5, 5, 0)
            };
            panelNiceView.Children.Add(roomHeader);
            foreach (var item in instance.rooms.room)
            {
                Label roomItem = new Label()
                {
                    Content = "Room ID : " + item.id + ", Size : " + item.size + ", Building : " + item.building,
                    FontSize = 16,
                    Height = 35,
                    Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                    Padding = new Thickness(10, 0, 0, 0),
                    Foreground = Brushes.Black,
                    Background = Brushes.White,
                    FontWeight = FontWeights.Thin,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(40, 5, 5, 0)
                };
                panelNiceView.Children.Add(roomItem);
            }

            Label curriculaHeader = new Label()
            {
                Content = "Curricula",
                FontSize = 16,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.White,
                Background = Brushes.Gray,
                FontWeight = FontWeights.Normal,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 5, 5, 0)
            };
            panelNiceView.Children.Add(curriculaHeader);
            foreach (var item in instance.curricula.curriculum)
            {
                Label curiculaID = new Label()
                {
                    Content = "Curriculum: " + item.id,
                    FontSize = 16,
                    Height = 35,
                    Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                    Padding = new Thickness(10, 0, 0, 0),
                    Foreground = Brushes.Black,
                    Background = Brushes.White,
                    FontWeight = FontWeights.Thin,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(40, 5, 5, 0)
                };
                panelNiceView.Children.Add(curiculaID);
                foreach (var itemCourse in item.course)
                {
                    Label itemCourseLabel = new Label()
                    {
                        Content = "Course: " + itemCourse.@ref,
                        FontSize = 16,
                        Height = 35,
                        Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                        Padding = new Thickness(10, 0, 0, 0),
                        Foreground = Brushes.Black,
                        Background = Brushes.White,
                        FontWeight = FontWeights.Thin,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(60, 5, 5, 0)
                    };
                    panelNiceView.Children.Add(itemCourseLabel);
                }
            }

            Label constraintHeader = new Label()
            {
                Content = "Constraints",
                FontSize = 16,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.White,
                Background = Brushes.Gray,
                FontWeight = FontWeights.Normal,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 5, 5, 0)
            };
            panelNiceView.Children.Add(constraintHeader);
            foreach (var item in instance.constraints.constraint)
            {
                Label constraintID = new Label()
                {
                    Content = "Constraint Type: " + item.type + ", Course : " + item.course,
                    FontSize = 16,
                    Height = 35,
                    Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                    Padding = new Thickness(10, 0, 0, 0),
                    Foreground = Brushes.Black,
                    Background = Brushes.White,
                    FontWeight = FontWeights.Thin,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(40, 5, 5, 0)
                };
                panelNiceView.Children.Add(constraintID);
                if (item.type == "period")
                {
                    foreach (var itemPeriod in item.timeslot)
                    {
                        Label itemPeriodLabel = new Label()
                        {
                            Content = "Timeslot Day :" + itemPeriod.day + ", Period : " + itemPeriod.period,
                            FontSize = 16,
                            Height = 35,
                            Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                            Padding = new Thickness(10, 0, 0, 0),
                            Foreground = Brushes.Black,
                            Background = Brushes.White,
                            FontWeight = FontWeights.Thin,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(60, 5, 5, 0)
                        };
                        panelNiceView.Children.Add(itemPeriodLabel);
                    }
                }
                else
                {
                    foreach (var itemRoom in item.room)
                    {
                        Label itemPeriodLabel = new Label()
                        {
                            Content = "Room : " + itemRoom.@ref,
                            FontSize = 16,
                            Height = 35,
                            Effect = new DropShadowEffect() { BlurRadius = 4, ShadowDepth = 0, Color = Colors.Black },
                            Padding = new Thickness(10, 0, 0, 0),
                            Foreground = Brushes.Black,
                            Background = Brushes.White,
                            FontWeight = FontWeights.Thin,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(60, 5, 5, 0)
                        };
                        panelNiceView.Children.Add(itemPeriodLabel);
                    }
                }
            }

            StackPanel EndPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Height = 35,
                Effect = new DropShadowEffect() { BlurRadius = 5, ShadowDepth = 0, Color = Colors.Black },
                Background = Brushes.Gray,
                Margin = new Thickness(5, 5, 5, 5)
            };
            panelNiceView.Children.Add(EndPanel);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Document (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                StreamWriter saveXMLStream = new StreamWriter(dlg.FileName);
                saveXMLStream.Write(txtXmlContent.Text);
                saveXMLStream.Close();
            }
        }

        private void btnAddConstraitsFromFile_Click(object sender, RoutedEventArgs e)
        {
            XMLgeneratorConnection xCon = new XMLgeneratorConnection();
            xCon.OpenConnection();
            AddConstraints addConstraints = new AddConstraints(@"C:\Users\Granit\Desktop\Projekt\onlyPeriodConstraints\Constraintt.csv");
            addConstraints.ReadFile();
            if (addConstraints.IsFileReaded() == true)
            {
                string messageOut;
                if (xCon.InsertConstraints(addConstraints.ReturnConstrantsToObject(), out messageOut) == true)
                {
                    Engine.ImportExport.Database ImportExportEngine = new Engine.ImportExport.Database();
                    if (ImportExportEngine.ImportAllData())
                    {
                        MessageBox.Show("kqyre Granit Databasen!");
                    }
                }

            }
        }
    }
}
