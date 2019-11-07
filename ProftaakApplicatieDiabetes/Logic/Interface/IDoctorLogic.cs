using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Logic.Interface
{
    public interface IDoctorLogic
    {
        IEnumerable<User> GetPatientsFromDoctorId(int doctorId);
    }
}
