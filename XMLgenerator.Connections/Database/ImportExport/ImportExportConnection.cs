using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLgenerator.Data.Model.ImportExport;

namespace XMLgenerator.Connections.Database.ImportExport
{
    public class ImportExportConnection
    {
        public ImportExportConnection()
        {
            connect = new SqlConnection(conString);
        }
        private SqlConnection connect;
        private SqlCommand cmd;
        private SqlDataReader reader;

        private string conString = "Data Source=127.0.0.1;Initial Catalog=XMLgeneratorDB;User ID=sa;Password=9323";
        //private string conString = "Data Source=192.168.0.19;Initial Catalog=XMLgeneratorDB;User ID=sa;Password=9323";

        public void OpenConnection()
        {
            try
            {
                connect.Open();
            }
            catch (Exception)
            {

            }
        }
        public void CloseConnection()
        {
            connect.Close();
        }

        //Metodat per eksportim

        public List<IEconstraints> ExportConstraints()
        {
            List<IEconstraints> econstraints = new List<IEconstraints>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Constraints", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        econstraints.Add(new IEconstraints()
                        {
                            id = reader[0].ToString(),
                            type = reader[1].ToString(),
                            course_id = reader[2].ToString()
                        });
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }

            return econstraints;
        }
        public List<IEconstraintt> ExportConstraintt()
        {
            List<IEconstraintt> econstraintt = new List<IEconstraintt>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Constraintt", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        econstraintt.Add(new IEconstraintt()
                        {
                            constraint_id = reader[0].ToString(),
                            type = reader[1].ToString(),
                            course_id = reader[2].ToString(),
                            room_id = reader[3].ToString(),
                            timeslot_day = reader[4].ToString(),
                            timeslot_period = reader[5].ToString()
                        });
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }

            return econstraintt;
        }
        public List<IEcourse> ExportCourse()
        {
            List<IEcourse> ecourse = new List<IEcourse>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Course", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ecourse.Add(new IEcourse()
                        {
                            id = reader[0].ToString(),
                            teacherID = reader[1].ToString(),
                            lectures = reader[2].ToString(),
                            minDays = reader[3].ToString(),
                            students = reader[4].ToString(),
                            doubleLectures = reader[5].ToString(),
                            courseName = reader[6].ToString()
                        });
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }

            return ecourse;
        }
        public List<IEcurricula> ExportCurricula()
        {
            List<IEcurricula> ecurricula = new List<IEcurricula>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Curricula", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ecurricula.Add(new IEcurricula()
                        {
                            curriculumID = reader[0].ToString()
                        });
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }
            return ecurricula;
        }
        public List<IEcurriculum> ExportCurriculum()
        {
            List<IEcurriculum> ecurriculum = new List<IEcurriculum>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Curriculum", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ecurriculum.Add(new IEcurriculum()
                        {
                            curriculumID = reader[0].ToString(),
                            courseID = reader[1].ToString()
                        });
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }
            return ecurriculum;
        }
        public List<IEroom> ExportRoom()
        {
            List<IEroom> eroom = new List<IEroom>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Room", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        eroom.Add(new IEroom()
                        {
                            id = reader[0].ToString(),
                            size = reader[1].ToString(),
                            building = reader[2].ToString()
                        });
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }
            return eroom;
        }
        public List<IEteacher> ExportTeacher()
        {
            List<IEteacher> eteacher = new List<IEteacher>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from teacher", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        eteacher.Add(new IEteacher()
                        {
                            id = reader[0].ToString(),
                            name = reader[1].ToString(),
                        });
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }
            return eteacher;
        }


        // Metodat per importim

        public bool InsertConstraints(List<IEconstraints> iconstraints)
        {
            bool rez = false;

            if (connect.State == ConnectionState.Open)
            {
                foreach (var item in iconstraints)
                {
                    cmd = new SqlCommand("Insert into Course(id, c_type, c_course_id)" + "values(@id, @type, @course_id)", connect);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 5).Value = item.id;
                    cmd.Parameters.Add("@type", SqlDbType.Int).Value = item.type;
                    cmd.Parameters.Add("@course_id", SqlDbType.Int).Value = item.course_id;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rez = true;
                    }
                    catch (Exception)
                    {
                        rez = false;
                    }
                }
            }
            else
            {
                rez = false;
            }

            return rez;
        }

        public bool InsertConstraintt(List<IEconstraintt> iconstraintt)
        {
            bool rez = false;

            if (connect.State == ConnectionState.Open)
            {
                foreach (var item in iconstraintt)
                {
                    cmd = new SqlCommand("Insert into Constraintt(constraint_id, c_type, course_id, room_id, timeslot_day, timeslot_period)" + "values(@id, @type, @course_id, @room_id, @tms_day, @tms_period)", connect);
                    cmd.Parameters.Add("@constraint_id", SqlDbType.VarChar, 5).Value = item.constraint_id;
                    cmd.Parameters.Add("@c_type", SqlDbType.Int).Value = item.type;
                    cmd.Parameters.Add("@course_id", SqlDbType.Int).Value = item.course_id;
                    cmd.Parameters.Add("@room_id", SqlDbType.VarChar, 5).Value = item.room_id;
                    cmd.Parameters.Add("@tms_day", SqlDbType.Int).Value = item.timeslot_day;
                    cmd.Parameters.Add("@tms_period", SqlDbType.Int).Value = item.timeslot_period;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rez = true;
                    }
                    catch (Exception)
                    {
                        rez = false;
                    }
                }
            }
            else
            {
                rez = false;
            }

            return rez;
        }

        public bool InsertCourse(List<IEcourse> icourse)
        {
            bool rez = false;

            if (connect.State == ConnectionState.Open)
            {
                foreach (var item in icourse)
                {
                    cmd = new SqlCommand("Insert into Course(id, teacherID, lectures, min_days, students, double_lectures, course_name)" + "values(@id, @teacherID, @lectures, @mind, @stud, @doublep,@coursen)", connect);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 5).Value = item.id;
                    cmd.Parameters.Add("@teacherID", SqlDbType.Int).Value = item.teacherID;
                    cmd.Parameters.Add("@lectures", SqlDbType.Int).Value = item.lectures;
                    cmd.Parameters.Add("@mind", SqlDbType.VarChar, 5).Value = item.minDays;
                    cmd.Parameters.Add("@stud", SqlDbType.Int).Value = item.students;
                    cmd.Parameters.Add("@doublep", SqlDbType.Int).Value = item.doubleLectures;
                    cmd.Parameters.Add("@coursen", SqlDbType.Int).Value = item.courseName;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rez = true;
                    }
                    catch (Exception)
                    {
                        rez = false;
                    }
                }
            }
            else
            {
                rez = false;
            }

            return rez;
        }

        public bool InsertCurricula(List<IEcurricula> icurricula)
        {
            bool rez = false;

            if (connect.State == ConnectionState.Open)
            {
                foreach (var item in icurricula)
                {
                    cmd = new SqlCommand("Insert into Curricula(curriculum_id)" + "values(@id)", connect);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 5).Value = item.curriculumID;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rez = true;
                    }
                    catch (Exception)
                    {
                        rez = false;
                    }
                }
            }
            else
            {
                rez = false;
            }

            return rez;
        }

        public bool InsertCurriculum(List<IEcurriculum> icurriculum)
        {
            bool rez = false;

            if (connect.State == ConnectionState.Open)
            {
                foreach (var item in icurriculum)
                {
                    cmd = new SqlCommand("Insert into Curricula(curriculum_id, course_id)" + "values(@id, @c_id)", connect);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 5).Value = item.curriculumID;
                    cmd.Parameters.Add("@c_id", SqlDbType.VarChar, 5).Value = item.courseID;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rez = true;
                    }
                    catch (Exception)
                    {
                        rez = false;
                    }
                }
            }
            else
            {
                rez = false;
            }

            return rez;
        }

        public bool InsertRoom(List<IEroom> iroom)
        {
            bool rez = false;

            if (connect.State == ConnectionState.Open)
            {
                foreach (var item in iroom)
                {
                    cmd = new SqlCommand("Insert into Curricula(id, size, building)" + "values(@id, @size, @building)", connect);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 5).Value = item.id;
                    cmd.Parameters.Add("@size", SqlDbType.VarChar, 5).Value = item.size;
                    cmd.Parameters.Add("@building", SqlDbType.VarChar, 5).Value = item.building;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rez = true;
                    }
                    catch (Exception)
                    {
                        rez = false;
                    }
                }
            }
            else
            {
                rez = false;
            }

            return rez;
        }

        public bool InsertTeacher(List<IEteacher> iteacher)
        {
            bool rez = false;

            if (connect.State == ConnectionState.Open)
            {
                foreach (var item in iteacher)
                {
                    cmd = new SqlCommand("Insert into Curricula(id, t_name)" + "values(@id, @t_name)", connect);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 5).Value = item.id;
                    cmd.Parameters.Add("@t_name", SqlDbType.VarChar, 5).Value = item.name;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rez = true;
                    }
                    catch (Exception)
                    {
                        rez = false;
                    }
                }
            }
            else
            {
                rez = false;
            }

            return rez;
        }

        public bool DropTables()
        {
            bool rez = false;

            if (connect.State == ConnectionState.Open)
            {

                cmd = new SqlCommand("Drop table Constraints; Drop table Constraintt; Drop table Course; Drop table Curricula; Drop table Curriculum; Drop table Room; Drop table teacher;", connect);
                try
                {
                    cmd.ExecuteNonQuery();
                    rez = true;
                }
                catch (Exception ex)
                {
                    rez = false;
                }
            }
            else
            {
                rez = false;
            }

            return rez;
        }
        public bool CreateTables()
        {
            bool rez = false;
            string Constraint = "create table Constraints(id int identity primary key,c_type varchar(10) not null,c_course_id varchar(10) not null);";
            string Constraintt = "create table Constraintt(constraint_id int not null,c_type varchar(10) not null,course_id varchar(10) not null,room_id varchar(5),timeslot_day varchar(5) ,timeslot_period varchar(5));";
            string Course = "create table Course(id varchar(10) not null primary key,teacherID varchar(10) not null,lectures int not null,min_days int not null,students int not null,double_lectures varchar(5) not null,course_name varchar(200));";
            string Curricula = "create table Curricula(curriculum_id varchar(10) not null primary key);";
            string Curriculum = "create table Curriculum(curriculum_id varchar(10) not null,course_id varchar(10) not null);";
            string Room = "create table Room(id varchar(5) not null,size int not null,building int not null);";
            string teacher = "create table teacher(id varchar(10) not null,t_name varchar(30) not null);";

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand(Constraint + Constraintt + Course + Curricula + Curriculum + Room + teacher, connect);
                try
                {
                    cmd.ExecuteNonQuery();
                    rez = true;
                }
                catch (Exception)
                {
                    rez = false;
                }
            }
            else
            {
                rez = false;
            }
            return rez;
        }



        public void ImportDataIntoTable(DataTable dataTable, string tableName)
        {
            using (SqlBulkCopy s = new SqlBulkCopy(connect))
            {
                s.DestinationTableName = tableName;
                foreach (var column in dataTable.Columns)
                    s.ColumnMappings.Add(column.ToString(), column.ToString());
                s.WriteToServer(dataTable);
            }
        }

    }
}
