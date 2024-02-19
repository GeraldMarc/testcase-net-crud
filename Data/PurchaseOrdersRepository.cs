using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestCaseNetCrud.Models;

namespace TestCaseNetCrud.Data
{
    public class PurchaseOrdersRepository
    {
        private SqlConnection _connection;

        public PurchaseOrdersRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            _connection = new SqlConnection(connStr);
        }

        public List<PurchaseOrdersEntity> GetAll()
        {
            List<PurchaseOrdersEntity> listPurchaseOrders = new List<PurchaseOrdersEntity>();
            string queryString = "SELECT ID, Code, PurchaseDate, SupplierID, Remarks FROM PurchaseOrders";
            SqlCommand sql = new SqlCommand(queryString, _connection);

            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql);

            DataTable dt = new DataTable();

            sqlAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                listPurchaseOrders.Add(
                    new PurchaseOrdersEntity
                    {
                        ID = Convert.ToString(dr["ID"]),
                        Code = Convert.ToString(dr["Code"]),
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                        SupplierID = Convert.ToString(dr["SupplierID"]),
                        Remarks = Convert.ToString(dr["Remarks"])
                    }
                );
            }

            dt.Dispose();
            sqlAdapter.Dispose();
            sql.Dispose();

            return listPurchaseOrders;
        }

        public PurchaseOrdersViewModel GetById(string id)
        {
            string queryString = 
                "SELECT a.ID, a.Code, a.PurchaseDate, a.SupplierID, b.Name AS SupplierName, a.Remarks " +
                "FROM PurchaseOrders a " +
                "JOIN Suppliers b ON a.SupplierID = b.ID " +
                "WHERE a.ID = @ID";
            SqlCommand sql = new SqlCommand(queryString, _connection);
            sql.Parameters.AddWithValue("@ID", Guid.Parse(id));

            _connection.Open();
            try
            {
                SqlDataReader reader = sql.ExecuteReader();
                
                if (reader.Read())
                {
                    PurchaseOrdersEntity purchaseOrder = new PurchaseOrdersEntity
                    {
                        ID = reader["ID"].ToString(),
                        Code = reader["Code"].ToString(),
                        PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]),
                        SupplierID = reader["SupplierID"].ToString(),
                        SupplierName = reader["SupplierName"].ToString(),
                        Remarks = reader["Remarks"].ToString()
                    };

                    _connection.Close();
                    sql.Dispose();

                    string queryStringChild =
                        "SELECT a.ID, a.PurchaseOrderID, a.ProductID, b.Name AS ProductName, a.Quantity, a.UnitPrice " +
                        "FROM PurchaseOrderDetails a " +
                        "JOIN Products b ON a.ProductID = b.ID " +
                        "WHERE a.PurchaseOrderID = @PurchaseOrderID";
                    List<PurchaseOrderDetailsEntity> listPurchaseOrderDetails = new List<PurchaseOrderDetailsEntity>();
                    SqlCommand sqlChild = new SqlCommand(queryStringChild, _connection);
                    sqlChild.Parameters.AddWithValue("@PurchaseOrderID", Guid.Parse(id));

                    SqlDataAdapter sqlChildAdapter = new SqlDataAdapter(sqlChild);

                    DataTable dtChild = new DataTable();

                    sqlChildAdapter.Fill(dtChild);

                    foreach (DataRow drChild in dtChild.Rows)
                    {
                        listPurchaseOrderDetails.Add(
                            new PurchaseOrderDetailsEntity
                            {
                                ID = Convert.ToString(drChild["ID"]),
                                PurchaseOrderID = Convert.ToString(drChild["PurchaseOrderID"]),
                                ProductID = Convert.ToString(drChild["ProductID"]),
                                ProductName = Convert.ToString(drChild["ProductName"]),
                                Quantity = Convert.ToInt32(drChild["Quantity"]),
                                UnitPrice = Convert.ToDecimal(drChild["UnitPrice"])
                            }
                        );
                    }

                    PurchaseOrdersViewModel purchaseOrdersViewModel = new PurchaseOrdersViewModel 
                    {
                        PurchaseOrders = purchaseOrder,
                        PurchaseOrderDetails = listPurchaseOrderDetails
                    };

                    _connection.Close();
                    dtChild.Dispose();
                    sqlChildAdapter.Dispose();
                    sqlChild.Dispose();
                    return purchaseOrdersViewModel;
                }

                _connection.Close();
                sql.Dispose();
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                _connection.Close();
                sql.Dispose();
                return null;
            }

        }
    }
}