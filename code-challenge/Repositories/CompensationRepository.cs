﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class CompensationRespository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRespository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            var compensation = _compensationContext.Compensations.AsEnumerable().Where(c => c.EmployeeId == id);
            foreach(Compensation c in _compensationContext.Compensations){
                Console.WriteLine("c.id: "+ c.EmployeeId);
            }
            return compensation.SingleOrDefault(); 
        }

        public List<Compensation> GetAll()
        {
            return _compensationContext.Compensations.ToList();
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }
    }
}
