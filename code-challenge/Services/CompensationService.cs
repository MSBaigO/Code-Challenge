using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService: ICompensationService
    {
        private readonly ICompensationService _compenastionRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationService compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            
        }

        public Compensation GetById(string id)
        {

        }

        public Compensation Replace(Compensation orignalCompensation, Compensation newCompensation)
        {
           
        }
    }
}
