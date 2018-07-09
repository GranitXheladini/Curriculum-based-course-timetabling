using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLgenerator.Data.Model
{
   
    public class instance
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name { get; set; }
        public Descriptor descriptor { get; set; }
        public Courses courses { get; set; }
        public Rooms rooms { get; set; }
        public Curricula curricula { get; set; }
        public Constraints constraints { get; set; }
        public instance()
        {
            descriptor = new Descriptor();
        }
    }

}
