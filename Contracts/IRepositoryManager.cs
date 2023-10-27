namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        ICommandRepository Command { get; }
        IPlayerRepository Player { get; }
        Task SaveAsync();
    }
}
