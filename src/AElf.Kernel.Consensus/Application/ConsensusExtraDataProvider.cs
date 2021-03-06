using System.Linq;
using System.Threading.Tasks;
using AElf.Kernel.Blockchain.Application;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AElf.Kernel.Consensus.Application
{
    public class ConsensusExtraDataProvider : IBlockExtraDataProvider
    {
        private readonly IConsensusService _consensusService;
        public ILogger<ConsensusExtraDataProvider> Logger { get; set; }

        public ConsensusExtraDataProvider(IConsensusService consensusService)
        {
            _consensusService = consensusService;

            Logger = NullLogger<ConsensusExtraDataProvider>.Instance;
        }

        public async Task<ByteString> GetExtraDataForFillingBlockHeaderAsync(BlockHeader blockHeader)
        {
            if (blockHeader.Height == 1 || blockHeader.ExtraData.Any())
            {
                return null;
            }

            var consensusInformation = await _consensusService.GetConsensusExtraDataAsync(new ChainContext
            {
                BlockHash = blockHeader.PreviousBlockHash,
                BlockHeight = blockHeader.Height - 1
            });

            if (consensusInformation == null) return ByteString.Empty;

            Logger.LogDebug($"Consensus extra data generated. Of size {consensusInformation.Length}");

            return ByteString.CopyFrom(consensusInformation);
        }
    }
}