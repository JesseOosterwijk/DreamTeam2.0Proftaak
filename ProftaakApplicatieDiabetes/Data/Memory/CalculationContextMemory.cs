using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Memory
{
    public class CalculationContextMemory
    {
        public CalculationContextMemory()
        {
            //Test
        }

        public double CalculateMealtimeDose(double Weight, double TotalCarbs, double CurrentBloodSugar, double TargetBloodSugar)
        {
            CalculationClass calc = new CalculationClass();
            double mealtimedose = calc.CalculateCHO(TotalCarbs, Weight) + calc.CalculateSugarCorrection(CurrentBloodSugar, TargetBloodSugar, Weight);
            mealtimedose = Math.Round(mealtimedose);
            return mealtimedose;
        }
    }
}
