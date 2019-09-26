using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Memory
{
    public class CalculationClass
    {
        public double CalculateCHO(double TotalCarbs, double Weight)
        {
            double coverage;
            double carbsPerInsuline = 500 / CalculateTotalDoseInsuline(Weight);
            coverage = TotalCarbs / carbsPerInsuline;
            coverage = Math.Round(coverage);
            return coverage;
        }

        public double CalculateSugarCorrection(double CurrentBloodSugar, double TargetBloodSugar, double Weight)
        {
            double sugardifference;
            sugardifference = CurrentBloodSugar - TargetBloodSugar;
            double correctionfactor = CalculateCorrectionFactor(Weight);
            double sugarcorrection = sugardifference / correctionfactor;
            sugarcorrection = Math.Round(sugarcorrection);
            return sugarcorrection;
        }

        public double CalculateCorrectionFactor(double Weight)
        {
            double correctionfactor;
            correctionfactor = 1800 / CalculateTotalDoseInsuline(Weight);
            correctionfactor = Math.Round(correctionfactor);
            return correctionfactor;
        }

        public double CalculateTotalDoseInsuline(double Weight)
        {
            double TDI;
            TDI = Weight * 0.55;
            TDI = Math.Round(TDI);
            return TDI;
        }
    }
}
