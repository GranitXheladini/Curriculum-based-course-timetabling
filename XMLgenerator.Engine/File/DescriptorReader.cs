using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMLgenerator.Data.Model;

namespace XMLgenerator.Engine.File
{
    public static class InstanceReader
    {
        public static instance Read()
        {
            instance instance = new instance();

            if (System.IO.File.Exists("Instance.gxh"))
            {
                try
                {
                    string filecontent = System.IO.File.ReadAllText("Instance.gxh");
                    string[] content = filecontent.Split('#');
                    instance.name = content[0];
                    instance.descriptor.days.value = content[1];
                    instance.descriptor.periods_per_day.value = content[2];
                    instance.descriptor.daily_lectures.min = content[3];
                    instance.descriptor.daily_lectures.max = content[4];
                }
                catch (Exception)
                {

                }
            }

            return instance;
        }

    }
}
