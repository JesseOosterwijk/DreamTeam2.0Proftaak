using Models;

namespace Data.Contexts
{
    public interface ICalculationContext
    {
        double CalculateMealtimeDose(ICalculation calc);
    }
}