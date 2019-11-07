using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ProftaakApplicatieDiabetes.ViewModels
{
    public class DoctorPatientSelectViewmodel
    {
        public List<User> Patients { get; set; }

        public DoctorPatientSelectViewmodel(List<User> patients)
        {
            Patients = patients;
        }
    }
}
