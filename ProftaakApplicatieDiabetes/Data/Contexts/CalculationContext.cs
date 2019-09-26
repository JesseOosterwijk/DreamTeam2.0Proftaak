using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;

namespace Data.Contexts
{
    public class CalculationContext : ICalculationContext
    {
        private readonly SqlConnection _con = Connection.GetConnection();

        public double CalculateMealtimeDose(ICalculation calc)
        {
            string query = "INSERT INTO [Measurement](UserBSN, Carbohydrates, Weight, CurrentBloodSugar, TargetBloodSugar, InsulinAdvice, Date) " +
                "Values (@UserBSN, @Carbohydrates, @Weight, @CurrentBloodSugar, @TargetBloodSugar, @InsulinAdvice, @Date)";

            double insulinAdvice = CalculateCHO(calc.TotalCarbs, calc.Weight) + CalculateSugarCorrection(calc.CurrentBloodsugar, calc.TargetBloodSugar, calc.Weight);

            using (SqlCommand cmd = new SqlCommand(query, _con))
            {
                _con.Open();

                cmd.Parameters.AddWithValue("@UserBSN", calc.UserBSN);
                cmd.Parameters.AddWithValue("@Carbohydrates", calc.TotalCarbs);
                cmd.Parameters.AddWithValue("@Weight", calc.Weight);
                cmd.Parameters.AddWithValue("@CurrentBloodSugar", calc.CurrentBloodsugar);
                cmd.Parameters.AddWithValue("@TargetBloodSugar", calc.TargetBloodSugar);
                cmd.Parameters.AddWithValue("@InsulinAdvice", insulinAdvice);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);

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
