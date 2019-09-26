using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.Contexts
{
    public class CalculationContext : ICalculationContext
    {
        private readonly SqlConnection _con = Connection.GetConnection();

        public double CalculateMealtimeDose(double weight, double totalCarbs, double CurrentBloodSugar, double targetBloodSugar, int userBSN)
        {
            string query = "INSERT INTO [Measurement](UserBSN, Carbohydrates, Weight, CurrentBloodSugar, TargetBloodSugar, InsulinAdvice) " +
                "Values (@UserBSN, @Carbohydrates, @Weight, @CurrentBloodSugar, @TargetBloodSugar, @InsulinAdvice)";

            double insulinAdvice = CalculateCHO(totalCarbs, weight) + CalculateSugarCorrection(CurrentBloodSugar, targetBloodSugar, weight);

            using (SqlCommand cmd = new SqlCommand(query, _con))
            {
                _con.Open();

                cmd.Parameters.AddWithValue("@UserBSN", userBSN);
                cmd.Parameters.AddWithValue("@Carbohydrates", totalCarbs);
                cmd.Parameters.AddWithValue("@Weight", weight);
                cmd.Parameters.AddWithValue("@CurrentBloodSugar", CurrentBloodSugar);
                cmd.Parameters.AddWithValue("@TargetBloodSugar", targetBloodSugar);
                cmd.Parameters.AddWithValue("@InsulinAdvice", insulinAdvice);

                cmd.ExecuteNonQuery();
                _con.Close();
            }
            return insulinAdvice;

        }

        private double CalculateCHO(double TotalCarbs, double Weight)
        {
            double coverage;
            double carbsPerInsuline = 500 / CalculateTotalDoseInsuline(Weight);
            coverage = TotalCarbs / carbsPerInsuline;
            return coverage;
        }

        private double CalculateSugarCorrection(double CurrentBloodSugar, double TargetBloodSugar, double Weight)
        {
            double sugardifference;
            sugardifference = CurrentBloodSugar - TargetBloodSugar;
            double correctionfactor = CalculateCorrectionFactor(Weight);
            double sugarcorrection = sugardifference / correctionfactor;
            return sugarcorrection;
        }

        private double CalculateCorrectionFactor(double Weight)
        {
            double correctionfactor;
            correctionfactor = 1800 / CalculateTotalDoseInsuline(Weight);
            return correctionfactor;
        }

        private double CalculateTotalDoseInsuline(double Weight)
        {
            double TDI;
            TDI = Weight * 0.55;
            return TDI;
        }
    }
}
