using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Calculation : ICalculation
    {
        public int UserBSN { get; set; }

        public int Weight { get; set; }

        public int TotalCarbs { get; set; }

        public int CurrentBloodsugar { get; set; }

        public int TargetBloodSugar { get; set; }

        public int InsulinAdvice { get; set; }

        public Calculation(int userBSN, int weight, int totalCarbs, int currentBloodSugar, int targetBloodSugar)
        {
            UserBSN = userBSN;
            Weight = weight;
            TotalCarbs = totalCarbs;
            CurrentBloodsugar = currentBloodSugar;
            TargetBloodSugar = targetBloodSugar;
        }
    }
}
