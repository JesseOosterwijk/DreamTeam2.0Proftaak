namespace Logic
{
    public interface ICalculationLogic
    {
        double CalculateMealtimeDose(double Weight, double TotalCarbs, double CurrentBloodSugar, double TargetBloodSugar, int userBSN);
    }
}