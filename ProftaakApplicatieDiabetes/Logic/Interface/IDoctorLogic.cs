using System.Collections.Generic;
using Models;

namespace Logic.Interface
{
    public interface IDoctorLogic
    {
        //IEnumerable<User> GetPatientsFromDoctorId(int doctorId);
        IEnumerable<User> GetAllLinkedPatients(int userId);
    }
}
