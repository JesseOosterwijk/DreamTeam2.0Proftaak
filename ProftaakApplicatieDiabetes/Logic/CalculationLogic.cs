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
        private readonly IMemory calMemory;

        public CalculationLogic(ICalculationContext context, IMemory _calMemory)
        {
            _context = context;
            calMemory = _calMemory;
        }

        public double CalculateMealtimeDose(double Weight, double TotalCarbs, double CurrentBloodSugar, double TargetBloodSugar)
        {
            return calMemory.CalculateMealtimeDose(Weight, TotalCarbs, CurrentBloodSugar, TargetBloodSugar);
        }
    }
}
