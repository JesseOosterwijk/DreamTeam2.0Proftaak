using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Couple : ICouple
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public Couple(int id, int doctorId, int patientId)
        {
            Id = id;
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}
