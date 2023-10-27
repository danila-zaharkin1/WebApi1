using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private ICommandRepository _commandRepository;
        private IPlayerRepository _playerRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public ICommandRepository Command
        {
            get
            {
                if (_commandRepository == null)
                    _commandRepository = new CommandRepository(_repositoryContext);
                return _commandRepository;
            }
        }
        public IPlayerRepository Player
        {
            get
            {
                if (_playerRepository == null)
                    _playerRepository = new PlayerRepository(_repositoryContext);
                return _playerRepository;
            }
        }
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
