using Models;
using System;
using System.Data.SqlClient;
using Data.Memory;
using System.Data;

namespace Data.Contexts
{
    public class CalculationContext : ICalculationContext
    {
        private readonly SqlConnection _con = Connection.GetConnection();
        private readonly CalculationClass calculationClass= new CalculationClass();

        public int CalculateMealtimeDose(ICalculation calc)
        {
            int insulinAdvice = (int)(calculationClass.CalculateCHO(calc.TotalCarbs, calc.Weight) + calculationClass.CalculateSugarCorrection(calc.CurrentBloodsugar, calc.TargetBloodSugar, calc.Weight));

            EncryptionKeyCreator keyCreator = new EncryptionKeyCreator();
            string encryptedString = keyCreator.KeyCreator();

            string query = "INSERT INTO [Measurement](UserId, Carbohydrates, Weight, CurrentBloodSugar, TargetBloodSugar, InsulinAdvice, Date, EncryptionKey) " +
                "Values (@UserId, @Carbohydrates, @Weight, @CurrentBloodSugar, @TargetBloodSugar, @InsulinAdvice, @Date, @Encryption)";

            using (SqlCommand cmd = new SqlCommand(query, _con))
            {
                _con.Open();

                cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = calc.UserBSN;
                cmd.Parameters.AddWithValue("@Carbohydrates", SqlDbType.VarChar).Value = Encrypting.Encrypt(calc.TotalCarbs.ToString(), encryptedString);
                cmd.Parameters.AddWithValue("@Weight", SqlDbType.VarChar).Value = Encrypting.Encrypt(calc.Weight.ToString(), encryptedString);
                cmd.Parameters.AddWithValue("@CurrentBloodSugar", SqlDbType.VarChar).Value = Encrypting.Encrypt(calc.CurrentBloodsugar.ToString(), encryptedString);
                cmd.Parameters.AddWithValue("@TargetBloodSugar", SqlDbType.VarChar).Value = Encrypting.Encrypt(calc.TargetBloodSugar.ToString(), encryptedString);
                cmd.Parameters.AddWithValue("@InsulinAdvice", SqlDbType.VarChar).Value = Encrypting.Encrypt(insulinAdvice.ToString(), encryptedString);
                cmd.Parameters.AddWithValue("@Encryption", SqlDbType.VarChar).Value = encryptedString;
                cmd.Parameters.AddWithValue("@Date", SqlDbType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                _con.Close();
            }
            return insulinAdvice;
        }

        public Calculation GetSpecificAdvice(int id)
        {
            int userBSN = 0;
            int weight = 0;
            int totalCarbs = 0;
            int currentBloodsugar = 0;
            int targetBloodSugar = 0;
            Calculation calc = new Calculation(userBSN, weight, totalCarbs, currentBloodsugar, targetBloodSugar);
            try
            {
                string query = "SELECT UserId, Carbohydrates, Weight, CurrentBloodSugar, TargetBloodSugar, EncryptionKey, InsulinAdvice FROM Measurement WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, _con);
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                _con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string encryptingString = reader.GetString(5);
                    calc.UserBSN = reader.GetInt32(0);
                    calc.TotalCarbs = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(1), encryptingString));
                    calc.Weight = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(2), encryptingString));
                    calc.CurrentBloodsugar = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(3), encryptingString));
                    calc.TargetBloodSugar = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(4), encryptingString));
                    calc.InsulinAdvice = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(6), encryptingString));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _con.Close();
            }
            
            return calc;
        }
    }
}
