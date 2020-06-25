using HollowService.Interfaces;
using HollowService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HollowService.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

        }
        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            return await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task<int> DeleteEmployeeByIdAsync(int id)
        {
            int result = 0;
            Employee emp = await _employeeRepository.GetEmployeeByIdAsync(id);
            if(emp == null)
            {
                result = 2; //Not found

            }
            else 
            {
                result = await  _employeeRepository.DeleteEmployeeByIdAsync(id);

            }
            return result;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _employeeRepository.GetAllEmployeeAsync();
        }

        public async Task<int> UpdateEmployeeByIdAsync(int id, Employee employee)
        {
            int result = 0;
            Employee emp = await _employeeRepository.GetEmployeeByIdAsync(id);
            if(emp == null)
            {
                result = 2; //Not found

            }
            else 
            {
                result = await _employeeRepository.UpdateEmployeeByIdAsync(id, employee);

            }
            return result;
        }
    }
}
