using FitApka.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace FitApka.DAL
{
    public class MealDAL
    {
        private string _connectionString;
        private IConfiguration configuration;
        public MealDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
            configuration = iconfiguration;
        }
        public List<Meal> GetList()
        {
            ProductDAL productDAL = new ProductDAL(configuration);
            var listMeal = new List<Meal>();
            string mealQuery = "SELECT * FROM [dbo].[Posilki]";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(mealQuery, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listMeal.Add(new Meal
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            ProductList = productDAL.GetMealProducts(Convert.ToInt32(rdr[0])),
                            Recipe = rdr[1].ToString(),
                            Category = rdr[2].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listMeal;
        }

        public void AddMeal(Meal newMeal)
        {
            string AddMealQuery = "INSERT [dbo].[Posilki] VALUES (@ParamID, @ParamRecipe, @ParamCategory)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddMealQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", newMeal.Id);
                    cmd.Parameters.AddWithValue("@ParamRecipe", newMeal.Recipe);
                    cmd.Parameters.AddWithValue("@ParamCategory", newMeal.Category);
                    cmd.ExecuteNonQuery();
                }
                foreach(Product product in newMeal.ProductList)
                {
                    AddMealProduct(newMeal.Id, product);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddMealProduct(int IDMeal, Product addProduct)
        {
            string AddMealProductQuery = "INSERT [dbo].[ProduktyPosilki] VALUES (@ParamIDMeal, @ParamIDProduct, @ParamQuantity, @ParamUnit)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddMealProductQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamIDMeal", IDMeal);
                    cmd.Parameters.AddWithValue("@ParamIDProduct", addProduct.Id);
                    cmd.Parameters.AddWithValue("@ParamQuantity", addProduct.Weight);
                    cmd.Parameters.AddWithValue("@ParamUnit", "100g");
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EditMealProducts(int IDMeal, List<Product> modifiedProducts)
        {
            string DeleteMealProductQuery = "DELETE FROM [dbo].[ProduktyPosilki] WHERE ID_Posilki1 = @ParamID";

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(DeleteMealProductQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", IDMeal);
                    
                    cmd.ExecuteNonQuery();
                }
                foreach (Product product in modifiedProducts)
                {
                    AddMealProduct(IDMeal, product);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditMeal(int IDMeal, Meal modifiedMeal)
        {
            string EditMealQuery = "UPDATE [dbo].[Posilki] SET Przepis = @ParamRecipe, Kategoria = @ParamCategory WHERE ID_Posilki = @ParamID";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(EditMealQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", IDMeal);
                    cmd.Parameters.AddWithValue("@ParamRecipe", modifiedMeal.Recipe);
                    cmd.Parameters.AddWithValue("@ParamCategory", modifiedMeal.Category);

                    cmd.ExecuteNonQuery();
                }
                EditMealProducts(IDMeal, modifiedMeal.ProductList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
