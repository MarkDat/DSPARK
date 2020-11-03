
using GATE_GUARD.Model.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GATE_GUARD.Model.Dao
{
    public class UserDao
    {
        string con = "Data Source=.;Initial Catalog=DTUPARKING;Integrated Security=True";
        public SqlConnection connect;
        public UserDao()
        {
            connect = new SqlConnection(con);
        }
        public void addUser(string idS,string id,string name, int role,string faculty)
        {
            String query = "INSERT INTO dbo.USER_VEHICLE( IDS, ID, Name, Role, Faculty )VALUES  (@a,@b,@c,@d,@e)";

            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.Add("@a", idS);
            command.Parameters.Add("@b", id);
            command.Parameters.Add("@c", name);
            command.Parameters.Add("@d", role);
            command.Parameters.Add("@e", faculty);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
        }
        public void addPlate(string imgPath,string imgPlate,string txtPlate)
        {
            
            String query = "INSERT INTO dbo.LICENSE_PLATE(ImgPath, ImgPlatePath, TxtPlate) VALUES(@a,@b,@c)";

            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.Add("@a", imgPath);
            command.Parameters.Add("@b", imgPlate);
            command.Parameters.Add("@c", txtPlate);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
        }
        public void addParking(string id,string txtPlate,DateTime dtIn,bool status,DateTime dtOut)
        {
            String query = "INSERT INTO dbo.PARKING ( ID ,TxtPlate ,DateTimeParkingIn ,Status ,DateTimeParkingOut)VALUES(@a,@b,@c,@d,@e)";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.Add("@a", id);
            command.Parameters.Add("@b", txtPlate);
            command.Parameters.Add("@c", dtIn);
            command.Parameters.Add("@d", status);
            command.Parameters.Add("@e", dtOut);

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
        }
        public bool findUser(string id)
        {

            int check=-1;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT count(*) FROM dbo.USER_VEHICLE WHERE ID='" + id+"'", connect))
            {
                connect.Open();
                try
                {
                    check = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception)
                {
                    connect.Close();
                    return false;
                }
                
                connect.Close();
            }
            return check > 0 ? true : false;
        }
        public bool findPlate(string txtPlate)
        {
            int check = -1;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT count(*) FROM dbo.LICENSE_PLATE WHERE TxtPlate like '%" + txtPlate + "%'", connect))
            {
               connect.Open();
                try
                {
                    check = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception)
                {
                    connect.Close();
                    return false;
                }
                
                connect.Close();
            }
            return check > 0 ? true : false;
        }
        public bool checkParking(string id)
        {
            
            int check = -1;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT count(*) FROM dbo.PARKING WHERE ID = '" + id+"' AND Status='true'", connect))
            {
                connect.Open();
                try
                {
                    check = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception)
                {
                    connect.Close();
                    return false;
                }
               
                connect.Close();
            }
            
            return check > 0 ? true : false;


        }
        public bool setLeave(string id)
        {
            Console.WriteLine("id setLeave: "+id);
            int check=-1;
            //""
            connect.Open();
            using (var cmd = new SqlCommand("UPDATE dbo.PARKING SET Status='false',DateTimeParkingOut=@date WHERE ID=@id", connect))
            {
                cmd.Parameters.AddWithValue("@date",DateTime.Now);
                cmd.Parameters.AddWithValue("@id", id);
               check= cmd.ExecuteNonQuery();
            }
            connect.Close();
            return check == 0 ? false : true;
        }
        public List<NewUser> getListUser()
        {
            List<NewUser> newU = new List<NewUser>();
            connect.Open();
            SqlCommand sqlCommand = new SqlCommand(@"SELECT u.ID,u.Name,u.[Role],u.Faculty,p.DateTimeParkingIn,p.DateTimeParkingOut,p.[Status] FROM dbo.PARKING p,dbo.LICENSE_PLATE l,dbo.USER_VEHICLE u WHERE p.ID=u.ID AND p.TxtPlate = l.TxtPlate AND p.[Status] = 1", connect);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                NewUser u = new NewUser();
                u.ID = (string)reader["ID"];
                u.Name = (string)reader["Name"];
                u.Role = reader["Role"]+"";
                u.Faculty = (string)reader["Faculty"];
                u.DateTimeIn = (DateTime)reader["DateTimeParkingIn"];
                u.DateTimeOut = (DateTime)reader["DateTimeParkingOut"];
                u.Status =  (bool)reader["Status"]?"On":"Off";
                newU.Add(u);
            }
            connect.Close();
            return newU;
        }

    }
}
