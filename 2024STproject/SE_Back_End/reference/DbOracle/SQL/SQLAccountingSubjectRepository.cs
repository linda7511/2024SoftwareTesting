using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLAccountingSubjectRepository : IAccountingSubjectRepository
    {
        private MyDbContext _context;

        public SQLAccountingSubjectRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(AccountingSubject accountingSubject)
        {
            try
            {
                _context.AccountingSubjects.Add(accountingSubject);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
            return true;
        }

        public bool Delete(decimal id)
        {
            try
            {
                var accountingSubject = _context.AccountingSubjects.FirstOrDefault(a => a.AccountSubjectId == id);
                _context.AccountingSubjects.Remove(accountingSubject);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
            return true;
        }

        public AccountingSubject? Get(decimal id)
        {
            try
            {
                AccountingSubject accountingSubject = _context.AccountingSubjects.FirstOrDefault(a => a.AccountSubjectId == id);
                if (accountingSubject == null)
                {
                    return null;
                }
                return accountingSubject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(AccountingSubject newSubject)
        {
            try
            {
                var existedSubject = _context.AccountingSubjects.Find(newSubject.AccountSubjectId);
                if (existedSubject == null)
                {
                    Console.WriteLine("无对应的资产科目信息 更新失败");
                    return false;
                }
                Type subjectType = typeof(AccountingSubject);
                PropertyInfo[] properties = subjectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newSubject);
                    if (value != null)
                    {
                        property.SetValue(existedSubject, value);
                    }
                }
                _context.AccountingSubjects.Update(existedSubject);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
