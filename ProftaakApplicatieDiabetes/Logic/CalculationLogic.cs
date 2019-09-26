using System;
using System.Collections.Generic;
using System.Text;
using Data.Contexts;
using Data.Interfaces;
using Data.Memory;

namespace Logic
{
    public class CalculationLogic : ICalculationLogic
    {
        private readonly ICalculationContext _context;

        public CalculationLogic(ICalculationContext context)
        {
            _context = context;
        }

        public double CalculateMealtimeDose(double Weight, double TotalCarbs, double CurrentBloodSugar, double TargetBloodSugar, int userBSN)
        {
            return _context.CalculateMealtimeDose(Weight, TotalCarbs, CurrentBloodSugar, TargetBloodSugar, userBSN);
        }
    }
}
