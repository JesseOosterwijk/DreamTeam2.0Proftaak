using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IMemory
    {
        double CalculateMealtimeDose(double Weight, double TotalCarbs, double CurrentBloodSugar, double TargetBloodSugar);
    }
}
