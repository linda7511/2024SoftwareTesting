using DbOracle.Models;
using DbOracle.Repository;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLAssetsInformationRepository : IAssetsInformationRepository
    {
        private MyDbContext _context;

        public SQLAssetsInformationRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(AssetsInformation assetsInformation)
        {
            try
            {
                _context.AssetsInformations.Add(assetsInformation);
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
                var assetsInformation = _context.AssetsInformations.FirstOrDefault(a => a.AssetId == id);
                _context.AssetsInformations.Remove(assetsInformation);
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

        public AssetsInformation? Get(decimal id)
        {
            try
            {
                AssetsInformation assetsInformation = _context.AssetsInformations.FirstOrDefault(a => a.AssetId == id);
                if (assetsInformation == null)
                {
                    return null;
                }
                return assetsInformation;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<AssetsInformation>? GetAll()
        {
            try
            {

                return _context.AssetsInformations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<AssetsInformation>? GetByIfScraped(bool scraped)
        {
            try
            {
                var assetsInfoList = (
                    from asset in _context.AssetsInformations
                    where asset.Valid == "报废"
                    select asset
                    );
                if (scraped)
                    return assetsInfoList;
                else
                    return _context.AssetsInformations.Except(assetsInfoList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Asset_Location>? GetUnscrapedAssets()
        {
            try
            {
                var assetsInfoList = (
                    from asset in _context.AssetsInformations
                    from loc in _context.Locations
                    where asset.Valid == "正常" && asset.LocationId == loc.LocationId
                    select new Asset_Location
                    {
                        assetsInformation = asset,
                        location = loc
                    }
                );
                return assetsInfoList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }


        public bool Update(AssetsInformation newAssets)
        {
            try
            {
                var existedAssets = _context.AssetsInformations.Find(newAssets.AssetId);
                if (existedAssets == null)
                {
                    Console.WriteLine("无对应的资产信息 更新失败");
                    return false;
                }
                Type assetsType = typeof(AssetsInformation);
                PropertyInfo[] properties = assetsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newAssets);
                    if (value != null)
                    {
                        property.SetValue(existedAssets, value);
                    }
                }
                _context.AssetsInformations.Update(existedAssets);
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

public class Asset_Location
{
    public AssetsInformation assetsInformation { get; set; }

    public Location location { get; set; }
}
