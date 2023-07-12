using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.Configuration.CSAttributes
{
    public class CSAttributesAppServiceTests : ConfigurationApplicationTestBase
    {
        private readonly ICSAttributesAppService _cSAttributesAppService;
        private readonly IRepository<CSAttribute, Guid> _cSAttributeRepository;

        public CSAttributesAppServiceTests()
        {
            _cSAttributesAppService = GetRequiredService<ICSAttributesAppService>();
            _cSAttributeRepository = GetRequiredService<IRepository<CSAttribute, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _cSAttributesAppService.GetListAsync(new GetCSAttributesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("d6723d82-c265-4f07-9328-80c8cea6eddc")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("7e1208c2-5d95-4066-91e8-3fd4cb98788d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cSAttributesAppService.GetAsync(Guid.Parse("d6723d82-c265-4f07-9328-80c8cea6eddc"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d6723d82-c265-4f07-9328-80c8cea6eddc"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CSAttributeCreateDto
            {
                AttributeID = "485fac1e5f",
                Description = "ed0a3047ca4b49ef928d975ba3f981bef5332d308dd34bac89f0db0341dd",
                ControlType = default,
                EntryMask = "efa6f710cd6f45258b0570f4ce37dde0f9ca1c461a4d41b6a7c627ee9108",
                RegExp = "584c22938b844fb0852583503073b89773df11422ccf44e5a357f3503fd6aadb23746b411f98452b9dc9de177853ffca72721ea906354b548e1af390609fd4e84215d8de63bd423e80ee163db92c8495fe67a2c29fb841fa983bd004bddf0f8823ce45bb6f534d4687ce539f1d56a59af677e19fe84c439e8bb8f79205e9c19",
                List = "153ffb3d",
                IsInternal = true,
                ContainsPersonalData = true,
                ObjectName = "0b137d73ead8458d925265c83cadeb387172dab35b4c48ba9419e5c1793a7ba332d65200b2404cf383b8c90e276c288da92302c32aeb48e893efe6d66429d8cb0b9a1cc3d222428fa6bb37d32faa558271ddd4b017a746a98c5c3ef66dbbadc1df42f579a348476583ac6b1e57e135cb4a49879491e346a2912a520492df3fc4b6a989c271c44cfdb9be3da0132982233b0a16634d7e4021b0942192c6e5127ed7ad84ff07b34010b0f95989f161a1a3640427f1843444cd802f0921dd3013474ebd14fb2690488db8edc4ccaa86b5e4a61406a875d44aaaa775ecb48fab16915fb5b4286e034b9a9f0e06274547babbd286b486c2cb41ab912b4b00a43a68e4",
                FieldName = "13fafafc953e48a8ad474493058082ab39b248be6c664affbcd55db985b27d3d24748d565bf7444c80621ff1894e34cafe77a2a9f4854a109206edb1268dca86a6bd1858dcd049cd9014e68b2d8a3e66bba1cb5186d340dc87ab408caea957955a58219b1661404a98685cda09b5e41f6a2d4f63bd214041882d2c66dacd192673ce9510f2914a71bb6afc517be0cd3860187c48632348b894821c436374f8e957ee6a0f824245c2a3f58ba24e804dc438a7bf22bc444c82942bb69114d25a548b77008fbf5e44be8020364df8a3fb8880ea811ffaea4c3aa541c244bcc9cd9239560f8530654090b2082020e3a6c30cb9ad4a2ed59e4ec6b859f76f6e0cbcc4"
            };

            // Act
            var serviceResult = await _cSAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _cSAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttributeID.ShouldBe("485fac1e5f");
            result.Description.ShouldBe("ed0a3047ca4b49ef928d975ba3f981bef5332d308dd34bac89f0db0341dd");
            result.ControlType.ShouldBe(default);
            result.EntryMask.ShouldBe("efa6f710cd6f45258b0570f4ce37dde0f9ca1c461a4d41b6a7c627ee9108");
            result.RegExp.ShouldBe("584c22938b844fb0852583503073b89773df11422ccf44e5a357f3503fd6aadb23746b411f98452b9dc9de177853ffca72721ea906354b548e1af390609fd4e84215d8de63bd423e80ee163db92c8495fe67a2c29fb841fa983bd004bddf0f8823ce45bb6f534d4687ce539f1d56a59af677e19fe84c439e8bb8f79205e9c19");
            result.List.ShouldBe("153ffb3d");
            result.IsInternal.ShouldBe(true);
            result.ContainsPersonalData.ShouldBe(true);
            result.ObjectName.ShouldBe("0b137d73ead8458d925265c83cadeb387172dab35b4c48ba9419e5c1793a7ba332d65200b2404cf383b8c90e276c288da92302c32aeb48e893efe6d66429d8cb0b9a1cc3d222428fa6bb37d32faa558271ddd4b017a746a98c5c3ef66dbbadc1df42f579a348476583ac6b1e57e135cb4a49879491e346a2912a520492df3fc4b6a989c271c44cfdb9be3da0132982233b0a16634d7e4021b0942192c6e5127ed7ad84ff07b34010b0f95989f161a1a3640427f1843444cd802f0921dd3013474ebd14fb2690488db8edc4ccaa86b5e4a61406a875d44aaaa775ecb48fab16915fb5b4286e034b9a9f0e06274547babbd286b486c2cb41ab912b4b00a43a68e4");
            result.FieldName.ShouldBe("13fafafc953e48a8ad474493058082ab39b248be6c664affbcd55db985b27d3d24748d565bf7444c80621ff1894e34cafe77a2a9f4854a109206edb1268dca86a6bd1858dcd049cd9014e68b2d8a3e66bba1cb5186d340dc87ab408caea957955a58219b1661404a98685cda09b5e41f6a2d4f63bd214041882d2c66dacd192673ce9510f2914a71bb6afc517be0cd3860187c48632348b894821c436374f8e957ee6a0f824245c2a3f58ba24e804dc438a7bf22bc444c82942bb69114d25a548b77008fbf5e44be8020364df8a3fb8880ea811ffaea4c3aa541c244bcc9cd9239560f8530654090b2082020e3a6c30cb9ad4a2ed59e4ec6b859f76f6e0cbcc4");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CSAttributeUpdateDto()
            {
                AttributeID = "da0d99412f",
                Description = "7446a43f9621454aaed614c035a15cac8627196a7d234f528a901158db88",
                ControlType = default,
                EntryMask = "d35ee54fb4bf4406b975043bdade812752ebea1986474404a75bd13ae2a5",
                RegExp = "87f8b81b95f74a87a59bbeb8c1d45d16d1af37bc11034735a74fd7626021a1f310344abf526941b183cd807b1e25ae7a1b779cdd222240bf92fb2eb4db80071fe6a3be3e4d4748adba91dd90425226fda8d684d32714404cbe0868b52d4af2e419cb09e9164d46678a8a6da44771ebff9597322ca658441c9334cb956669b38",
                List = "9d0b5168f2",
                IsInternal = true,
                ContainsPersonalData = true,
                ObjectName = "8af320309c814ca182dd92545ac5087e7b5d8dc137e4435090cc562bbb42afd180d07ff4bf5a4a6094f76c2ce776c7e32dcd50373d5641a086437383bd70eec3fc00311b861941f0bdec7b0dde148c3a269a54fd95a24e7c93e46a4f1236f5b25f1b18681f4b4ada9b0d8b82d3f925d30bfbe54debec481ca90293d647354caf2fdab20479824a9f9517a699406324b089d42919e2d2425c8b205f9cb867ab7af01617eb3c374bf98a4fed6a58c236347b0b0820dea04ecda41d806dc71a5e39fae7c1a5138d40c48c520b78630ce7bd10105f646df8417a9fb01342304ea33cf2c333ac3dfe4c74b8528604487193f61567bd4ac78445d99a244b2fbf07e554",
                FieldName = "4e7ea090fc3f4e86b9099f5f172c86cf006c1be40e92402196f803bd4f717831c4cc63cb328046b79e6589695b840f166c89d220e11b43bba340baaba9446f58f35ba137cd02498cb2e8706d86207cd4432d685dcdb9402abcbefa43e580e03901436b11412648749abdf638b1c7b8e8979b8841beef4dc997f229169e828dde655c4bddffa04e02ad612a1d001fa40c8308eb748c244ca5a2a5bdf6ee04099dd07c5426a3224237ae4177d0f6a15d6745d79c7f72de45af8c436f6a68b654f1eb8f7d7ea5bd4fd5adcdcf96b94c5f8d49d8bb8d520e49eb8ea9a8f92f76ac10eb8bb00cbf834b0a8fd684b7057bc598775c423be24e46cdbd716265a0534492"
            };

            // Act
            var serviceResult = await _cSAttributesAppService.UpdateAsync(Guid.Parse("d6723d82-c265-4f07-9328-80c8cea6eddc"), input);

            // Assert
            var result = await _cSAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttributeID.ShouldBe("da0d99412f");
            result.Description.ShouldBe("7446a43f9621454aaed614c035a15cac8627196a7d234f528a901158db88");
            result.ControlType.ShouldBe(default);
            result.EntryMask.ShouldBe("d35ee54fb4bf4406b975043bdade812752ebea1986474404a75bd13ae2a5");
            result.RegExp.ShouldBe("87f8b81b95f74a87a59bbeb8c1d45d16d1af37bc11034735a74fd7626021a1f310344abf526941b183cd807b1e25ae7a1b779cdd222240bf92fb2eb4db80071fe6a3be3e4d4748adba91dd90425226fda8d684d32714404cbe0868b52d4af2e419cb09e9164d46678a8a6da44771ebff9597322ca658441c9334cb956669b38");
            result.List.ShouldBe("9d0b5168f2");
            result.IsInternal.ShouldBe(true);
            result.ContainsPersonalData.ShouldBe(true);
            result.ObjectName.ShouldBe("8af320309c814ca182dd92545ac5087e7b5d8dc137e4435090cc562bbb42afd180d07ff4bf5a4a6094f76c2ce776c7e32dcd50373d5641a086437383bd70eec3fc00311b861941f0bdec7b0dde148c3a269a54fd95a24e7c93e46a4f1236f5b25f1b18681f4b4ada9b0d8b82d3f925d30bfbe54debec481ca90293d647354caf2fdab20479824a9f9517a699406324b089d42919e2d2425c8b205f9cb867ab7af01617eb3c374bf98a4fed6a58c236347b0b0820dea04ecda41d806dc71a5e39fae7c1a5138d40c48c520b78630ce7bd10105f646df8417a9fb01342304ea33cf2c333ac3dfe4c74b8528604487193f61567bd4ac78445d99a244b2fbf07e554");
            result.FieldName.ShouldBe("4e7ea090fc3f4e86b9099f5f172c86cf006c1be40e92402196f803bd4f717831c4cc63cb328046b79e6589695b840f166c89d220e11b43bba340baaba9446f58f35ba137cd02498cb2e8706d86207cd4432d685dcdb9402abcbefa43e580e03901436b11412648749abdf638b1c7b8e8979b8841beef4dc997f229169e828dde655c4bddffa04e02ad612a1d001fa40c8308eb748c244ca5a2a5bdf6ee04099dd07c5426a3224237ae4177d0f6a15d6745d79c7f72de45af8c436f6a68b654f1eb8f7d7ea5bd4fd5adcdcf96b94c5f8d49d8bb8d520e49eb8ea9a8f92f76ac10eb8bb00cbf834b0a8fd684b7057bc598775c423be24e46cdbd716265a0534492");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cSAttributesAppService.DeleteAsync(Guid.Parse("d6723d82-c265-4f07-9328-80c8cea6eddc"));

            // Assert
            var result = await _cSAttributeRepository.FindAsync(c => c.Id == Guid.Parse("d6723d82-c265-4f07-9328-80c8cea6eddc"));

            result.ShouldBeNull();
        }
    }
}