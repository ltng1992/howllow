using HollowService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HollowService.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeeAsync();
        Task<int> AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<int> DeleteEmployeeByIdAsync(int id);
        Task<int> UpdateEmployeeByIdAsync(int id, Employee employee);
    }
}
