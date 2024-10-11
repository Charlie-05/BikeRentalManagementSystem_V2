using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BikeRentalManagement_V2
{
    internal class BikeRepository
    {
        static string ConnectionString = "server=(localdb)\\MSSQLLocalDB;Database=BikeRentalManagement";

        public void AddBike()
        {
            Console.WriteLine("Enter Bike ID");
            string BikeID = (Console.ReadLine());
            Console.WriteLine("Enter Bike Brand");
            string input = Console.ReadLine();
            string Brand = CapitalizeBrand(input);
            Console.WriteLine("Enter Bike Model");
            string Model = Console.ReadLine();
            Console.WriteLine("Enter Rental Price");
            decimal RentalPrice = decimal.Parse(Console.ReadLine());

            Bike bike = new Bike() { BikeId = BikeID, Brand = Brand, Model = Model, RentalPrice = RentalPrice };

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "insert into Bikes(BikeId ,Brand ,Model ,RentalPrice) values(@BikeId , @Brand , @Model , @RentalPrice)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BikeId", BikeID);
                    cmd.Parameters.AddWithValue("@Brand", Brand);
                    cmd.Parameters.AddWithValue("@Model", Model);
                    cmd.Parameters.AddWithValue("@RentalPrice", RentalPrice);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Bike successfully added");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }

        public string ReadAllBikes()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string str = "";
                conn.Open();
                string query = "SELECT * FROM bikes";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            str += $"BikeId : {reader["BikeId"]} Brand : {reader["Brand"]}  Model :  {reader["Model"]} RentalPrice : {reader["RentalPrice"]}\n";
                        }
                    }
                    return str;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public Bike ReadBikeById()
        {
            Console.WriteLine("Enter Bike ID");
            string BikeID = (Console.ReadLine());
            string str = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                { Bike bike = new Bike();   
                    conn.Open();
                    string query = "SELECT * FROM bikes WHERE BikeId = @BikeId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BikeId", BikeID);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            str = ($"BikeId : {reader["BikeId"]} Brand : {reader["Brand"]}  Model :  {reader["Model"]} RentalPrice : {reader["RentalPrice"]}");

                            bike.BikeId = reader["BikeId"].ToString();
                            bike.Brand = reader["Brand"].ToString();
                            bike.Model = reader["Model"].ToString();
                            bike.RentalPrice = (decimal)(reader["RentalPrice"]);
                            
                            
                        }
                    }
                    return (bike);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void UpdateBike()
        {
            Bike bike = this.ReadBikeById();

            if (bike != null)
            {
                Console.WriteLine("Enter New Bike Brand");
                string Brand = Console.ReadLine();
                Console.WriteLine("Enter New Bike Model");
                string Model = Console.ReadLine();
                Console.WriteLine("Enter New Rental Price");
                decimal RentalPrice = decimal.Parse(Console.ReadLine());

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "Update  Bikes  set  Brand=@Brand , Model = @Model , RentalPrice = @RentalPrice where BikeId = @BikeId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {                     
                        cmd.Parameters.AddWithValue("@Brand", Brand);
                        cmd.Parameters.AddWithValue("@Model", Model);
                        cmd.Parameters.AddWithValue("@RentalPrice", RentalPrice);
                        cmd.Parameters.AddWithValue("@BikeId", bike.BikeId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Bike successfully updated");
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
        }

        public void deleteBike()
        {
            Console.WriteLine("Enter Bike ID to be deleted");
            string BikeID = (Console.ReadLine());

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE  FROM bikes WHERE BikeId = @BikeId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BikeId", BikeID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0) {
                            Console.WriteLine("Bike Successfully deleted");
                        }
                        else
                        {
                            throw new Exception();
                        }
                      
                    }
                 
                }
            }
            catch (Exception ex)
            {
                return ;
            }

        }

        public string CapitalizeBrand(string name)
        {
            var firstLetter = name.Substring(0, 1);
            var remaining = name.Substring(1 , name.Length - 1);
            return $"{firstLetter.ToUpper()}{remaining}";
        }
    }
}
