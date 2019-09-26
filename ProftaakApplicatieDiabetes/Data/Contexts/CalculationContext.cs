using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;

namespace Data.Contexts
{
    public class CalculationContext : ICalculationContext
    {
        private readonly SqlConnection _con = Connection.GetConnection();


        public decimal CalculateInsulin(decimal carbohydrates, decimal bloodsugar)
        {
            throw new NotImplementedException();
        }
    }
}
