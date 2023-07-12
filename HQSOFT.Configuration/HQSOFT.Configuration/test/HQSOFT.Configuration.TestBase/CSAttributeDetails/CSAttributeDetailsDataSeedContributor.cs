using HQSOFT.Configuration.CSAttributes;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.Configuration.CSAttributeDetails;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICSAttributeDetailRepository _cSAttributeDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CSAttributesDataSeedContributor _cSAttributesDataSeedContributor;

        public CSAttributeDetailsDataSeedContributor(ICSAttributeDetailRepository cSAttributeDetailRepository, IUnitOfWorkManager unitOfWorkManager, CSAttributesDataSeedContributor cSAttributesDataSeedContributor)
        {
            _cSAttributeDetailRepository = cSAttributeDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _cSAttributesDataSeedContributor = cSAttributesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _cSAttributesDataSeedContributor.SeedAsync(context);

            await _cSAttributeDetailRepository.InsertAsync(new CSAttributeDetail
            (
                id: Guid.Parse("c1387657-c4f0-4b8b-a95c-bca06d36bd56"),
                valueID: "5ae069b05e",
                description: "35e344cdbd4044ef9128f0e1d339cac9fa6108be62b44100bff1361ea318",
                sortOrder: 341034228,
                disabled: true,
                cSAttributeId: null
            ));

            await _cSAttributeDetailRepository.InsertAsync(new CSAttributeDetail
            (
                id: Guid.Parse("a1e0da9c-16cb-40e0-94d4-783028f36da6"),
                valueID: "4ff78ec0b8",
                description: "d1fd265525d5436c989320534481207caee88a2172b9470aa2dc3413382b",
                sortOrder: 1073529109,
                disabled: true,
                cSAttributeId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}