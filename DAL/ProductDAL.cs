using FitApka.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace FitApka.DAL
{
    public class ProductDAL
    {
        private string _connectionString;
        public ProductDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<Product> GetList()
        {
            var listProduct = new List<Product>();
            string GetList = "SELECT * FROM [dbo].[Produkty]";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(GetList, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listProduct.Add(new Product
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Name = rdr[8].ToString(),
                            Weight = 1,
                            NutritionalValue = new NutritionalValue
                            {
                                Energy = Convert.ToInt32(rdr[1]),
                                Fat = Convert.ToInt32(rdr[2]),
                                Carbohydrate = Convert.ToInt32(rdr[3]),
                                Sugar = Convert.ToInt32(rdr[4]),
                                Fibre = Convert.ToInt32(rdr[5]),
                                Protein = Convert.ToInt32(rdr[6]),
                                Salt = Convert.ToInt32(rdr[7]),
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listProduct;
        }

        public List<Product> GetMealProducts(int IdMeal)
        {
            var listProducts = new List<Product>();
            string ProductQuery = "SELECT P.ID_Produkty, P.Kcal, P.Tluszcz, P.Weglowodany, P.Cukier, P.Blonnik, P.Bialko, P.Sol, P.Nazwa, PP.Ilosc FROM [dbo].[Produkty] AS P, [dbo].[ProduktyPosilki] AS PP WHERE (P.ID_Produkty IN (SELECT ID_Produkty1 FROM [dbo].[ProduktyPosilki] WHERE ID_Posilki1 =@TestParam1)) AND (P.ID_Produkty = PP.ID_Produkty1) AND (PP.ID_Posilki1 =@TestParam1)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(ProductQuery, con);
                    cmd.Parameters.AddWithValue("@TestParam1", IdMeal);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        listProducts.Add(new Product
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Name = rdr[8].ToString(),
                            Weight = Convert.ToInt32(rdr[9]),
                            NutritionalValue = new NutritionalValue
                            {
                                Energy = Convert.ToInt32(rdr[1]),
                                Fat = Convert.ToInt32(rdr[2]),
                                Carbohydrate = Convert.ToInt32(rdr[3]),
                                Sugar = Convert.ToInt32(rdr[4]),
                                Fibre = Convert.ToInt32(rdr[5]),
                                Protein = Convert.ToInt32(rdr[6]),
                                Salt = Convert.ToInt32(rdr[7]),
                            }
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listProducts;
        }

        public List<Product> GetShoppingProducts(int IdShoppingList)
        {
            var ShoppingListProducts = new List<Product>();
            string ShoppingListProductQuery = "SELECT * FROM [dbo].[Produkty] WHERE ID_Produkty IN (SELECT ID_Produkty1 FROM [dbo].[ListaZakupowProdukty] WHERE ID_ListaZakupow1 =@TestParam1)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(ShoppingListProductQuery, con);
                    cmd.Parameters.AddWithValue("@TestParam1", IdShoppingList);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        ShoppingListProducts.Add(new Product
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Name = rdr[8].ToString(),
                            Weight = 1,
                            NutritionalValue = new NutritionalValue
                            {
                                Energy = Convert.ToInt32(rdr[1]),
                                Fat = Convert.ToInt32(rdr[2]),
                                Carbohydrate = Convert.ToInt32(rdr[3]),
                                Sugar = Convert.ToInt32(rdr[4]),
                                Fibre = Convert.ToInt32(rdr[5]),
                                Protein = Convert.ToInt32(rdr[6]),
                                Salt = Convert.ToInt32(rdr[7]),
                            }
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ShoppingListProducts;
        }

        public void AddProduct(Product newProduct)
        {
            string AddProductQuery = "INSERT [dbo].[Produkty] VALUES (@ParamID, @ParamCalories, @ParamFat, @ParamCarbs, @ParamSugar, @ParamFibre, @ParamProtein, @ParamSalt, @ParamName)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddProductQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", newProduct.Id);
                    cmd.Parameters.AddWithValue("@ParamName", newProduct.Name);
                    cmd.Parameters.AddWithValue("@ParamCalories", newProduct.NutritionalValue.Energy);
                    cmd.Parameters.AddWithValue("@ParamFat", newProduct.NutritionalValue.Fat);
                    cmd.Parameters.AddWithValue("@ParamCarbs", newProduct.NutritionalValue.Carbohydrate);
                    cmd.Parameters.AddWithValue("@ParamSugar", newProduct.NutritionalValue.Sugar);
                    cmd.Parameters.AddWithValue("@ParamFibre", newProduct.NutritionalValue.Fibre);
                    cmd.Parameters.AddWithValue("@ParamProtein", newProduct.NutritionalValue.Protein);
                    cmd.Parameters.AddWithValue("@ParamSalt", newProduct.NutritionalValue.Salt);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditProduct(int IDProduct, Product modifiedProduct)
        {
            if (IDProduct != modifiedProduct.Id)
                return;

            string EditProductQuery = "Update [dbo].[Produkty] SET Kcal = @ParamCalories, Tluszcz = @ParamFat, Weglowodany = @ParamCarbs, Cukier = @ParamSugar, Blonnik = @ParamFibre, Bialko = @ParamProtein, Sol = @ParamSalt, Nazwa = @ParamName WHERE ID_Produkty = @ParamID";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(EditProductQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", modifiedProduct.Id);
                    cmd.Parameters.AddWithValue("@ParamName", modifiedProduct.Name);
                    cmd.Parameters.AddWithValue("@ParamCalories", modifiedProduct.NutritionalValue.Energy);
                    cmd.Parameters.AddWithValue("@ParamFat", modifiedProduct.NutritionalValue.Fat);
                    cmd.Parameters.AddWithValue("@ParamCarbs", modifiedProduct.NutritionalValue.Carbohydrate);
                    cmd.Parameters.AddWithValue("@ParamSugar", modifiedProduct.NutritionalValue.Sugar);
                    cmd.Parameters.AddWithValue("@ParamFibre", modifiedProduct.NutritionalValue.Fibre);
                    cmd.Parameters.AddWithValue("@ParamProtein", modifiedProduct.NutritionalValue.Protein);
                    cmd.Parameters.AddWithValue("@ParamSalt", modifiedProduct.NutritionalValue.Salt);
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
