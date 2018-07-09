using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLgenerator.Data.Model.ImportExport
{
    public class IEconstraints
    {
        public string id { get; set; }
        public string type { get; set; }
        public string course_id { get; set; }
    }
    public class IEconstraintt
    {
        public string constraint_id { get; set; }
        public string type { get; set; }
        public string course_id { get; set; }
        public string room_id { get; set; }
        public string timeslot_day { get; set; }
        public string timeslot_period { get; set; }
    }
    public class IEcourse
    {
        public string id { get; set; }
        public string teacherID { get; set; }
        public string lectures { get; set; }
        public string minDays { get; set; }
        public string students { get; set; }
        public string doubleLectures { get; set; }
        public string courseName { get; set; }
    }
    public class IEcurricula
    {
        public string curriculumID { get; set; }
    }
    public class IEcurriculum
    {
        public string curriculumID { get; set; }
        public string courseID { get; set; }
    }
    public class IEroom
    {
        public string id { get; set; }
        public string size { get; set; }
        public string building { get; set; }
    }
    public class IEteacher
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class DatabaseData
    {
        public List<IEconstraints> ieContraints { get; set; }
        public List<IEconstraintt> ieconstraintts { get; set; }
        public List<IEcourse> iecourse { get; set; }
        public List<IEcurricula> iecurricula { get; set; }
        public List<IEcurriculum> iecurriculum { get; set; }
        public List<IEroom> ieroom { get; set; }
        public List<IEteacher> ieteacher { get; set; }
    }


}
