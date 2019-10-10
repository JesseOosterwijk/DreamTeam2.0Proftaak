using Models;

namespace Logic
{
    public interface ICalculationLogic
    {
        double CalculateMealtimeDose(ICalculation calc);
        Calculation GetSpecificAdvice(int id);
    }
}