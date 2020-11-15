using System;
using System.Collections.Generic;
using System.Text;

namespace FitApka.Model
{
    public class Training
    {
        public int Id { get; set; }
        public List<Exercise> Exercises { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
    }
}
