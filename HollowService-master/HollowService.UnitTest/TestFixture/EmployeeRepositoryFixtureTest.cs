using System;
using HollowService.Model;
using Microsoft.EntityFrameworkCore;

namespace HollowService.UnitTest.TestFixture
{
    public class EmployeeRepositoryFixtureTest : IDisposable
    {
        private MyDBContext _dbContext;

        public EmployeeRepositoryFixtureTest()
        {
            // Init dbcontext and repository
            _dbContext = new MyDBContext(new DbContextOptions<MyDBContext>());
        }

        public MyDBContext DbContext
        {
            get { return this._dbContext; }
        }

        public void Dispose()
        {
            // Remove fake data from db after testing            
        }
    }
}