﻿using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        void Create(Employee emploee);
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        Employee GetEmployee(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);

    }
}
