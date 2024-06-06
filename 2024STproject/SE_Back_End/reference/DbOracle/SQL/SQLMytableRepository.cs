using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLMytableRepository : IMytableRepository
	{
		private MyDbContext _context;

		public SQLMytableRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Mytable myTable)
		{
			try
			{
				_context.Mytables.Add(myTable);
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
				var myTable = _context.Mytables.FirstOrDefault(a => a.TableId == id);
				_context.Mytables.Remove(myTable);
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

		public Mytable? Get(decimal id)
		{
			try
			{
				Mytable myTable = _context.Mytables.FirstOrDefault(a => a.TableId == id);
				if (myTable == null)
				{
					return null;
				}
				return myTable;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

        public IEnumerable<Mytable>? GetAll()
		{
            try
            {
				return _context.Mytables;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// 桌位表 通过桌位id修改状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public bool? UpdateStatus(decimal tableId, string newStatus)
        {
            try
            {
                var table = _context.Mytables.Find(tableId);
                if (table != null)
                {
                    table.TableStatus = newStatus;
                    _context.SaveChanges();
                    return true;
                }
                return false; // If the table with given tableId doesn't exist.
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
        }

        public bool Update(Mytable newTable)
        {
            try
            {
                var existedTable = _context.Mytables.Find(newTable.TableId);
                if (existedTable == null)
                {
                    Console.WriteLine("无对应的桌位信息 更新失败");
                    return false;
                }
                Type tableType = typeof(Mytable);
                PropertyInfo[] properties = tableType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newTable);
                    if (value != null)
                    {
                        property.SetValue(existedTable, value);
                    }
                }
                _context.Mytables.Update(existedTable);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public IEnumerable<Mytable> GetLeisureTable()
		{
            try
            {
                var leisureTables = _context.Mytables.Where(a => a.TableStatus == "空闲");
                return leisureTables;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

    }
}
