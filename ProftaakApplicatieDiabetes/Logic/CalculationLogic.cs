using Data.Contexts;
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
            if (calc.TargetBloodSugar > calc.CurrentBloodsugar)
            {
                return 0;
            }
            else
            {
                return _context.CalculateMealtimeDose(calc);
            }
        }

        public Calculation GetSpecificAdvice(int id)
        {
            return _context.GetSpecificAdvice(id);
        }
    }
}
