using HollowService.Model;
using HollowService.Repository;
using HollowService.UnitTest.TestFixture;
using Xunit;

namespace HollowService.UnitTest.TestRepositories
{
    public class EmployeeRepositoryTest : IClassFixture<EmployeeRepositoryFixtureTest>
    {
        private EmployeeRepositoryFixtureTest _employeeRepositoryFixture;
        private EmployeeRepository _repository;

        public EmployeeRepositoryTest(EmployeeRepositoryFixtureTest employeeRepositoryFixture)
        {
            this._employeeRepositoryFixture = employeeRepositoryFixture;
            this._repository = new EmployeeRepository(employeeRepositoryFixture.DbContext, null);
        }

        [Fact]
        public void DbContextIsValid()
        {
            Assert.NotNull(_employeeRepositoryFixture.DbContext);
        }

        [Fact]
        public void GetEmployeeByIdAsync_SuccessfulResult()
        {
            // Begin Arrange
            // Init fake Employee data
            const int employeeId = 1;
            Employee fakeEmployeeData = new Employee();
            fakeEmployeeData.Id = employeeId;
            fakeEmployeeData.FirstName = "First Name Test";
            fakeEmployeeData.LastName = "Last Name Test";
            fakeEmployeeData.Title = "Title Test";

            // Add fake data to db for testing
            _employeeRepositoryFixture.DbContext.Employees.Add(fakeEmployeeData);
            _employeeRepositoryFixture.DbContext.SaveChanges();
            // End Arrange

            //Act
            var employee = _repository.GetEmployeeByIdAsync(employeeId);

            //Assert
            Assert.NotNull(employee);
            Assert.Equal(1, employee.Id);
        }
    }
}