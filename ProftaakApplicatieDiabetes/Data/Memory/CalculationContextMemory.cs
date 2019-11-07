using System;

namespace Data.Memory
{
    public class CalculationContextMemory
    {
        public CalculationContextMemory()
        {
        }

        public int CalculateMealtimeDose(int Weight, int TotalCarbs, int CurrentBloodSugar, int TargetBloodSugar)
        {
            CalculationClass calc = new CalculationClass();
            double mealtimedose = Math.Round(calc.CalculateCHO(TotalCarbs, Weight) + calc.CalculateSugarCorrection(CurrentBloodSugar, TargetBloodSugar, Weight));
            return (int)mealtimedose;
        }
    }
}
