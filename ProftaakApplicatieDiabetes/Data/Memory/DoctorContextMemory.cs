using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Memory
{
    public class DoctorContextMemory : IDoctorContext
    {
        private List<Calculation> testCalcs = new List<Calculation>();
        private List<Couple> testCouples = new List<Couple>();

        public List<Calculation> UnsortedTestCalcs()
        {
            IEnumerable<Calculation> list = new List<Calculation>(testCalcs);
            return list.ToList();
        }

        public List<Calculation> SortedTestCalcs()
        {
            var list = testCalcs.OrderByDescending(c => c.Date);
            return list.ToList();
        }

        public void MakeCouple(User doctor, User patient)
        {
            testCouples.Add(new Couple(doctor, patient));
        }

        public IEnumerable<User> GetAllLinkedPatients(int userId)
        {
            List<User> linkedPatients = new List<User>();

            foreach (var item in testCouples.Where(c => c.Doctor.UserId == userId))
            {
                linkedPatients.Add(item.Patient);
            }

            return linkedPatients;
        }

        public IEnumerable<Calculation> GetPatientData(int patientId)
        {
            var calcs = testCalcs.Where(c => c.UserId == patientId);
            return calcs;
        }

        public void MakeTestCalcs(int amount, int id, string firstName, string lastName)
        {
            for (int i = 0; i < amount; i++)
            {
                testCalcs.Add(TestCalc(id, firstName, lastName));
            }
        }

        private Calculation TestCalc(int id, string firstName, string lastName)
        {
            return new Calculation(id, firstName, lastName);
        }

        public void AddToTestList(Calculation calc)
        {
            testCalcs.Add(calc);
        }
    }
}
