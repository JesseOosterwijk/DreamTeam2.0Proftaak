namespace Models
{
    public interface ICalculation
    {
        int UserBSN { get; set; }
        int Weight { get; set; }
        int TotalCarbs { get; set; }
        int CurrentBloodsugar { get; set; }
        int TargetBloodSugar { get; set; }
        int InsulinAdvice { get; set; }
    }
}