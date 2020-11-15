using System;
using FitApka.Model;
using FitApka.View;
using FitApka.DAL;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using FitApka.Controller;
using NHibernate;

namespace FitApka
{
    #region OldVersion
    /*class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {

            GetAppSettingsFile();
            TrainingController trainingController = new TrainingController(_iconfiguration);
            MealController mealController = new MealController(_iconfiguration);
            ShoppingListController shoppingListController = new ShoppingListController(_iconfiguration);
            DietController dietController = new DietController(_iconfiguration);
            UserController userController = new UserController(_iconfiguration);
            ExerciseController exerciseController = new ExerciseController(_iconfiguration);
            ProductController productController = new ProductController(_iconfiguration);
            List<Meal> listMeal = mealController.GetList();
            //User user;
            UsersMeals diet = new UsersMeals();
            void View()
            {
                int idMenuSelected;
                string[] options = { "Użytkownik", "Posiłki", "Lista Zakupów", "Twórcy", "Wyjście" };
                string prompt = @"

                                                        ______ _ _              _             
                                                        |  ___(_) |            | |            
                                                        | |_   _| |_ __ _ _ __ | | ____ _     
                                                        |  _| | | __/ _` | '_ \| |/ / _` |    
                                                        | |   | | || (_| | |_) |   < (_| |   
                                                        \_|   |_|\__\__,_| .__/|_|\_\__,_|    
                                                                         | |                    
                                                                         |_|                    
                                                       --<< Fitapka wersja konsolowa v1.0>>--  
                                                       Użyj strzałek góra/dół do wyboru opcji, 
                                                       zatwierdź enterem                       
";

                Menu menu = new Menu(prompt, options);
                idMenuSelected = menu.Run();
                //User test = null;
                switch (idMenuSelected)
                {
                    case 0:
                        Person.View();
                        View();
                        break;
                    case 1:
                        Console.Clear();
                        diet.user = new User(Person.GetClient().Name, Person.GetClient().Email, Person.GetClient().Weight, Person.GetClient().Height, Person.GetClient().UserAge, Person.GetClient().Sex, Person.GetClient().UserExpectation.ExpectInt, Person.GetClient().UserExpectation.Activity);
                        diet.Meals = listMeal;
                        diet.GenerateDiet();
                        diet.WriteDiet();
                        Console.ReadKey();
                        View();
                        break;
                    case 2:
                        ListToBuy listToBuy = new ListToBuy();
                        listToBuy.diet = diet.diet;
                        listToBuy.View();
                        Console.ReadKey();
                        View();
                        break;
                    case 3:
                        About();
                        break;
                    case 4:
                        ExitApp();
                        break;
                    default:
                        break;
                }

            }



            void ExitApp()
            {
                Console.Clear();
                Console.WriteLine("Wciśnij dowolny klawisz aby zamknąć aplikacje");
                Console.ReadKey(true);
                Environment.Exit(0);
            }

            void About()
            {
                Console.Clear();
                Console.WriteLine(@"
Wykonali:
    Klaudia Matuszak
    Krystian Wysocki
    Michal Mlenko
");

                Console.ReadKey();
                Back();
            }

            void Back()
            {
                View();
            }

            View();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }


    }*/
    #endregion

    #region ORMVersion

    class Program
    {
        static void Main(string[] args)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            IQuery query = session.CreateQuery("FROM ORMExercise");

            IList<ORMExercise> exercises = query.List<ORMExercise>();
            session.Close();
            if (exercises.Count == 0)
                Console.WriteLine("Lista exercises jest pusta");
            else
            {
                foreach(ORMExercise x in exercises)
                {
                    Console.WriteLine(x.Id);
                    Console.WriteLine(x.Name);
                    Console.WriteLine(x.Description);
                    Console.WriteLine(x.Repetition);
                    Console.WriteLine("---------");
                }
            }

            Console.ReadKey();
        }
    }

    #endregion
}
