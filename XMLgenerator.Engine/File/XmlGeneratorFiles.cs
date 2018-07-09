using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using XMLgenerator.Data.Model;

namespace XMLgenerator.Engine.File
{
    public static class XmlGeneratorFiles
    {
        public static void GenerateXml(instance instance)
        {
            if (System.IO.File.Exists("instance.xml"))
            {
                System.IO.File.Delete("instance.xml");
            }

            var xns = new XmlSerializerNamespaces();

            xns.Add(string.Empty, string.Empty);

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("instance.xml", settings))
            {
                writer.WriteDocType("instance", null, "http://tabu.diegm.uniud.it/ctt/cb_ctt.dtd", null);
                var serializer = new XmlSerializer(instance.GetType());
                serializer.Serialize(writer, instance, xns);
            }


        }
    }
}
