using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLgenerator.Data.Model;

namespace XMLgenerator.Engine.AddFromFile
{
    public class AddConstraints
    {
        private string FileLocation;
        private bool isFileReaded = false;
        private Constraints constraints = new Constraints();
        public AddConstraints(string fileLocation)
        {
            FileLocation = fileLocation;
        }

        public void ReadFile()
        {
            constraints = new Constraints();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(FileLocation);
                string[] header = lines[0].Split(',');
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] tempData = lines[i].Split(',');
                    if (tempData[1] == "period")
                    {

                        List<Timeslot> timeslots = new List<Timeslot>();
                        for (int j = 0; j < Convert.ToInt32(tempData[5]); j++)
                        {
                            timeslots.Add(new Timeslot() { day = tempData[4], period = j.ToString() });
                        }


                        int id = 0;
                        if (ValidateCourseAndType(tempData[2], tempData[1], out id) == true)
                        {
                            constraints.constraint[id].timeslot.AddRange(timeslots);
                        }
                        else
                        {
                            constraints.constraint.Add(new Constraint() { course = tempData[2], type = tempData[1], timeslot = timeslots });
                        }
                    }
                    else
                    {

                    }
                }
                isFileReaded = true;
            }
            catch (Exception)
            {
                isFileReaded = false;
            }
        }

        private bool ValidateCourseAndType(string course, string type, out int ID)
        {
            bool result = false;
            ID = 0;
            int k = 0;
            foreach (var item in constraints.constraint)
            {
                if (item.course == course && item.type == type)
                {
                    ID = k;
                    result = true;
                }
                k++;
            }
            return result;
        }

        public bool IsFileReaded()
        {
            return isFileReaded;
        }
        public Constraints ReturnConstrantsToObject()
        {
            return constraints;
        }
    }
}
