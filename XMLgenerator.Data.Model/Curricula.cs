using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLgenerator.Data.Model
{
    public class Curricula
    {
        [System.Xml.Serialization.XmlElementAttribute("curriculum")]
        public List<Curriculum> curriculum { get; set; }
        public Curricula()
        {
            curriculum = new List<Curriculum>();
        }
    }

    public class Curriculum
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("course")]
        public List<Course1> course { get; set; }
        public Curriculum()
        {
            course = new List<Course1>();
        }
    }
    public class Course1
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @ref { get; set; }
    }

}
