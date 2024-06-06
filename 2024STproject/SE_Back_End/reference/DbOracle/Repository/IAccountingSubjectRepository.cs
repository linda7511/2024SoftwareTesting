using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IAccountingSubjectRepository
    {
        public bool Add(AccountingSubject accountingSubject);

        public bool Delete(decimal id);

        public bool Update(AccountingSubject accountingSubject);

        public AccountingSubject? Get(decimal id);
    }
}
