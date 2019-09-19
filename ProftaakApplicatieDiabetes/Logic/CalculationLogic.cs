using System;
using System.Collections.Generic;
using System.Text;
using Data.Contexts;
using Data.Memory;

namespace Logic
{
    class CalculationLogic : ICalculationLogic
    {
        private readonly IMeasurementContext _context;
        private readonly CalculationContextMemory calMemory;

        public CalculationLogic(IMeasurementContext context, CalculationContextMemory _calMemory)
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
