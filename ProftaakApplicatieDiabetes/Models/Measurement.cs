using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Measurement : IMeasurement
    {
        public int Carbohydrates { get; }
        public decimal Bloodsugar { get; }
        public decimal Insulin { get; }

        public Measurement(int carbohydrates, decimal bloodsugar, decimal insulin)
        {
            Carbohydrates = carbohydrates;
            Bloodsugar = bloodsugar;
            Insulin = insulin;
        }
    }
}
