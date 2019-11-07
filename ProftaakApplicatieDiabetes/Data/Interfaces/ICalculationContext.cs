using Models;

namespace Data.Contexts
{
    public interface ICalculationContext
    {
        int CalculateMealtimeDose(ICalculation calc);
        Calculation GetSpecificAdvice(int id);
    }
}