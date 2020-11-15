using FitApka.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace FitApka.DAL
{
    public class NutritionalValueDAL
    {
        private string _connectionString;
        public NutritionalValueDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<NutritionalValue> GetList()
        {
            var listNutritionalValue = new List<NutritionalValue>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("getNutritionalValues", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listNutritionalValue.Add(new NutritionalValue
                        {
                            //Id = Convert.ToInt32(rdr[0]),
                            Energy = Convert.ToInt32(rdr[1]),
                            Fat = Convert.ToInt32(rdr[2]),
                            Carbohydrate = Convert.ToInt32(rdr[3]),
                            Sugar = Convert.ToInt32(rdr[4]),
                            Fibre = Convert.ToInt32(rdr[5]),
                            Protein = Convert.ToInt32(rdr[6]),
                            Salt = Convert.ToInt32(rdr[7]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listNutritionalValue;
        }
    }
}

