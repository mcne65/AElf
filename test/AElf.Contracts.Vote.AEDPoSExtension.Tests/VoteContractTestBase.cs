using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AElf.Contracts.Consensus.AEDPoS;
using AElf.Contracts.TestKet.AEDPoSExtension;
using AElf.Contracts.TestKit;
using AElf.Kernel.Consensus;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;
using Shouldly;
using Volo.Abp.Threading;
using Xunit;

namespace AElf.Contract.Vote
{
    // ReSharper disable once InconsistentNaming
    public class VoteContractTestBase : AEDPoSExtensionTestBase
    {
        internal AEDPoSContractImplContainer.AEDPoSContractImplStub ConsensusStub =>
            GetTester<AEDPoSContractImplContainer.AEDPoSContractImplStub>(
                ContractAddresses[ConsensusSmartContractAddressNameProvider.Name],
                SampleECKeyPairs.KeyPairs[0]);

        public readonly Dictionary<Hash, Address> ContractAddresses;

        public VoteContractTestBase()
        {
            ContractAddresses = AsyncHelper.RunSync(() => BlockMiningService.DeploySystemContractsAsync(
                new Dictionary<Hash, byte[]>
                {
                    {VoteSmartContractAddressNameProvider.Name, Codes.Single(c => c.Key.Contains("Vote")).Value}
                }));
        }

        [Fact]
        public async Task AEDPoSExtensionTestingFrameworkTest()
        {
            // Check round information after initialization.
            {
                var round = await ConsensusStub.GetCurrentRoundInformation.CallAsync(new Empty());
                round.RoundNumber.ShouldBe(1);
                round.TermNumber.ShouldBe(1);
                round.RealTimeMinersInformation.Count.ShouldBe(AEDPoSExtensionConstants.InitialKeyPairCount);
            }

            await BlockMiningService.MineBlockAsync(new List<Transaction>());
            
            {
                var round = await ConsensusStub.GetCurrentRoundInformation.CallAsync(new Empty());
                round.RealTimeMinersInformation.Values.Count(m => m.OutValue != null).ShouldBe(1);
            }
            
            await BlockMiningService.MineBlockAsync(new List<Transaction>());
            
            {
                var round = await ConsensusStub.GetCurrentRoundInformation.CallAsync(new Empty());
                round.RealTimeMinersInformation.Values.Count(m => m.OutValue != null).ShouldBe(2);
            }

            await BlockMiningService.MineBlockAsync(new List<Transaction>
            {
                
            });

            {
                var round = await ConsensusStub.GetCurrentRoundInformation.CallAsync(new Empty());
                round.RealTimeMinersInformation.Values.Count(m => m.OutValue != null).ShouldBe(3);
            }

            for (int i = 0; i < 10; i++)
            {
                await BlockMiningService.MineBlockAsync(new List<Transaction>());
            }
            
            {
                var round = await ConsensusStub.GetCurrentRoundInformation.CallAsync(new Empty());
                round.RoundNumber.ShouldBe(3);
            }

        }
    }
}