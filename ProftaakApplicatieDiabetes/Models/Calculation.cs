using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Calculation : ICalculation
    {
        public int UserBSN { get; set; }

        public double Weight { get; set; }

        public double TotalCarbs { get; set; }

        public double CurrentBloodsugar { get; set; }

        public double TargetBloodSugar { get; set; }

        public Calculation(int userBSN, double weight, double totalCarbs, double currentBloodSugar, double targetBloodSugar)
        {
            UserBSN = userBSN;
            Weight = weight;
            TotalCarbs = totalCarbs;
            CurrentBloodsugar = currentBloodSugar;
            TargetBloodSugar = targetBloodSugar;
        }
    }
}
