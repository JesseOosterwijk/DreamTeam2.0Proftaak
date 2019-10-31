using System;

namespace Data.Memory
{
    public class CalculationContextMemory
    {
        public CalculationContextMemory()
        {
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
