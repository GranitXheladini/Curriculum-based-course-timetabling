using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLgenerator.Data.Model
{
    public class Rooms
    {
        [System.Xml.Serialization.XmlElementAttribute("room")]
        public List<Room> room { get; set; }
        public Rooms() => room = new List<Room>();
    }

    public class Room
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string size { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string building { get; set; }

    }
}
