using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLAssetsSummarizeRepository : IAssetsSummarizeRepository
    {
        private MyDbContext _context;

        public SQLAssetsSummarizeRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(AssetsSummarize assetsSummarize)
        {
            try
            {
                _context.AssetsSummarizes.Add(assetsSummarize);
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
                var assetsSummarize = _context.AssetsSummarizes.FirstOrDefault(a => a.AssetsSummarizeId == id);
                _context.AssetsSummarizes.Remove(assetsSummarize);
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

        public AssetsSummarize? Get(decimal id)
        {
            try
            {
                AssetsSummarize assetsSummarize = _context.AssetsSummarizes.FirstOrDefault(a => a.AssetsSummarizeId == id);
                if (assetsSummarize == null)
                {
                    return null;
                }
                return assetsSummarize;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(AssetsSummarize newAssets)
        {
            try
            {
                var existedAssets = _context.AssetsSummarizes.Find(newAssets.AssetsSummarizeId);
                if (existedAssets == null)
                {
                    Console.WriteLine("无对应的资产汇总信息 更新失败");
                    return false;
                }
                Type assetsType = typeof(AssetsSummarize);
                PropertyInfo[] properties = assetsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newAssets);
                    if (value != null)
                    {
                        property.SetValue(existedAssets, value);
                    }
                }
                _context.AssetsSummarizes.Update(existedAssets);
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
