﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.Contexts
{
    public class CalculationContext : ICalculationContext
    {
        //private readonly SqlConnection _con = Connection.GetConnection();

        //public MeasurementContext(SqlConnection con)
        //{
        //    _con = con;
        //}

        public decimal CalculateInsulin(decimal carbohydrates, decimal bloodsugar)
        {
            throw new NotImplementedException();
        }
    }
}
