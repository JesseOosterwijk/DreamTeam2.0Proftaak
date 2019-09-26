using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Memory
{
    public class CalculationContextMemory : IMemory
    {
        public CalculationContextMemory()
        {
            MakeTestValues();
        }
        
        public double CalculateMealtimeDose(double Weight, double TotalCarbs, double CurrentBloodSugar, double TargetBloodSugar)
        {
            double mealtimedose = CalculateCHO(TotalCarbs, Weight) + CalculateSugarCorrection(CurrentBloodSugar, TargetBloodSugar, Weight);
            return mealtimedose;
        }

        public double CalculateCHO(double TotalCarbs, double Weight)
        {
            double coverage;
            double carbsPerInsuline = 500 / CalculateTotalDoseInsuline(Weight);
            coverage = TotalCarbs / carbsPerInsuline;
            return coverage;
        }

        public double CalculateSugarCorrection(double CurrentBloodSugar, double TargetBloodSugar, double Weight)
        {
            double sugardifference;
            sugardifference = CurrentBloodSugar - TargetBloodSugar;
            double correctionfactor = CalculateCorrectionFactor(Weight);
            double sugarcorrection = sugardifference / correctionfactor;
            return sugarcorrection;
        }

        public double CalculateCorrectionFactor(double Weight)
        {
            double correctionfactor;
            correctionfactor = 1800 / CalculateTotalDoseInsuline(Weight);
            return correctionfactor;
        }

        public double CalculateTotalDoseInsuline(double Weight)
        {
            double TDI;
            TDI = Weight * 0.55;
            return TDI;
        }

        public void MakeTestValues()
        {
            //Calculation values = new Calculation(70, 60, 220, 120);
        }
    }
}
