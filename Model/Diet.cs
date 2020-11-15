using System;
using System.Collections.Generic;
using System.Text;

namespace FitApka.Model
{
    class Diet
    {
        public int IdUser { get; set; }
        public Meal[,] diet = new Meal[7, 5];
    }
}
