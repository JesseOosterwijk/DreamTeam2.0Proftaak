using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    public interface IDoctorContext
    {
        //IEnumerable<User> GetPatientsFromDoctorId(int doctorId);
        IEnumerable<User> GetAllLinkedPatients(int userId);
        IEnumerable<Calculation> GetPatientData(int patientId);

    }
}
