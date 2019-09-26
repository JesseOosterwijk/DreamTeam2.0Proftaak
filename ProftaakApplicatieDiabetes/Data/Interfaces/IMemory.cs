using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IMemory
    {
        double CalculateMealtimeDose(double Weight, double TotalCarbs, double CurrentBloodSugar, double TargetBloodSugar);
        double CalculateCHO(double Carbs, double Weight);
        double CalculateSugarCorrection(double CurrentBloodSugar, double TargetBloodSugar, double Weight);
        double CalculateCorrectionFactor(double Weight);
        double CalculateTotalDoseInsuline(double Weight);
    }
}
