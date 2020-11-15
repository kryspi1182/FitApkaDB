using System;
using System.Collections.Generic;
using System.Text;
using FitApka.DAL;
using FitApka.Model;
using Microsoft.Extensions.Configuration;

namespace FitApka.Controller
{
    class DietController
    {
        DietDAL dietDal;
        public DietController(IConfiguration _configuration)
        {
            dietDal = new DietDAL(_configuration);
        }
        public Meal[,] GetUserDiet(int IdUser)
        {
            Meal[,] diet = dietDal.GetUserDiet(IdUser);
            return diet;
        }

        public void AddDiet(Diet newDiet)
        {
            dietDal.AddDiet(newDiet);
        }
    }
}
