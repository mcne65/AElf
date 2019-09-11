using System.Collections.Generic;

namespace AElf.Contracts.Consensus.AEDPoS
{
    // ReSharper disable once InconsistentNaming
    public partial class AEDPoSContract
    {
        private readonly Dictionary<long, Round> _rounds = new Dictionary<long, Round>();
        private string _processingBlockMinerPubkey;
        private bool? _isMainChain;
    }
}