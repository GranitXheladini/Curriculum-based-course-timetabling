using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLgenerator.Data.Model.Graphic
{
    public class VisualizationGraph
    {
        public ObservableCollection<VisualizationModel> graphLinesList { get; private set; }
        public VisualizationGraph()
        {
            //IN-DEVELOPMENT PROCCESS - STATIC DATA (Test Data)

            graphLinesList = new ObservableCollection<VisualizationModel>();

            graphLinesList.Add(new VisualizationModel() { Category = "Done", Number = 75 });
            graphLinesList.Add(new VisualizationModel() { Category = "Error", Number = 2 });
            graphLinesList.Add(new VisualizationModel() { Category = "Warning", Number = 12 });
            graphLinesList.Add(new VisualizationModel() { Category = "Skipped", Number = 44 });
        }
        public VisualizationGraph(ObservableCollection<VisualizationModel> visualizationModels)
        {
            graphLinesList = visualizationModels;
        }

        private object selectedItem = null;
        public object SelectItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
            }
        }
    }
}
