using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProftaakApplicatieDiabetes.Models
{
    public class CalcModel
    {
        public int userBSN { get; set; }

        public double Weight { get; set; }

        public double TotalCarbs { get; set; }

        public double CurrentBloodsugar { get; set; }

        public double TargetBloodSugar { get; set; }

        public double Result { get; set; }
    }
}
