using AElf.Common;
using AElf.Common.Serializers;
using AElf.Database;

namespace AElf.Kernel.Storages
{
    public class MinersStore : KeyValueStoreBase<StateKeyValueDbContext>, IMinersStore
    {
        public MinersStore(IByteSerializer byteSerializer, StateKeyValueDbContext keyValueDbContext, string dataPrefix) 
            : base(byteSerializer, keyValueDbContext, GlobalConfig.MinersPrefix)
        {
        }
    }
}
