namespace Models
{
    public interface IMeasurement
    {
        int Carbohydrates { get; }
        decimal Bloodsugar { get; }
        decimal Insulin { get; }
    }
}