using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLgenerator.Data.Model;

namespace XMLgenerator.Connections.Database.Visualization
{
    public class VisualizationConnection
    {

        private SqlConnection connect;
        private SqlCommand cmd;
        private SqlDataReader reader;

        private string conString = "Data Source=127.0.0.1;Initial Catalog=XMLgeneratorDB;User ID=sa;Password=9323";
        //private string conString = "Data Source=192.168.0.19;Initial Catalog=XMLgeneratorDB;User ID=sa;Password=9323";

        public VisualizationConnection()
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

        public int CountRooms()
        {
            int result = 0;

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select count(id) from Room", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0]);
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
        public int CountRoomBuildings()
        {
            int result = 0;

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("select Count(DISTINCT building) from room", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0]);
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
        public Rooms ReadRoom()
        {
            Rooms rooms = new Rooms();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select * from Room", connect);
                try
                {
                    reader = cmd.ExecuteReader();
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

        public int CountCourses()
        {
            int result = 0;

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select count(id) from Course", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0]);
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
        public List<string> ReadTeacherFromCourse()
        {
            List<string> result = new List<string>();

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select DISTINCT teacherID from Course", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
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
        public int CountCoursesOfTeacher(string teacherID)
        {
            int result = 0;

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("select count(teacherID) from Course where teacherID='" + teacherID + "'", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0]);
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


        public int CountCurricula()
        {
            int result = 0;

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select count(curriculum_id) from Curricula", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0]);
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
        public int CountConstraint()
        {
            int result = 0;

            if (connect.State == ConnectionState.Open)
            {
                cmd = new SqlCommand("Select count(id) from Constraints", connect);
                try
                {
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0]);
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
