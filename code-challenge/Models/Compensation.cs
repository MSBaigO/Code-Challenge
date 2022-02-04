using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        public Employee _Employee { get; set; }
        public Decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
