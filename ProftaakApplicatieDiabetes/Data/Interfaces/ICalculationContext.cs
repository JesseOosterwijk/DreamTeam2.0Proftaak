namespace Data.Contexts
{
    public interface ICalculationContext
    {
        decimal CalculateInsulin(decimal carbohydrates, decimal bloodsugar);
    }
}