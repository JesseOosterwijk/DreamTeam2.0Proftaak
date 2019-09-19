using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Calculation : ICalculation
    {
        public double Weight { get; set; }

        public double TotalCarbs { get; set; }

        public double CurrentBloodsugar { get; set; }

        public double TargetBloodSugar { get; set; }

        public Calculation(double weight, double totalCarbs, double currentBloodSugar, double targetBloodSugar)
        {
            Weight = weight;
            TotalCarbs = totalCarbs;
            CurrentBloodsugar = currentBloodSugar;
            TargetBloodSugar = targetBloodSugar;
        }
    }
}
