using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TestCaseNetCrud.Models;
using System.Configuration;

namespace TestCaseNetCrud.Data
{
    public class SuppliersRepository
    {
        private SqlConnection _connection;

        public SuppliersRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            _connection = new SqlConnection(connStr);
        }

        public List<SuppliersEntity> GetAll()
        {
            string queryString = "SELECT ID, Code, Name, City FROM Suppliers";
            List<SuppliersEntity> listSuppliers = new List<SuppliersEntity>();
            SqlCommand sql = new SqlCommand(queryString, _connection);

            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql);

            DataTable dt = new DataTable();

            sqlAdapter.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                listSuppliers.Add(
                    new SuppliersEntity
                    {
                        ID = Convert.ToString(dr["ID"]),
                        Code = Convert.ToString(dr["Code"]),
                        Name = Convert.ToString(dr["Name"]),
                        City = Convert.ToString(dr["City"])
                    }
                );
            }

            dt.Dispose();
            sqlAdapter.Dispose();
            sql.Dispose();

            return listSuppliers;
        }

        public SuppliersEntity GetById(string id)
        {
            string queryString = "SELECT ID, Code, Name, City FROM Suppliers WHERE ID = @ID";
            SqlCommand sql = new SqlCommand(queryString, _connection);
            sql.Parameters.AddWithValue("@ID", Guid.Parse(id));

            _connection.Open();
            try
            {
                SqlDataReader reader = sql.ExecuteReader();
                
                if (reader.Read())
                {
                    SuppliersEntity supplier = new SuppliersEntity
                    {
                        ID = reader["ID"].ToString(),
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString(),
                        City = reader["City"].ToString()
                    };
                    
                    _connection.Close();
                    sql.Dispose();
                    return supplier;
                 }
                
                _connection.Close();
                sql.Dispose();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                _connection.Close();
                sql.Dispose();
                return null;
            }

        }

        public bool CreateSupplier(SuppliersEntity input)
        {
            string queryString = "INSERT INTO Suppliers (ID, Code, Name, City) VALUES (@ID, @Code, @Name, @City)";

            SqlCommand sql = new SqlCommand(queryString,_connection);
            sql.Parameters.AddWithValue("@ID", Guid.NewGuid());
            sql.Parameters.AddWithValue("@Code", input.Code);
            sql.Parameters.AddWithValue("@Name", input.Name);
            sql.Parameters.AddWithValue("@City", input.City);

            _connection.Open();
            try{
                sql.ExecuteNonQuery();
                _connection.Close();
                sql.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                _connection.Close();
                sql.Dispose();
                return false;
            }
        }

        public bool EditSupplier(string id, SuppliersEntity input)
        {
            string queryString = "UPDATE Suppliers SET Code=@Code, Name=@Name, City=@City WHERE ID=@ID";

            SqlCommand sql = new SqlCommand(queryString, _connection);
            sql.Parameters.AddWithValue("@ID", Guid.Parse(id));
            sql.Parameters.AddWithValue("@Code", input.Code);
            sql.Parameters.AddWithValue("@Name", input.Name);
            sql.Parameters.AddWithValue("@City", input.City);

            _connection.Open();
            try
            {
                sql.ExecuteNonQuery();
                _connection.Close();
                sql.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                _connection.Close();
                sql.Dispose();
                return false;
            }
        }

        public bool DeleteSupplier(string id)
        {
            string queryString = "DELETE FROM Suppliers WHERE ID=@ID";

            SqlCommand sql = new SqlCommand(queryString, _connection);
            sql.Parameters.AddWithValue("@ID", id);

            _connection.Open();
            try
            {
                sql.ExecuteNonQuery();
                _connection.Close();
                sql.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                _connection.Close();
                sql.Dispose();
                return false;
            }
        }
    }
}