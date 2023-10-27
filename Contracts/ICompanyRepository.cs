using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        void Delete1(Company company);
        public IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges);

    }
}
