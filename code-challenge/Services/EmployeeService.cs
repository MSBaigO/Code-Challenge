using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetReportingStructureById(string id){
            var employee = GetById(id);
            if(employee != null){
                return new ReportingStructure
                {
                    Employee = employee,
                    NumberOfReports = GetAllDirectReports(employee).Count
                };
            }
            return null;
        }

        List<string> GetAllDirectReports(Employee employee){

            List<string> directReportIds = new List<string>();
            Employee selectedDirectReport;

            foreach(Employee e in employee.DirectReports)
            {
                selectedDirectReport = _employeeRepository.GetById(e.EmployeeId);
                directReportIds.Add(selectedDirectReport.EmployeeId);

                if (selectedDirectReport.DirectReports != null && selectedDirectReport.DirectReports.Count > 0)
                    directReportIds.AddRange(GetAllDirectReports(selectedDirectReport));
            }

            return directReportIds;
        }
    }
}
