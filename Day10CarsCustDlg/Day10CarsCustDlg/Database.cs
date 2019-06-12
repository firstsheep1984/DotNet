using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10CarsCustDlg
{
    public class Database
    {
        private SqlConnection conn;

        // Note: Handle SqlException and SystemException when using constructor
        public Database()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ipd.BH210-12\Documents\2019-ipd17-dotnet\Day10CarsCustDlg\CarsDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }

        public List<Car> GetAllCars()
        {
            List<Car> result = new List<Car>();
            SqlCommand command = new SqlCommand("SELECT * FROM Cars", conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // while there is another record present
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string makeModel = (string)reader["MakeModel"];
                    double engineSizeL = (double)reader["EngineSize"];
                    // FIXME: decided what to do with parsing exception
                    FuelType fuelType = (FuelType)Enum.Parse(typeof(FuelType), (string)reader["FuelType"]);
                    Car car = new Car() { Id = id, MakeModel = makeModel, EngineSizeL = engineSizeL, FuelType = fuelType };
                    result.Add(car);
                }
            }
            return result;
        }

        public void AddCar(Car car)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Cars (MakeModel, EngineSize, FuelType) VALUES (@MakeModel, @EngineSize, @FuelType)", conn);
            command.Parameters.AddWithValue("@MakeModel", car.MakeModel);
            command.Parameters.AddWithValue("@EngineSize", car.EngineSizeL);
            command.Parameters.AddWithValue("@FuelType", car.FuelType.ToString());
            command.ExecuteNonQuery();
        }

        public void UpdateCar(Car car)
        {
            SqlCommand command = new SqlCommand("UPDATE Cars SET MakeModel=@MakeModel, EngineSize=@EngineSize, FuelType=@FuelType WHERE Id=@Id", conn);
            command.Parameters.AddWithValue("@MakeModel", car.MakeModel);
            command.Parameters.AddWithValue("@EngineSize", car.EngineSizeL);
            command.Parameters.AddWithValue("@FuelType", car.FuelType.ToString());
            command.Parameters.AddWithValue("@Id", car.Id);
            command.ExecuteNonQuery();
        }

        public void DeleteCar(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Cars WHERE Id=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }


    }
}

/*

CREATE TABLE [dbo].[Cars]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MakeModel] NVARCHAR(50) NOT NULL, 
    [EngineSize] FLOAT NOT NULL, 
    [FuelType] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [CK_Cars_Column] CHECK (FuelType IN ('Gasoline', 'Diesel', 'Hybrid', 'Electric', 'Propane', 'Alcohol'))
)


*/
