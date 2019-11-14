using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface IPatientToDoctorContext
    {
        int GetDoctorIdFromPatientId(int patientId);
        IEnumerable<User> GetPatientsFromDoctorId(int doctorId);
    }
}
