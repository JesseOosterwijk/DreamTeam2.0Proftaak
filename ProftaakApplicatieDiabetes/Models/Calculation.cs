using System;

namespace Models
{
    public class Calculation : ICalculation
    {
        public int UserBSN { get; set; }

        public int UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public int Weight { get; set; }

        public int TotalCarbs { get; set; }

        public int CurrentBloodsugar { get; set; }

        public int TargetBloodSugar { get; set; }

        public int InsulinAdvice { get; set; }

        public DateTime Date { get; set; }

        public Calculation(int userBSN, int weight, int totalCarbs, int currentBloodSugar, int targetBloodSugar)
        {
            UserBSN = userBSN;
            Weight = weight;
            TotalCarbs = totalCarbs;
            CurrentBloodsugar = currentBloodSugar;
            TargetBloodSugar = targetBloodSugar;
        }

        public Calculation()
        {

        }
    }
}
