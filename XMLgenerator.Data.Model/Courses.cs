using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLgenerator.Data.Model
{
    public class Courses
    {
        [System.Xml.Serialization.XmlElementAttribute("course")]
        public List<Course> course { get; set; }

        public Courses()
        {
            course = new List<Course>();
        }
    }

    public class Course
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string teacher { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lectures { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string min_days { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string students { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string double_lectures { get; set; }
    }
}
