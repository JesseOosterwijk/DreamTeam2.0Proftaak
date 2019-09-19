namespace Models
{
    public interface ICalculation
    {
        double Weight { get; set; }
        double TotalCarbs { get; set; }
        double CurrentBloodsugar { get; set; }
        double TargetBloodSugar { get; set; }
    }
}