using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IAssetsInformationRepository
    {
        public bool Add(AssetsInformation assetsInformation);

        public bool Delete(decimal id);

        public bool Update(AssetsInformation assetsInformation);

        public AssetsInformation? Get(decimal id);

        public IEnumerable<AssetsInformation>? GetAll();

        public IEnumerable<AssetsInformation>? GetByIfScraped(bool scraped);

        public IEnumerable<Asset_Location>? GetUnscrapedAssets();
    }
}
