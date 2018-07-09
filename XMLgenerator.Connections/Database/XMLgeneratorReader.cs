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
    public class XMLgeneratorReader
    {
        private SqlConnection connect;
        private SqlCommand command;
        private SqlDataReader reader;

        private string connectionString = "Data Source=127.0.0.1;Initial Catalog=XMLgeneratorDB;User ID=sa;Password=9323";
        // private string connectionString = "Data Source=192.168.0.19;Initial Catalog=XMLgeneratorDB;User ID=sa;Password=9323";


        public XMLgeneratorReader()
        {
            connect = new SqlConnection(connectionString);
        }
        public void OpenConnection()
        {
            try
            {
                connect.Open();
            }
            catch (Exception)
            {
                connect.Close();
            }
        }
        public void CloseConnection()
        {
            connect.Close();
        }


        public Courses ReadCourse()
        {
            Courses courses = new Courses();

            if (connect.State == ConnectionState.Open)
            {
                command = new SqlCommand("Select * from Course", connect);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Course course = new Course();

                        course.id = reader[0].ToString();
                        course.teacher = reader[1].ToString();
                        course.lectures = reader[2].ToString();
                        course.min_days = reader[3].ToString();
                        course.students = reader[4].ToString();
                        course.double_lectures = reader[5].ToString();

                        courses.course.Add(course);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }
            return courses;
        }

        public Rooms ReadRoom()
        {
            Rooms rooms = new Rooms();

            if (connect.State == ConnectionState.Open)
            {
                command = new SqlCommand("Select * from Room", connect);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Room room = new Room();

                        room.id = reader[0].ToString();
                        room.size = reader[1].ToString();
                        room.building = reader[2].ToString();

                        rooms.room.Add(room);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }

            }
            return rooms;
        }

        private Curricula ReadCurriculaID()
        {
            Curricula curricula = new Curricula();

            if (connect.State == ConnectionState.Open)
            {
                command = new SqlCommand("Select * from Curricula", connect);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Curriculum curriculum = new Curriculum();
                        curriculum.id = reader[0].ToString();

                        curricula.curriculum.Add(curriculum);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }

            return curricula;
        }
        public Curricula ReadCurriculum()
        {
            Curricula curricula = ReadCurriculaID();

            if (connect.State == ConnectionState.Open)
            {
                int k = 0;
                foreach (var item in curricula.curriculum)
                {
                    command = new SqlCommand("Select * from Curriculum where curriculum_id='" + item.id + "'", connect);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Course1 course1 = new Course1();
                        course1.@ref = reader[1].ToString();
                        //ose
                        //item.course.Add(course1);
                        curricula.curriculum[k].course.Add(course1); //ose item.course.Add(course1);
                    }
                    reader.Close();
                    k++;
                }

            }
            return curricula;
        }

        public Constraints ReadConstraints(out List<int> IDs)
        {
            Constraints constraints = new Constraints();
            IDs = new List<int>();

            if (connect.State == ConnectionState.Open)
            {
                command = new SqlCommand("Select * from Constraints order by c_type", connect);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Data.Model.Constraint constraint = new Data.Model.Constraint();
                        IDs.Add(Convert.ToInt32(reader[0]));
                        constraint.type = reader[1].ToString();
                        constraint.course = reader[2].ToString();
                        constraints.constraint.Add(constraint);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }
            return constraints;
        }

        public Constraints GenerateAllConstraints()
        {
            Constraints constraints = new Constraints();
            List<int> IdOfConstraints = new List<int>();
            constraints = ReadConstraints(out IdOfConstraints);
            int k = 0;
            foreach (var item in constraints.constraint)
            {
                if (item.type == "period")
                {
                    constraints.constraint[k].timeslot = ReadTimeslotsFromID(IdOfConstraints[k]);
                }
                else
                {
                    constraints.constraint[k].room = ReadRoomsFromID(IdOfConstraints[k]);
                }
                k++;
            }
            //for (int i = 0; i < constraints.constraint.Count; i++)
            //{
            //    if (constraints.constraint[i].type == "period")
            //    {
            //        constraints.constraint[i].timeslot = ReadTimeslotsFromID(IdOfConstraints[i]);
            //    }
            //    else
            //    {
            //        constraints.constraint[i].room = ReadRoomsFromID(IdOfConstraints[i]);
            //    }
            //}

            return constraints;
        }
        public List<Timeslot> ReadTimeslotsFromID(int id)
        {
            List<Timeslot> timeslots = new List<Timeslot>();

            if (connect.State == ConnectionState.Open)
            {
                command = new SqlCommand("Select * from Constraintt where constraint_id='" + id + "'", connect);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Timeslot timeslot = new Timeslot();

                        timeslot.day = reader[4].ToString();
                        timeslot.period = reader[5].ToString();

                        timeslots.Add(timeslot);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }

            }

            return timeslots;
        }
        public List<Room1> ReadRoomsFromID(int id)
        {
            List<Room1> rooms = new List<Room1>();

            if (connect.State == ConnectionState.Open)
            {
                command = new SqlCommand("Select * from Constraintt where constraint_id ='" + id + "'", connect);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Room1 room = new Room1();

                        room.@ref = reader[3].ToString();

                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }

            }

            return rooms;
        }
    }
}
