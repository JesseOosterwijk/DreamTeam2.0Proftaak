namespace Data.Interfaces
{
    public interface IMemory
    {
        double CalculateMealtimeDose(int Weight, int TotalCarbs, int CurrentBloodSugar, int TargetBloodSugar);
        double CalculateCHO(int Carbs, int Weight);
        double CalculateSugarCorrection(int CurrentBloodSugar, int TargetBloodSugar, int Weight);
        double CalculateCorrectionFactor(int Weight);
        double CalculateTotalDoseInsuline(int Weight);
    }
}
