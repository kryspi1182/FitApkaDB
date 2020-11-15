using System;
using System.Collections.Generic;
using System.Text;

namespace FitApka
{
    class ORMExercise
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Repetition { get; set; }
    }
}
