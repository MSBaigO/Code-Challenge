using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public interface ICompensationService
    {
        Employee GetById(String id);
        Compensation Create(Compensation employee);
        Employee Replace(Compensation originalCompensation, Compensation newCompensation);
    }
}
