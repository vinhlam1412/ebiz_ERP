using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailRepositoryTests : ConfigurationEntityFrameworkCoreTestBase
    {
        private readonly ICSAttributeDetailRepository _cSAttributeDetailRepository;

        public CSAttributeDetailRepositoryTests()
        {
            _cSAttributeDetailRepository = GetRequiredService<ICSAttributeDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cSAttributeDetailRepository.GetListAsync(
                    valueID: "5ae069b05e",
                    description: "35e344cdbd4044ef9128f0e1d339cac9fa6108be62b44100bff1361ea318",
                    disabled: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c1387657-c4f0-4b8b-a95c-bca06d36bd56"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cSAttributeDetailRepository.GetCountAsync(
                    valueID: "4ff78ec0b8",
                    description: "d1fd265525d5436c989320534481207caee88a2172b9470aa2dc3413382b",
                    disabled: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}