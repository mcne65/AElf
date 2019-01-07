using AElf.Common;
using AElf.Common.Serializers;
using AElf.Database;

namespace AElf.Kernel.Storages
{
    public class BlockBodyStore : KeyValueStoreBase<BlockChainKeyValueDbContext>, IBlockBodyStore
    {
        public BlockBodyStore(IByteSerializer byteSerializer, BlockChainKeyValueDbContext keyValueDbContext, string dataPrefix) 
            : base(byteSerializer, keyValueDbContext, GlobalConfig.BlockBodyPrefix)
        {
        }
    }
}
