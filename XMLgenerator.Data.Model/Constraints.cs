using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLgenerator.Data.Model
{
    public class Constraints
    {
        [System.Xml.Serialization.XmlElementAttribute("constraint")]
        public List<Constraint> constraint { get; set; }
        public Constraints()
        {
            constraint = new List<Constraint>();
        }
    }
    public class Constraint
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string course { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("timeslot")]
        public List<Timeslot> timeslot { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("room")]
        public List<Room1> room { get; set; }
    }
    public class Timeslot
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string day { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string period { get; set; }
    }
    public class Room1
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @ref { get; set; }
    }

}
