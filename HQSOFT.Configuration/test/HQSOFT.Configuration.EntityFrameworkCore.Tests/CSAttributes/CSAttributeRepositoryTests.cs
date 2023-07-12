using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.Configuration.CSAttributes;
using HQSOFT.Configuration.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.Configuration.CSAttributes
{
    public class CSAttributeRepositoryTests : ConfigurationEntityFrameworkCoreTestBase
    {
        private readonly ICSAttributeRepository _cSAttributeRepository;

        public CSAttributeRepositoryTests()
        {
            _cSAttributeRepository = GetRequiredService<ICSAttributeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cSAttributeRepository.GetListAsync(
                    attributeID: "ac6c5f1242",
                    description: "5bd96a38fd9d47adae5a3103e68726265e65846f952c44439dda45c214fe",
                    controlType: default,
                    entryMask: "6460ac0d172d4e0bb3fcb5a567bca3adcd0d26b3f07e4f67af7ce56065d9",
                    regExp: "0d7d33437b6e417da9365f0bf7e89488077cddcfe8f7455f9011f2e5a0729b3d6e1ae96adb4b4f608e92796bb450e584f9a0b2f47f4e4dbc97ee5fa7b08083d196512a983f0e44ef850f6aa20d875aef54f263f7815c42eb93c5a9169ba8bf00d2b3b27c62ad4454b05ef92d6388dc2738cbbaa0603145fdb266cd90443d2dc",
                    list: "07fdb759db0e4de3972036d6f40e38c853",
                    isInternal: true,
                    containsPersonalData: true,
                    objectName: "b00fbff520b34825b68f8a7cc6faeb709b463b40db4d4547b44aa1a88afa5cf6f5cc42465ef04566b33b165cbef5a6ccb55e8bdbddf44e04a197bb4481012a904c2d84179c9a4fe6b734f93064fcc407b7cd957319714c3da578db475a323f325f544cdb6a904157ada61eb62327bcc959647a62d7e34c149b9c22374ae4992db9b1c981b06141e297670abfd1ef76fe783d303e14f64906b30a51aa9ffbd87cf6a48c263ac944a1b92ef7d2fb32e86c9082d541bf67470cb22d0d5ff9ffcedf4268f82a9c74425b9630d10a1f0895574d405217329e468fb62f80dfa4cb7fc4c01b203c9dc2405c9a2f76f466ab5c86d1f662e506fb467aa57fac1b231cc053",
                    fieldName: "2bb711c6828e488c99cbac833d32d36ebefd361719664d4eb729ffbef3e6dcc32c48b36f8bb24b48aa2f83e49fc2e118234e9420a68547438d62770bbb456e118b8b3d2ee7304cd4a8bd3e2ebd070f62e1a84edd699d452499fe92f61e30a93f8c3ef7df09a24b9687328eec734f7b1d079d171fc7134387b913ec15f989874d2fdb488d15f24616a2101b27dc75fcb4fba395a324a7410391daaaf06f9f2f87dccb0873940f4f2fbb08270aa78653429c957af6359945d6b46d5f7b9bf0da7babf3b3d6e7f540b3818d385cfa32de06ee495e0625af43f893e5213ee99f726e49b4cbd16c604c59bae5eb68a4d9e703daf939bca0e34de39da65694b4522f22"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d6723d82-c265-4f07-9328-80c8cea6eddc"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cSAttributeRepository.GetCountAsync(
                    attributeID: "f8c8fb4ff1",
                    description: "0c73e401b9554a90b2f1005f15cb86d041ef7f0b62e14a18a56f3794b0d5",
                    controlType: default,
                    entryMask: "6775f09d856b46958df2fc28dab09ca13d090c03abf34eecb6ca4a5e116e",
                    regExp: "c17e8a3585dc47d1b875cce5a2c94498b02cff44a1af4d189d35e500088769500f5b2d579de5439cacedea4819c29e37f14d19f9177f492ab98e50f64a58bb90b6b488b246a04a30a03d4ea01da008d527dcbcbb3b6a4e8d80bceb77c104d2e2cd43fe008fba4bfeb571c480c2dd11acf44f92909114499b852732308d11e63",
                    list: "f348e8800b5b4cd29e3ce8b057b816581f404f5c468e49359de2066cfcccf5fbe531b",
                    isInternal: true,
                    containsPersonalData: true,
                    objectName: "8f635acb9deb41e4bdbcbdf053f247fd5c63fcc22b294d6bac9db5eedf3c4523058141a512714c88b9d500f02121ac9cc8abc7f829994533b7b31528b54124b0c88dcae7c77544ce9235c24db3fac6f9f5be0c8163d845bcbce78950640b0c3e9e0fa5f1236b426789dfa3a836506443869d68aa52394a9aa2d793ba2da94c2a9e136cfe64a948d98e7301c1fb3f9ebd1764f54583cd4072b51c87bfab9f62c77b3846c38faf4b5dbec72b95c3089026279e627ec8284e83adbb299e75953cbcf7e8d1899153482696222cdc24fa82280c6d45314a9f4f6baefdec86d77eb54d17d4a671265c4ac489471b4e296e9057c2198139289f4a4984e5c3db7f1dd206",
                    fieldName: "45c683260c6b493780d5a3d9794239c00c47b4d26cd1438d95cdd4608c321d1ea1635b0898634a81ac991d925e480922c3ad0e04d63846a7b63e7763154fede1ab84b0cea1ab455b985bc252f50b409ebc9b0aef9b644a2786fe259a98dd1f715ba1f4ce52774edcb0c081241739fc75e2e37a7c87654e838b3c6869a7cacbf6ef7546eed52848309ec16f8d785a4e57f3b1b2a2e9a247f991c2150578d2c407b48b9620f34c45d59e9549a692f42ca655d32dac4957486d9b5e39df0763bf6c9ee56396758b445cb1fd2afca8f294ebb815ae4d9d614b9595b4f7f4c3e62ecf75027e513324449e8dadfaa8b9b468b3805c9bcf289a48c98d17a6cce321b25b"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}