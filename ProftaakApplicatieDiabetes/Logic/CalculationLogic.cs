using System;
using System.Collections.Generic;
using System.Text;
using Data.Contexts;
using Data.Interfaces;
using Data.Memory;
using Models;

namespace Logic
{
    public class CalculationLogic : ICalculationLogic
    {
        private readonly ICalculationContext _context;

        public CalculationLogic(ICalculationContext context)
        {
            _context = context;
        }

        public double CalculateMealtimeDose(ICalculation calc)
        {
            return _context.CalculateMealtimeDose(calc);
        }

        public Calculation GetSpecificAdvice(int id)
        {
            return _context.GetSpecificAdvice(id);
        }
    }
}
