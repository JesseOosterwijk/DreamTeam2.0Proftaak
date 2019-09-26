namespace Data.Contexts
{
    public interface ICalculationContext
    {
        double CalculateMealtimeDose(double weight, double totalCarbs, double currentBloodSugar, double TargetBloodSugar, int userBSN);
    }
}