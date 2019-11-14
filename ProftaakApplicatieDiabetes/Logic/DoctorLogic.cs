using System.Collections.Generic;
using Logic.Interface;
using Data.Contexts;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class DoctorLogic : IDoctorLogic
    {
        private readonly IDoctorContext _doctorContext;

        public DoctorLogic(IDoctorContext doctorContext)
        {
            _doctorContext = doctorContext;
        }

        //public IEnumerable<User> GetPatientsFromDoctorId(int doctorId)
        //{
        //    return _doctorContext.GetPatientsFromDoctorId(doctorId);
        //}

        public IEnumerable<User> GetAllLinkedPatients(int userId)
        {
            return _doctorContext.GetAllLinkedPatients(userId);
        }
    }
}
