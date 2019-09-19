using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProftaakApplicatieDiabetes.Models
{
    public class CalcModel
    {
        public int Carbohydrates { get; }
        public decimal Bloodsugar { get; }

        public CalcModel(int carbohydrates, decimal bloodsugar)
        {
            Carbohydrates = carbohydrates;
            Bloodsugar = bloodsugar;
        }
    }
}
