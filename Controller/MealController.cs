using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using FitApka.DAL;
using FitApka.Model;

namespace FitApka.Controller
{
    class MealController
    {
        MealDAL mealDAL;
        public MealController(IConfiguration _configuration)
        {
            mealDAL = new MealDAL(_configuration);
        }
        public List<Meal> GetList()
        {
            var listMeals = mealDAL.GetList();
            return listMeals;
        }

        public void AddMeal(Meal newMeal)
        {
            mealDAL.AddMeal(newMeal);
        }

        public void EditMeal(int IDMeal, Meal modifiedMeal)
        {
            mealDAL.EditMeal(IDMeal, modifiedMeal);
        }
    }
}
