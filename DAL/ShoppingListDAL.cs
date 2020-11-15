using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FitApka.Model;

namespace FitApka.DAL
{
    class ShoppingListDAL
    {
        private string _connectionString;
        private IConfiguration configuration;
        public ShoppingListDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
            configuration = iconfiguration;
        }

        public List<ShoppingList> GetList()
        {
            ProductDAL productDAL = new ProductDAL(configuration);
            var ShoppingLists = new List<ShoppingList>();
            string ShoppingListQuery = "SELECT * FROM [dbo].[ListaZakupow]";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(ShoppingListQuery, con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ShoppingLists.Add(new ShoppingList
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Name = rdr[1].ToString(),
                            //Weight = Convert.ToInt32(rdr[2]),
                            IdUser = Convert.ToInt32(rdr[2]),
                            ShoppingListProducts = productDAL.GetShoppingProducts(Convert.ToInt32(rdr[0]))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ShoppingLists;
        }

        public void AddShoppingList(ShoppingList newShoppingList)
        {
            string AddShoppingListQuery = "INSERT [dbo].[ListaZakupow] VALUES (@ParamID, @ParamName, @ParamIDUser)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddShoppingListQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", newShoppingList.Id);
                    cmd.Parameters.AddWithValue("@ParamName", newShoppingList.Name);
                    cmd.Parameters.AddWithValue("@ParamIDUser", newShoppingList.IdUser);
                    cmd.ExecuteNonQuery();
                }
                foreach(Product product in newShoppingList.ShoppingListProducts)
                {
                    AddShoppingListProduct(newShoppingList.Id, product.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddShoppingListProduct(int IDShoppingList, int IDProduct)
        {
            string AddShoppingListQuery = "INSERT [dbo].[ListaZakupowProdukty] VALUES (@ParamIDProduct, @ParamIDShoppingList)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddShoppingListQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamIDProduct", IDProduct);
                    cmd.Parameters.AddWithValue("@ParamIDShoppingList", IDShoppingList);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
