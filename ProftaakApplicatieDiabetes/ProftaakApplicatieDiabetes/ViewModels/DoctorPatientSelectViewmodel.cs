using System.Collections.Generic;
using Models;

namespace ProftaakApplicatieDiabetes.ViewModels
{
    public class DoctorPatientSelectViewmodel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Calculation> Calculations { get; set; }
    }
}
