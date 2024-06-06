using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IAssetsSummarizeRepository
    {
        public bool Add(AssetsSummarize assetsSummarize);

        public bool Delete(decimal id);

        public bool Update(AssetsSummarize assetsSummarize);

        public AssetsSummarize? Get(decimal id);
    }
}
