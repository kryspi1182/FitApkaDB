using FitApka.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace FitApka.DAL
{
    public class TrainingDAL
    {
        private string _connectionString;
        private IConfiguration configuration;
        public TrainingDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
            configuration = iconfiguration;
        }
        public List<Training> GetList()
        {
            ExerciseDAL exerciseDAL = new ExerciseDAL(configuration);
            var listTraining = new List<Training>();
            string trainingQuery = "SELECT * FROM [dbo].[Treningi]";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(trainingQuery, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listTraining.Add(new Training
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Exercises = exerciseDAL.GetTrainingExercises(Convert.ToInt32(rdr[0])),
                            Duration = Convert.ToInt32(rdr[1]),
                            Name = rdr[2].ToString()
                        }) ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTraining;
        }

        public Training GetTraining(int IdTraining)
        {
            ExerciseDAL exerciseDAL = new ExerciseDAL(configuration);
            Training result = new Training();
            string TrainingQuery = "SELECT * FROM [dbo].[Treningi] WHERE ID_Treningi = @ParamID";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(TrainingQuery, con);
                    cmd.Parameters.AddWithValue("@ParamID", IdTraining);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        result.Id = Convert.ToInt32(rdr[0]);
                        result.Duration = Convert.ToInt32(rdr[1]);
                        result.Name = rdr[2].ToString();
                        result.Exercises = exerciseDAL.GetTrainingExercises(IdTraining);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void AddTraining(Training newTraining)
        {
            string AddTrainingQuery = "INSERT [dbo].[Treningi] VALUES (@ParamID, @ParamDuration, @ParamName)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddTrainingQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamID", newTraining.Id);
                    cmd.Parameters.AddWithValue("@ParamDuration", newTraining.Duration);
                    cmd.Parameters.AddWithValue("@ParamName", newTraining.Name);
                    cmd.ExecuteNonQuery();
                }
                foreach (Exercise Exercise in newTraining.Exercises)
                {
                    AddTrainingExercise(newTraining.Id, Exercise.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddTrainingExercise(int IDTraining, int IDExercise)
        {
            string AddTrainingExerciseQuery = "INSERT [dbo].[CwiczeniaTrening] VALUES (@ParamIDTraining, @ParamIDExercise)";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(AddTrainingExerciseQuery, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ParamIDTraining", IDTraining);
                    cmd.Parameters.AddWithValue("@ParamIDExercise", IDExercise);
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

