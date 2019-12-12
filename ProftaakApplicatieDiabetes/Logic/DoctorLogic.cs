using System.Collections.Generic;
using Logic.Interface;
using Data.Interfaces;
using Models;
using System.Linq;

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

        public IEnumerable<Calculation> GetPatientData(int patientId)
        {
            List<Calculation> calculatons = new List<Calculation>();

            calculatons.AddRange(_doctorContext.GetPatientData(patientId));
            calculatons = calculatons.OrderByDescending(c => c.Date).ToList();

            return calculatons;
        }
    }
}
