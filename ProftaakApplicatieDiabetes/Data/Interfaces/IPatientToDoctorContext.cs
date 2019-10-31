using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    interface IPatientToDoctorContext
    {
        int GetDoctorIdFromPatientId(int patientId);
        IEnumerable<User> GetPatientIdsFromDoctorId(int doctorId);
    }
}
