using System;
using System.Collections.Generic;
using System.Text;
using Data.Contexts;

namespace Logic
{
    class MeasurementLogic : IMeasurementLogic
    {
        private readonly IMeasurementContext _context;

        public MeasurementLogic(IMeasurementContext context)
        {
            _context = context;
        }

        //hoi
        public decimal CalculateInulin(decimal carbohydrates, decimal bloodsugar)
        {
            return _context.CalculateInsulin(carbohydrates, bloodsugar);
        }
    }
}
