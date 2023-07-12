using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailsAppServiceTests : ConfigurationApplicationTestBase
    {
        private readonly ICSAttributeDetailsAppService _cSAttributeDetailsAppService;
        private readonly IRepository<CSAttributeDetail, Guid> _cSAttributeDetailRepository;

        public CSAttributeDetailsAppServiceTests()
        {
            _cSAttributeDetailsAppService = GetRequiredService<ICSAttributeDetailsAppService>();
            _cSAttributeDetailRepository = GetRequiredService<IRepository<CSAttributeDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _cSAttributeDetailsAppService.GetListAsync(new GetCSAttributeDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CSAttributeDetail.Id == Guid.Parse("c1387657-c4f0-4b8b-a95c-bca06d36bd56")).ShouldBe(true);
            result.Items.Any(x => x.CSAttributeDetail.Id == Guid.Parse("a1e0da9c-16cb-40e0-94d4-783028f36da6")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cSAttributeDetailsAppService.GetAsync(Guid.Parse("c1387657-c4f0-4b8b-a95c-bca06d36bd56"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c1387657-c4f0-4b8b-a95c-bca06d36bd56"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CSAttributeDetailCreateDto
            {
                ValueID = "61b84117d9",
                Description = "193d6f3add724baaa658da4785b2e8e63c4ffe135a5d417896c2b686a6c3",
                SortOrder = 691459538,
                Disabled = true
            };

            // Act
            var serviceResult = await _cSAttributeDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _cSAttributeDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ValueID.ShouldBe("61b84117d9");
            result.Description.ShouldBe("193d6f3add724baaa658da4785b2e8e63c4ffe135a5d417896c2b686a6c3");
            result.SortOrder.ToString().ShouldBe("691459538");
            result.Disabled.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CSAttributeDetailUpdateDto()
            {
                ValueID = "d31674803d",
                Description = "65c58494b2ff419590eb5b43cefc32f39ba82a15264c4d279bdf227d6b58",
                SortOrder = 2104615012,
                Disabled = true
            };

            // Act
            var serviceResult = await _cSAttributeDetailsAppService.UpdateAsync(Guid.Parse("c1387657-c4f0-4b8b-a95c-bca06d36bd56"), input);

            // Assert
            var result = await _cSAttributeDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ValueID.ShouldBe("d31674803d");
            result.Description.ShouldBe("65c58494b2ff419590eb5b43cefc32f39ba82a15264c4d279bdf227d6b58");
            result.SortOrder.ToString().ShouldBe("2104615012");
            result.Disabled.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cSAttributeDetailsAppService.DeleteAsync(Guid.Parse("c1387657-c4f0-4b8b-a95c-bca06d36bd56"));

            // Assert
            var result = await _cSAttributeDetailRepository.FindAsync(c => c.Id == Guid.Parse("c1387657-c4f0-4b8b-a95c-bca06d36bd56"));

            result.ShouldBeNull();
        }
    }
}