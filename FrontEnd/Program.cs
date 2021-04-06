using BackEnd;
using System;
using System.Linq;
using Simulator;

namespace FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cage.CreateCage();
            //ExerciseCage.CreateExerciseCage();
            //ReadFromFile.ImportHamsters();
            //Cage.FillCages();
            //ExerciseCage.Exercise();
            //ExerciseCage.GoToCage();
            var simulator = new Simulator.Simulator();
            Console.ReadKey();
        }
    }
}