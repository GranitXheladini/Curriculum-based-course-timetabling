using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using XMLgenerator.Data.Model;

namespace XMLgenerator.Connections.Database
{
    public class XMLgeneratorConnection
    {
        private SqlConnection connect;
        private SqlCommand cmd;
        private SqlDataReader reader;

        private string conString = "Data Source=127.0.0.1;Initial Catalog=XMLgeneratorDB;User ID=sa;Password=9323";
        //private string conString = "Data Source=192.168.0.19;Initial Catalog=XMLgeneratorDB;User ID=sa;Password=9323";

        public XMLgeneratorConnection()
        {
            connect = new SqlConnection(conString);
        }
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

        // out - e ndryshon vleren e parametrit qe eshte perdor
        public bool InsertCourse(CourseWithName courseWithName, out string message)
        {
            bool rez = false;
            message = "";

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Insert into Course(id, teacherID, lectures, min_days, students, double_lectures, course_name)" +
                    "values(@id, @teacherID, @lectures, @min_days, @students, @double_lectures, @name)", connect);
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 10).Value = courseWithName.course.id;
                cmd.Parameters.Add("@teacherID", SqlDbType.VarChar, 10).Value = courseWithName.course.teacher;
                cmd.Parameters.Add("@lectures", SqlDbType.Int).Value = Convert.ToInt32(courseWithName.course.lectures);
                cmd.Parameters.Add("@min_days", SqlDbType.Int).Value = Convert.ToInt32(courseWithName.course.min_days);
                cmd.Parameters.Add("@students", SqlDbType.Int).Value = Convert.ToInt32(courseWithName.course.students);
                cmd.Parameters.Add("@double_lectures", SqlDbType.VarChar, 5).Value = courseWithName.course.double_lectures;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 200).Value = courseWithName.name;

                try
                {
                    cmd.ExecuteNonQuery();
                    rez = true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    rez = false;
                }
            }
            else
            {
                rez = false;
                message = "Connection is closed";
            }

            return rez;
        }

        public List<CourseWithName> ReadCourse()
        {
            List<CourseWithName> list = new List<CourseWithName>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Course", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CourseWithName courseWithName = new CourseWithName();
                        courseWithName.course.id = reader[0].ToString();
                        courseWithName.course.teacher = reader[1].ToString();
                        courseWithName.course.lectures = reader[2].ToString();
                        courseWithName.course.min_days = reader[3].ToString();
                        courseWithName.course.students = reader[4].ToString();
                        courseWithName.course.double_lectures = reader[5].ToString();
                        courseWithName.name = reader[6].ToString();
                        list.Add(courseWithName);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }


            return list;
        }

        /// <summary>
        /// Kjo metode si parameter duhet te kete xmlgeneratorConnection.ReadCourse()
        /// </summary>
        /// <param name="listCourseWithName"></param>
        /// <returns></returns>
        public List<string> ReadCourse(List<CourseWithName> listCourseWithName) // Kyparameter mbushet me info nga DB permes metodes ReadCourse
        {
            List<string> rez = new List<string>();

            foreach (var item in listCourseWithName)
            {
                rez.Add(item.course.id);
            }

            return rez;
        }

        public bool InsertRoom(Room room, out string message)
        {
            bool rez = false;
            message = "";

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Insert into Room(id, size, building)" + "values(@id, @size, @building)", connect);
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 5).Value = room.id;
                cmd.Parameters.Add("@size", SqlDbType.Int).Value = Convert.ToInt32(room.size);
                cmd.Parameters.Add("@building", SqlDbType.Int).Value = Convert.ToInt32(room.building);

                try
                {
                    cmd.ExecuteNonQuery();
                    rez = true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    rez = false;
                }
            }
            else
            {
                rez = false;
                message = "Connection is closed";
            }

            return rez;
        }

        public List<string> ReadRoom()
        {
            List<string> rez = new List<string>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Room", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        rez.Add(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                    reader.Close();
                }
            }

            return rez;
        }

        public bool InsertTeacher(Teacher teacher, out string message)
        {
            bool rez = false;
            message = "";

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Insert into Teacher(id, t_name)" + "values(@id, @name)", connect);
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 10).Value = teacher.id;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 30).Value = teacher.name;

                try
                {
                    cmd.ExecuteNonQuery();
                    rez = true;
                }
                catch (Exception ex)
                {
                    rez = false;
                    message = ex.Message;

                }
            }
            else
            {
                rez = false;
                message = "Connection is closed";
            }

            return rez;
        }



        public bool InsertCurricula(Curricula curricula, out string message)
        {
            bool rez = false;
            message = "";

            if (connect.State == ConnectionState.Open)
            {
                //fillimisht kemi me i rujt te gjitha id e curriculumeve
                //se dyti id se bashku me kurset kan me u rujt ne curriculum

                List<bool> IdsInCurricula = InsertIdsInCurricula(curricula.curriculum);
                if (AreIDsRegistred(IdsInCurricula) == true)
                {
                    if (AreIDsRegistred(InsertCurriculums(curricula.curriculum)) == true)
                    {
                        rez = true;
                    }
                }

            }

            return rez;
        }
        private List<bool> InsertIdsInCurricula(List<Curriculum> Curriculumlist)
        {
            List<bool> rez = new List<bool>();

            foreach (Curriculum item in Curriculumlist)
            {
                cmd = new SqlCommand("Insert into Curricula(curriculum_id)" + "values(@curr_id)", connect);
                cmd.Parameters.Add("@curr_id", SqlDbType.VarChar, 10).Value = item.id;

                try
                {
                    cmd.ExecuteNonQuery();
                    rez.Add(true);
                }
                catch (Exception)
                {
                    rez.Add(false);
                }

            }

            return rez;
        }
        private List<bool> InsertCurriculums(List<Curriculum> Curriculums)
        {
            List<bool> rez = new List<bool>();

            foreach (Curriculum item in Curriculums)
            {
                //regjistro kursin ne nivel te curriculumit

                rez.Add(InsertCourseAndIdInCurriculum(item));
            }

            return rez;
        }

        private bool InsertCourseAndIdInCurriculum(Curriculum curriculum)
        {
            bool rez = false;

            foreach (var item in curriculum.course)
            {
                cmd = new SqlCommand("Insert into Curriculum(curriculum_id, course_id)"
                    + "values(@curr_id, @course_id)", connect);
                cmd.Parameters.Add("@curr_id", SqlDbType.VarChar, 10).Value = curriculum.id;
                cmd.Parameters.Add("@course_id", SqlDbType.VarChar, 10).Value = item.@ref;
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

            return rez;
        }

        private bool AreIDsRegistred(List<bool> list)
        {
            bool rez = true;

            foreach (var item in list)
            {
                if (item == false)
                {
                    rez = false;
                }
            }

            return rez;
        }

        public bool InsertConstraints(Constraints constraints, out string message)
        {
            bool rez = false;
            message = "";

            if (connect.State == ConnectionState.Open)
            {
                foreach (Data.Model.Constraint item in constraints.constraint)
                {
                    if (InsertIdInConstraints(item) == true)
                    {
                        int id = SelectIdFromConstraints(item);
                        if (id != 0)
                        {
                            if (InsertConstraint(id, item) == true)
                            {
                                rez = true;
                            }
                        }
                    }
                }

            }
            else
            {
                rez = false;
                message = "Connection is closed";
            }

            return rez;
        }
        private bool InsertIdInConstraints(Data.Model.Constraint constraint)
        {
            bool rez = false;

            cmd = new SqlCommand("Insert into Constraints(c_type, c_course_id)" + "values(@type, @course)", connect);
            cmd.Parameters.Add("@type", SqlDbType.VarChar, 10).Value = constraint.type;
            cmd.Parameters.Add("@course", SqlDbType.VarChar, 10).Value = constraint.course;

            try
            {
                cmd.ExecuteNonQuery();
                rez = true;
            }
            catch (Exception)
            {
                rez = false;
            }

            return rez;
        }
        public int SelectIdFromConstraints(Data.Model.Constraint constraint)
        {
            int rez = 0;

            cmd = new SqlCommand("Select id from Constraints where c_type='" + constraint.type + "' and c_course_id='" + constraint.course + "'", connect);
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    rez = Convert.ToInt32(reader[0]);
                    reader.Close();
                }

            }
            catch (Exception)
            {
                reader.Close();
            }

            return rez;
        }

        public bool InsertConstraint(int id, Data.Model.Constraint constraint)
        {
            bool rez = false;

            if (constraint.type == "room")
            {
                foreach (var item in constraint.room)
                {
                    cmd = new SqlCommand("Insert into Constraintt(constraint_id, c_type, course_id, room_id)" + "values(@id, @type, @course, @room)", connect);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar, 10).Value = constraint.type;
                    cmd.Parameters.Add("@course", SqlDbType.VarChar, 10).Value = constraint.course;
                    cmd.Parameters.Add("@room", SqlDbType.VarChar, 5).Value = item.@ref;

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
            }
            else
            {
                foreach (var item in constraint.timeslot)
                {
                    cmd = new SqlCommand("Insert into Constraintt(constraint_id, c_type, course_id, timeslot_day, timeslot_period)" + "values(@id, @type, @course, @day, @period)", connect);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar, 10).Value = constraint.type;
                    cmd.Parameters.Add("@course", SqlDbType.VarChar, 10).Value = constraint.course;
                    cmd.Parameters.Add("@day", SqlDbType.VarChar, 5).Value = item.day;
                    cmd.Parameters.Add("@period", SqlDbType.VarChar, 5).Value = item.period;

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
            }
            return rez;
        }

        public List<Teacher> ReadTeachers()
        {
            List<Teacher> result = new List<Teacher>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("select * from teacher", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //ose menyra tjeter
                        //Teacher temp = new Teacher();
                        //temp.id = reader[0].ToString();
                        //temp.name = reader[1].ToString();
                        //result.Add(temp);
                        result.Add(new Teacher() { id = reader[0].ToString(), name = reader[1].ToString() });
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }

            return result;
        }

    }
}
