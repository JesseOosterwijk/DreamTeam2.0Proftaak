using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Memory
{
    public class CalculationClass
    {
        public double CalculateCHO(int TotalCarbs, int Weight)
        {
            double coverage;
            double carbsPerInsuline = 500 / CalculateTotalDoseInsuline(Weight);
            coverage = TotalCarbs / carbsPerInsuline;
            return coverage;
        }

        public double CalculateSugarCorrection(int CurrentBloodSugar, int TargetBloodSugar, int Weight)
        {
            double sugardifference;
            sugardifference = CurrentBloodSugar - TargetBloodSugar;
            double correctionfactor = CalculateCorrectionFactor(Weight);
            double sugarcorrection = sugardifference / correctionfactor;
            return sugarcorrection;
        }

        public double CalculateCorrectionFactor(int Weight)
        {
            double correctionfactor;
            correctionfactor = 1800 / CalculateTotalDoseInsuline(Weight);
            return correctionfactor;
        }

        public double CalculateTotalDoseInsuline(int Weight)
        {
            double TDI;
            TDI = Weight * 0.55;
            return TDI;
        }
    }
}
