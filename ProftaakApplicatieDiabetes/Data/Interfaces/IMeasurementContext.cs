namespace Data.Contexts
{
    public interface IMeasurementContext
    {
        decimal CalculateInsulin(decimal carbohydrates, decimal bloodsugar);
    }
}