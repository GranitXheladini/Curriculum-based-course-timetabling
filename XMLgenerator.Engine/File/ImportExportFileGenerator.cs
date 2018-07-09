using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLgenerator.Data.Model.ImportExport;

namespace XMLgenerator.Engine.File
{
    public class ImportExportFileGenerator
    {
        DatabaseData dbData;
        public ImportExportFileGenerator(DatabaseData databaseData)
        {
            dbData = databaseData;
        }
        public bool Generate()
        {
            bool result = false;

            string saveLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XMLgenerator//Export//" + DateTime.Now.ToString("yyyyMMhh_mmss");
            if (Directory.Exists(saveLocation) == false)
            {
                Directory.CreateDirectory(saveLocation);
            }
            ConstraintsStream(saveLocation);
            ConstrainttStream(saveLocation);
            CourseStream(saveLocation);
            CurriculaStream(saveLocation);
            CurriculumStream(saveLocation);
            RoomStream(saveLocation);
            TeacherStream(saveLocation);

            return result;
        }

        public string[] GetFilesForImport()
        {
            string saveLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XMLgenerator//Export";

            var directoryInfos = new DirectoryInfo(saveLocation).GetDirectories();
            string latestdirectory = "";

            DateTime lastUpdated = DateTime.MinValue;

            foreach (DirectoryInfo directory in directoryInfos)
            {
                if (directory.LastWriteTime > lastUpdated)
                {
                    lastUpdated = directory.LastWriteTime;
                    latestdirectory = directory.Name;
                }
            }
            string[] fileInFolder = Directory.GetFiles(saveLocation + "//" + latestdirectory);

            return fileInFolder;
        }

        public DataTable ReadFileDataCSV(string fileLocation)
        {
            DataTable dataTable = new DataTable();

            string[] lines = System.IO.File.ReadAllLines(fileLocation);
            string[] header = lines[0].Split(',');
            foreach (var item in header)
            {
                dataTable.Columns.Add(item);
            }
            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                dataTable.Rows.Add(data);
            }
            return dataTable;
        }

        private void ConstraintsStream(string FolderLocation)
        {
            StreamWriter streamWriter = new StreamWriter(FolderLocation + "//Constraints.csv");
            string stringHeader = "id,c_type,c_course_id";
            streamWriter.WriteLine(stringHeader);
            foreach (var item in dbData.ieContraints)
            {
                string stringTemp = item.id + "," + item.type + "," + item.course_id;
                streamWriter.WriteLine(stringTemp);
            }
            streamWriter.Close();
        }
        private void ConstrainttStream(string FolderLocation)
        {
            StreamWriter streamWriter = new StreamWriter(FolderLocation + "//Constraintt.csv");
            string stringHeader = "constraint_id,c_type,course_id,room_id,timeslot_day,timeslot_period";
            streamWriter.WriteLine(stringHeader);
            foreach (var item in dbData.ieconstraintts)
            {
                string stringTemp = item.constraint_id + "," + item.type + ","
                    + item.course_id + "," + item.room_id + "," + item.timeslot_day + "," + item.timeslot_period;
                streamWriter.WriteLine(stringTemp);
            }
            streamWriter.Close();
        }
        private void CourseStream(string FolderLocation)
        {
            StreamWriter streamWriter = new StreamWriter(FolderLocation + "//Course.csv");
            string stringHeader = "id,teacherID,lectures,min_days,students,double_lectures,course_name";
            streamWriter.WriteLine(stringHeader);
            foreach (var item in dbData.iecourse)
            {
                string stringTemp = item.id + "," + item.teacherID + "," + item.lectures + ","
                    + item.minDays + "," + item.students + "," + item.doubleLectures + "," + item.courseName;
                streamWriter.WriteLine(stringTemp);
            }
            streamWriter.Close();
        }
        private void CurriculaStream(string FolderLocation)
        {
            StreamWriter streamWriter = new StreamWriter(FolderLocation + "//Curricula.csv");
            string stringHeader = "curriculum_id";
            streamWriter.WriteLine(stringHeader);
            foreach (var item in dbData.iecurricula)
            {
                string stringTemp = item.curriculumID;
                streamWriter.WriteLine(stringTemp);
            }
            streamWriter.Close();
        }
        private void CurriculumStream(string FolderLocation)
        {
            StreamWriter streamWriter = new StreamWriter(FolderLocation + "//Curriculum.csv");
            string stringHeader = "curriculum_id,course_id";
            streamWriter.WriteLine(stringHeader);
            foreach (var item in dbData.iecurriculum)
            {
                string stringTemp = item.curriculumID + "," + item.courseID;
                streamWriter.WriteLine(stringTemp);
            }
            streamWriter.Close();
        }
        private void RoomStream(string FolderLocation)
        {
            StreamWriter streamWriter = new StreamWriter(FolderLocation + "//Room.csv");
            string stringHeader = "id,size,building";
            streamWriter.WriteLine(stringHeader);
            foreach (var item in dbData.ieroom)
            {
                string stringTemp = item.id + "," + item.size + "," + item.building;
                streamWriter.WriteLine(stringTemp);
            }
            streamWriter.Close();
        }
        private void TeacherStream(string FolderLocation)
        {
            StreamWriter streamWriter = new StreamWriter(FolderLocation + "//teacher.csv");
            string stringHeader = "id,t_name";
            streamWriter.WriteLine(stringHeader);
            foreach (var item in dbData.ieteacher)
            {
                string stringTemp = item.id + "," + item.name;
                streamWriter.WriteLine(stringTemp);
            }
            streamWriter.Close();
        }
    }
}
