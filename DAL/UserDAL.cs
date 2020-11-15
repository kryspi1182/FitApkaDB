using FitApka.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace FitApka.DAL
{
    public class UserDAL
    {
        private string _connectionString;
        public UserDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<User> GetList()
        {
            var listUser = new List<User>();
            string GetList = "SELECT * FROM [dbo].[Uzytkownik]";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(GetList, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listUser.Add(new User
                        (
                            //Convert.ToInt32(rdr[0]),
                            rdr[1].ToString(),
                            rdr[2].ToString(),
                            Convert.ToInt32(rdr[4]),
                            
                            (float)Convert.ToDouble(rdr[5]),
                            Convert.ToInt32(rdr[6]),
                            Convert.ToBoolean(rdr[3]),
                            2,
                            1.2
                        //Convert.ToInt32(rdr[7]),
                        //rdr[8].ToString()
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listUser;
        }

        public void AddUser(User newUser)
        {
            string AddUserQuery = "INSERT [dbo].[Uzytkownik] VALUES (@ParamID,@ParamName,@ParamEmail,@ParamSex,@ParamWeight,@ParamHeight,@ParamAge,@ParamExpectation,@ParamPassword)";
            int x;
            if (newUser.Sex)
                x = 1;
            else
                x = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddUserQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", newUser.Id);
                    cmd.Parameters.AddWithValue("@ParamName", newUser.Name);
                    cmd.Parameters.AddWithValue("@ParamEmail", newUser.Email);
                    cmd.Parameters.AddWithValue("@ParamSex", x);
                    cmd.Parameters.AddWithValue("@ParamWeight", newUser.Weight);
                    cmd.Parameters.AddWithValue("@ParamWeight", newUser.Height);
                    cmd.Parameters.AddWithValue("@ParamAge", newUser.UserAge);
                    cmd.Parameters.AddWithValue("@ParamExpectation", 0); //cel uzytkownika do zrobienia
                    cmd.Parameters.AddWithValue("@ParamPassword", newUser.Password);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditUser(int IDUser, User modifiedUser)
        {
            if (IDUser != modifiedUser.Id)
                return;


            int x;
            if (modifiedUser.Sex)
                x = 1;
            else
                x = 0;
            string EditUserQuery = "Update [dbo].[Uzytkownik] SET Imie = @ParamName, Email = @ParamEmail, Plec = @ParamSex, Waga = @ParamWeight, Wzrost = @ParamHeight, Wiek = @ParamAge, Cel = @ParamExpecation, Haslo = @ParamPassword WHERE ID_Uzytkownik = @ParamID";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(EditUserQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", modifiedUser.Id);
                    cmd.Parameters.AddWithValue("@ParamName", modifiedUser.Name);
                    cmd.Parameters.AddWithValue("@ParamEmail", modifiedUser.Email);
                    cmd.Parameters.AddWithValue("@ParamSex", x);
                    cmd.Parameters.AddWithValue("@ParamWeight", modifiedUser.Weight);
                    cmd.Parameters.AddWithValue("@ParamWeight", modifiedUser.Height);
                    cmd.Parameters.AddWithValue("@ParamAge", modifiedUser.UserAge);
                    cmd.Parameters.AddWithValue("@ParamExpectation", 0); //cel uzytkownika do zrobienia
                    cmd.Parameters.AddWithValue("@ParamPassword", modifiedUser.Password);
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

