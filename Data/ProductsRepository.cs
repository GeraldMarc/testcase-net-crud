using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TestCaseNetCrud.Models;
using System.Configuration;

namespace TestCaseNetCrud.Data
{
    public class ProductsRepository
    {
        private SqlConnection _connection;

        public ProductsRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            _connection = new SqlConnection(connStr);
        }

        public List<ProductsEntity> GetAll()
        {
            string queryString = "SELECT ID, Code, Name FROM Products";
            List<ProductsEntity> listProducts = new List<ProductsEntity>();
            SqlCommand sql = new SqlCommand(queryString, _connection);

            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql);

            DataTable dt = new DataTable();

            sqlAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                listProducts.Add(
                    new ProductsEntity
                    {
                        ID = Convert.ToString(dr["ID"]),
                        Code = Convert.ToString(dr["Code"]),
                        Name = Convert.ToString(dr["Name"])
                    }
                );
            }

            dt.Dispose();
            sqlAdapter.Dispose();
            sql.Dispose();

            return listProducts;
        }

        public ProductsEntity GetById(string id)
        {
            string queryString = "SELECT ID, Code, Name FROM Products WHERE ID = @ID";
            SqlCommand sql = new SqlCommand(queryString, _connection);
            sql.Parameters.AddWithValue("@ID", Guid.Parse(id));

            _connection.Open();
            try
            {
                SqlDataReader reader = sql.ExecuteReader();

                if (reader.Read())
                {
                    ProductsEntity product = new ProductsEntity
                    {
                        ID = reader["ID"].ToString(),
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString()
                    };

                    _connection.Close();
                    sql.Dispose();
                    return product;
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

        public bool CreateProduct(ProductsEntity input)
        {
            string queryString = "INSERT INTO Products (ID, Code, Name) VALUES (@ID, @Code, @Name)";

            SqlCommand sql = new SqlCommand(queryString, _connection);
            sql.Parameters.AddWithValue("@ID", Guid.NewGuid());
            sql.Parameters.AddWithValue("@Code", input.Code);
            sql.Parameters.AddWithValue("@Name", input.Name);

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

        public bool EditProduct(string id, ProductsEntity input)
        {
            string queryString = "UPDATE Products SET Code=@Code, Name=@Name WHERE ID=@ID";

            SqlCommand sql = new SqlCommand(queryString, _connection);
            sql.Parameters.AddWithValue("@ID", Guid.Parse(id));
            sql.Parameters.AddWithValue("@Code", input.Code);
            sql.Parameters.AddWithValue("@Name", input.Name);

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

        public bool DeleteProduct(string id)
        {
            string queryString = "DELETE FROM Products WHERE ID=@ID";

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
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                _connection.Close();
                sql.Dispose();
                return false;
            }
        }
    }
}