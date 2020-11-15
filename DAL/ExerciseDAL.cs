using FitApka.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace FitApka.DAL
{
    public class ExerciseDAL
    {
        private string _connectionString;
        public ExerciseDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<Exercise> GetList()
        {
            var listExercise = new List<Exercise>();
            string GetList = "SELECT * FROM [dbo].[Cwiczenia]";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(GetList, con);
                    
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listExercise.Add(new Exercise
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Name = rdr[1].ToString(),
                            Description = rdr[2].ToString(),
                            Repetition = Convert.ToInt32(rdr[3])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listExercise;
        }
        public Exercise GetExercise(int IdExercise)
        {
            Exercise result = new Exercise();
            string ExerciseQuery = "SELECT * FROM [dbo].[Cwiczenia] WHERE ID_Cwiczenia = @ParamID";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(ExerciseQuery, con);
                    cmd.Parameters.AddWithValue("@ParamID", IdExercise);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        result.Id = Convert.ToInt32(rdr[0]);
                        result.Name = rdr[1].ToString();
                        result.Description = rdr[2].ToString();
                        result.Repetition = Convert.ToInt32(rdr[3]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Exercise> GetTrainingExercises(int IdTraining)
        {
            var listExercises = new List<Exercise>();
            string ExerciseQuery = "SELECT * FROM [dbo].[Cwiczenia] WHERE ID_Cwiczenia IN (SELECT ID_Cwiczenia1 FROM [dbo].[CwiczeniaTrening] WHERE ID_Treningi1 =@TestParam1)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(ExerciseQuery, con);
                    cmd.Parameters.AddWithValue("@TestParam1", IdTraining);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        listExercises.Add(new Exercise
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Name = rdr[1].ToString(),
                            Description = rdr[2].ToString(),
                            Repetition = Convert.ToInt32(rdr[3])
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listExercises;
        }

        public void AddExercise(Exercise newExercise)
        {
            string AddExerciseQuery = "INSERT [dbo].[Cwiczenia] VALUES (@ParamID, @ParamName, @ParamDescription, @ParamRepetition)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddExerciseQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", newExercise.Id);
                    cmd.Parameters.AddWithValue("@ParamName", newExercise.Name);
                    cmd.Parameters.AddWithValue("@ParamDescription", newExercise.Description);
                    cmd.Parameters.AddWithValue("@ParamRepetition", newExercise.Repetition);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditExercise(int IDExercise, Exercise modifiedExercise)
        {
            if (IDExercise != modifiedExercise.Id)
                return;

            string EditExerciseQuery = "Update [dbo].[Cwiczenia] SET Nazwa = @ParamName, Opis = @ParamDescription, Powtorzenia = @ParamRepetition WHERE ID_Cwiczenia = @ParamID";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(EditExerciseQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamName", modifiedExercise.Name);
                    cmd.Parameters.AddWithValue("@ParamDescription", modifiedExercise.Description);
                    cmd.Parameters.AddWithValue("@ParamRepetition", modifiedExercise.Repetition);
                    cmd.Parameters.AddWithValue("@ParamID", IDExercise);
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

