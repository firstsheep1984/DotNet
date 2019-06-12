using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09TodoDBApp
{
    public class Database
    {
        private SqlConnection conn;

        // Note: Handle SqlException and SystemException when using constructor
        public Database()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ipd.BH210-12\Documents\2019-ipd17-dotnet\Day09TodoDBApp\Day09TodoDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }

        public List<Todo> GetAllTodos(string order)
        {
            List<Todo> result = new List<Todo>();
            SqlCommand command = new SqlCommand("SELECT * FROM Todos ORDER BY " + order, conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // while there is another record present
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string task = (string)reader["Task"];
                    // FIXME: decided what to do with parsing exception
                    Status status = (Status)Enum.Parse(typeof(Status), (string)reader["Status"]);
                    DateTime dueDate = (DateTime)reader["DueDate"];
                    Todo todo = new Todo() { Id = id, Task = task, Status = status, DueDate = dueDate };
                    result.Add(todo);
                }
            }
            return result;
        }

        public void AddTodo(Todo todo)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Todos (Task, Status, DueDate) VALUES (@Task, @Status, @DueDate)", conn);
            command.Parameters.AddWithValue("@Task", todo.Task);
            command.Parameters.AddWithValue("@Status", todo.Status.ToString());
            command.Parameters.AddWithValue("@DueDate", todo.DueDate);
            command.ExecuteNonQuery();
        }

        public void UpdateTodo(Todo todo)
        {
            SqlCommand command = new SqlCommand("UPDATE Todos SET Task=@Task, Status=@Status, DueDate=@DueDate WHERE Id=@Id", conn);
            command.Parameters.AddWithValue("@Task", todo.Task);
            command.Parameters.AddWithValue("@Status", todo.Status.ToString());
            command.Parameters.AddWithValue("@DueDate", todo.DueDate);
            command.Parameters.AddWithValue("@Id", todo.Id);
            command.ExecuteNonQuery();
        }

        public void DeleteTodo(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Todos WHERE Id=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }


    }
}
