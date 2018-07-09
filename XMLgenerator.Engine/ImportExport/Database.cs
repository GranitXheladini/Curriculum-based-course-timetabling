using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLgenerator.Connections.Database.ImportExport;
using XMLgenerator.Data.Model.ImportExport;

namespace XMLgenerator.Engine.ImportExport
{

    public class Database
    {
        ImportExportConnection importExportCon;
        public Database()
        {
            importExportCon = new ImportExportConnection();
            importExportCon.OpenConnection();
        }
        public DatabaseData TableToFile()
        {
            DatabaseData databaseData = new DatabaseData();

            databaseData.ieContraints = importExportCon.ExportConstraints();
            databaseData.ieconstraintts = importExportCon.ExportConstraintt();
            databaseData.iecourse = importExportCon.ExportCourse();
            databaseData.iecurricula = importExportCon.ExportCurricula();
            databaseData.iecurriculum = importExportCon.ExportCurriculum();
            databaseData.ieroom = importExportCon.ExportRoom();
            databaseData.ieteacher = importExportCon.ExportTeacher();

            return databaseData;
        }

        public bool DropTables() => importExportCon.DropTables();   // metode qe ne return kthen return importExportCon.DropTable();
        public bool CreateTables()
        {
            return importExportCon.CreateTables();
        }

        public bool ImportAllData()
        {
            bool result = false;

            File.ImportExportFileGenerator importExportFileGenerator = new File.ImportExportFileGenerator(null);
            string[] files = importExportFileGenerator.GetFilesForImport();
            foreach (var item in files)
            {
                string fileName= Path.GetFileNameWithoutExtension(item);
                DataTable dataT = importExportFileGenerator.ReadFileDataCSV(item);
                importExportCon.ImportDataIntoTable(dataT, fileName);
            }

            return result;
        }
    }


}
