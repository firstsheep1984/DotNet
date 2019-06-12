using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08FriendsDB
{
    public class Database
    {
        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ipd.BH210-12\Documents\2019-ipd17-dotnet\Day08FriendsDB\FriendsDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }

        public List<Friend> GetAllFriends()
        {
            List<Friend> result = new List<Friend>();
            SqlCommand command = new SqlCommand("SELECT * FROM Friends", conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // while there is another record present
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];
                    int age = (int)reader["Age"];
                    Friend friend = new Friend() { Id = id, Name = name, Age = age };
                    result.Add(friend);
                }
            }
            return result;
        }

        public void AddFriend(Friend friend)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Friends (Name, Age) VALUES (@Name, @Age)", conn);
            command.Parameters.AddWithValue("@Name", friend.Name);
            command.Parameters.AddWithValue("@Age", friend.Age);
            command.ExecuteNonQuery();
        }

        public void UpdateFriend(Friend friend)
        {
            SqlCommand command = new SqlCommand("UPDATE Friends SET Name=@Name, Age=@Age WHERE Id=@Id", conn);
            command.Parameters.AddWithValue("@Name", friend.Name);
            command.Parameters.AddWithValue("@Age", friend.Age);
            command.Parameters.AddWithValue("@Id", friend.Id);
            command.ExecuteNonQuery();
        }

        public void DeleteFriend(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Friends WHERE Id=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }


    }
}
