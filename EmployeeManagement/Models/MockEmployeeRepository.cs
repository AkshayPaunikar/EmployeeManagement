namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id = 1, Name="Akshay", Email="akshay@gmail.com", Department=Dept.IT},
                new Employee(){Id = 2, Name="Mrunal", Email="mrunal@gmail.com", Department=Dept.Payroll },
                new Employee(){Id = 3,Name="Ankit", Email="ankit@gmail.com", Department=Dept.HR },
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(x => x.Id)+1;
            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
