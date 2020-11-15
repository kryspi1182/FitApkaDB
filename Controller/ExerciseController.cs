using FitApka.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using FitApka.Model;

namespace FitApka.Controller
{
    class ExerciseController
    {
        ExerciseDAL exerciseDAL;
        
        public ExerciseController(IConfiguration _configuration)
        {
            exerciseDAL = new ExerciseDAL(_configuration);
        }

        public List<Exercise> GetList()
        {
            List<Exercise> exercises = exerciseDAL.GetList();
            return exercises;
        }

        public List<Exercise> GetTrainingExercises(int IDTraining)
        {
            List<Exercise> exercises = exerciseDAL.GetTrainingExercises(IDTraining);
            return exercises;
        }

        public Exercise GetExercise(int IDExercise)
        {
            Exercise exercise = exerciseDAL.GetExercise(IDExercise);
            return exercise;
        }

        public void AddExercise(Exercise newExercise)
        {
            exerciseDAL.AddExercise(newExercise);
        }

        public void EditExercise(int IDExercise, Exercise modifiedExercise)
        {
            exerciseDAL.EditExercise(IDExercise, modifiedExercise);
        }
    }
}
