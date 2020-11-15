using System;
using System.Collections.Generic;
using System.Text;
using FitApka.Model;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FitApka.DAL
{
    class DietDAL
    {
        private string _connectionString;
        public DietDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public Meal[,] GetUserDiet(int IdUser)
        {
            Meal[,] diet = new Meal[7, 5];
            
            int x;
            string ProductQuery = "SELECT P.ID_Posilki, P.Przepis, P.Kategoria, UP.Dzien FROM[dbo].[Posilki] AS P, [dbo].[UzytkownikPosilki] AS UP WHERE(P.ID_Posilki IN(SELECT ID_Posilki1 FROM[dbo].[UzytkownikPosilki] WHERE ID_Uzytkownik1 = 1)) AND (P.ID_Posilki = UP.ID_Posilki1)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(ProductQuery, con);
                    cmd.Parameters.AddWithValue("@TestParam1", IdUser);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Meal meal = new Meal
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Recipe = rdr[1].ToString(),
                            Category = rdr[2].ToString()
                        };
                        x = CategoryToInt(rdr[2].ToString());
                        if (x<5)
                        {
                            diet[Convert.ToInt32(rdr[3]), x] = meal;
                            
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return diet;
        }

        public void AddDiet(Diet newDiet)
        {
            string AddDietQuery = "INSERT [dbo].[UzytkownikPosilki] VALUES (@ParamDay, @ParamUserID, @ParamMealID)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(AddDietQuery, con);
                    SqlParameter Day = new SqlParameter("@ParamDay", SqlDbType.Int, 28);

                    SqlParameter UserID = new SqlParameter("@ParamUserID", SqlDbType.Int, 28);

                    SqlParameter MealID = new SqlParameter("@ParamMealID", SqlDbType.Int, 28);

                    for (int i = 0; i < 7; i++)
                    {
                        for(int j = 0; j < 5; j++)
                        {
                            Day.Value = i;
                            UserID.Value = newDiet.IdUser;
                            MealID.Value = newDiet.diet[i, j].Id;
                            cmd.Parameters.Add(Day);
                            cmd.Parameters.Add(UserID);
                            cmd.Parameters.Add(MealID);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CategoryToInt(string category)
        {
            int result;
            switch(category)
            {
                case "śniadanie":
                    result = 0;
                    break;
                case "drugie śniadanie":
                    result = 1;
                    break;
                case "obiad":
                    result = 2;
                    break;
                case "podwieczorek":
                    result = 3;
                    break;
                case "kolacja":
                    result = 4;
                    break;
                default:
                    result = 5;
                    break;
            }
            return result;
        }
    }
}
