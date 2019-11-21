using Models;
using System;

namespace Data.Memory
{
    public class CalculationContextMemory
    {
        public CalculationContextMemory()
        {

        }

        public int CalculateMealtimeDose(ICalculation calc)
        {
            CalculationClass calculation = new CalculationClass();
            double mealtimedose = Math.Round(calculation.CalculateCHO(calc.TotalCarbs, calc.Weight) + calculation.CalculateSugarCorrection(calc.CurrentBloodsugar, calc.TargetBloodSugar, calc.Weight));
            return (int)mealtimedose;
        }
    }
}
