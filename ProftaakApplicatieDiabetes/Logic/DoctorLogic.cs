using System;
using System.Collections.Generic;
using System.Text;
using Logic.Interface;
using Data;
using Data.Contexts;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class DoctorLogic : IDoctorLogic
    {
        private IPatientToDoctorContext _patientToDoctorContext = new PatientToDoctorContextSQL();

        public IEnumerable<User> GetPatientsFromDoctorId(int doctorId)
        {
            return _patientToDoctorContext.GetPatientsFromDoctorId(doctorId);
        }
    }
}
