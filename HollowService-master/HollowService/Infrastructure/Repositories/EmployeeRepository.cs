
using HollowService.Interfaces;
using HollowService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HollowService.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyDBContext _db;
        private readonly ILogger _logger;
        public EmployeeRepository(MyDBContext dBContext, ILogger<EmployeeRepository> logger)
        {
            _db = dBContext;
            _logger = logger;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            int result = 0;
            try
            {
                await _db.Employees.AddAsync(employee);
                await _db.SaveChangesAsync();
                result = 1;
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Error when add employee by id: {0}: {1} : {2} ", ex.Message,
                           ex.InnerException != null ? ex.InnerException.Message : string.Empty,
                           ex.InnerException != null && ex.InnerException.InnerException != null
                               ? ex.InnerException.InnerException.Message
                               : string.Empty));
            }
          
            return result;
        }

        public async Task<int> DeleteEmployeeByIdAsync(int id)
        {
            int result = 0;
            try
            {
                var employee = new Employee { Id = id };
                _db.Employees.Attach(employee);
                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();
                result = 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error when delete employee by id: {0}: {1} : {2}", ex.Message,
                            ex.InnerException != null ? ex.InnerException.Message : string.Empty,
                            ex.InnerException != null && ex.InnerException.InnerException != null
                                ? ex.InnerException.InnerException.Message
                                : string.Empty));
            }
           
            return result;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee employee = new Employee();
            try
            {
                employee = await (from p in _db.Employees
                                  where p.Id == id
                                  select p).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Error when get employee by id: {0}: {1} : {2}", ex.Message,
                           ex.InnerException != null ? ex.InnerException.Message : string.Empty,
                           ex.InnerException != null && ex.InnerException.InnerException != null
                               ? ex.InnerException.InnerException.Message
                               : string.Empty));
            }
            return employee;

        }


        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            List<Employee> result = new List<Employee>();
            try
            {
                result = await _db.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error when get all employee: {0}: {1}", ex.Message,
                           ex.InnerException != null ? ex.InnerException.Message : string.Empty,
                           ex.InnerException != null && ex.InnerException.InnerException != null
                               ? ex.InnerException.InnerException.Message
                               : string.Empty));
            }
            return result;
        }

        public async Task<int> UpdateEmployeeByIdAsync(int id, Employee employee)
        {
            int result = 0;
            try
            {
                var update = new Employee { Id = id };
                _db.Employees.Attach(update);
                _db.Entry(update).State = EntityState.Modified;
                update.FirstName = employee.FirstName;
                update.LastName = employee.LastName;
                update.Title = employee.Title;
                await _db.SaveChangesAsync();
                result = 1;
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Error when update employee by id: {0}: {1}", ex.Message,
                          ex.InnerException != null ? ex.InnerException.Message : string.Empty,
                          ex.InnerException != null && ex.InnerException.InnerException != null
                              ? ex.InnerException.InnerException.Message
                              : string.Empty));
            }
           
            return result;
        }
    }
}
