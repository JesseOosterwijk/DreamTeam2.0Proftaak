namespace Models
{
    public interface ICalculation
    {
        double Weight { get; }
        double TotalCarbs { get; }
        double CurrentBloodsugar { get; }
        double TargetBloodSugar { get; }
    }
}