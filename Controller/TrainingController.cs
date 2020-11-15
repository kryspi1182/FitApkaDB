using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using FitApka.DAL;
using FitApka.Model;

namespace FitApka.Controller
{
    class TrainingController
    {
        TrainingDAL trainingDAL;
        public TrainingController(IConfiguration _configuration)
        {
            trainingDAL = new TrainingDAL(_configuration);
        }
        public List<Training> GetList()
        {
            List<Training> listTraining = trainingDAL.GetList();
            return listTraining;
        }

        public Training GetTraining(int IDTraining)
        {
            Training training = trainingDAL.GetTraining(IDTraining);
            return training;
        }

        public void AddTraining(Training newTraining)
        {
            trainingDAL.AddTraining(newTraining);
        }
    }
}
