using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Calculation : ICalculation
    {
        public int userBSN { get; set; }

        public double Weight { get; set; }

        public double TotalCarbs { get; set; }

        public double CurrentBloodsugar { get; set; }

        public double TargetBloodSugar { get; set; }

    }
}
