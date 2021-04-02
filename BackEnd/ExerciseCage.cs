using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ExerciseCage
    {
        public int Id { get; set; }
        public virtual ICollection<Hamster> Hamsters { get; set; }

        public static void GoExercise()
        {
            var dbContext = new DaycareContext();
            foreach (var hamster in dbContext.Hamsters)
            {
                
            }
        }
    }
}