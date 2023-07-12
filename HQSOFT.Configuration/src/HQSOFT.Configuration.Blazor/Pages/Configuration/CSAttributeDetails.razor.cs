using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.Permissions;
using HQSOFT.Configuration.Shared;

namespace HQSOFT.Configuration.Blazor.Pages.Configuration
{
    public partial class CSAttributeDetails
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<CSAttributeDetailWithNavigationPropertiesDto> CSAttributeDetailList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateCSAttributeDetail { get; set; }
        private bool CanEditCSAttributeDetail { get; set; }
        private bool CanDeleteCSAttributeDetail { get; set; }
        private CSAttributeDetailCreateDto NewCSAttributeDetail { get; set; }
        private Validations NewCSAttributeDetailValidations { get; set; } = new();
        private CSAttributeDetailUpdateDto EditingCSAttributeDetail { get; set; }
        private Validations EditingCSAttributeDetailValidations { get; set; } = new();
        private Guid EditingCSAttributeDetailId { get; set; }
        private Modal CreateCSAttributeDetailModal { get; set; } = new();
        private Modal EditCSAttributeDetailModal { get; set; } = new();
        private GetCSAttributeDetailsInput Filter { get; set; }
        private DataGridEntityActionsColumn<CSAttributeDetailWithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "cSAttributeDetail-create-tab";
        protected string SelectedEditTab = "cSAttributeDetail-edit-tab";
        private IReadOnlyList<LookupDto<Guid>> CSAttributesCollection { get; set; } = new List<LookupDto<Guid>>();

        public CSAttributeDetails()
        {
            NewCSAttributeDetail = new CSAttributeDetailCreateDto();
            EditingCSAttributeDetail = new CSAttributeDetailUpdateDto();
            Filter = new GetCSAttributeDetailsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            CSAttributeDetailList = new List<CSAttributeDetailWithNavigationPropertiesDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetCSAttributeCollectionLookupAsync();


        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:CSAttributeDetails"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewCSAttributeDetail"], async () =>
            {
                await OpenCreateCSAttributeDetailModalAsync();
            }, IconName.Add, requiredPolicyName: ConfigurationPermissions.CSAttributeDetails.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateCSAttributeDetail = await AuthorizationService
                .IsGrantedAsync(ConfigurationPermissions.CSAttributeDetails.Create);
            CanEditCSAttributeDetail = await AuthorizationService
                            .IsGrantedAsync(ConfigurationPermissions.CSAttributeDetails.Edit);
            CanDeleteCSAttributeDetail = await AuthorizationService
                            .IsGrantedAsync(ConfigurationPermissions.CSAttributeDetails.Delete);
        }

        private async Task GetCSAttributeDetailsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await CSAttributeDetailsAppService.GetListAsync(Filter);
            CSAttributeDetailList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetCSAttributeDetailsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await CSAttributeDetailsAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Configuration") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/configuration/c-sAttribute-details/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CSAttributeDetailWithNavigationPropertiesDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetCSAttributeDetailsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateCSAttributeDetailModalAsync()
        {
            NewCSAttributeDetail = new CSAttributeDetailCreateDto{
                
                
            };
            await NewCSAttributeDetailValidations.ClearAll();
            await CreateCSAttributeDetailModal.Show();
        }

        private async Task CloseCreateCSAttributeDetailModalAsync()
        {
            NewCSAttributeDetail = new CSAttributeDetailCreateDto{
                
                
            };
            await CreateCSAttributeDetailModal.Hide();
        }

        private async Task OpenEditCSAttributeDetailModalAsync(CSAttributeDetailWithNavigationPropertiesDto input)
        {
            var cSAttributeDetail = await CSAttributeDetailsAppService.GetWithNavigationPropertiesAsync(input.CSAttributeDetail.Id);
            
            EditingCSAttributeDetailId = cSAttributeDetail.CSAttributeDetail.Id;
            EditingCSAttributeDetail = ObjectMapper.Map<CSAttributeDetailDto, CSAttributeDetailUpdateDto>(cSAttributeDetail.CSAttributeDetail);
            await EditingCSAttributeDetailValidations.ClearAll();
            await EditCSAttributeDetailModal.Show();
        }

        private async Task DeleteCSAttributeDetailAsync(CSAttributeDetailWithNavigationPropertiesDto input)
        {
            await CSAttributeDetailsAppService.DeleteAsync(input.CSAttributeDetail.Id);
            await GetCSAttributeDetailsAsync();
        }

        private async Task CreateCSAttributeDetailAsync()
        {
            try
            {
                if (await NewCSAttributeDetailValidations.ValidateAll() == false)
                {
                    return;
                }

                await CSAttributeDetailsAppService.CreateAsync(NewCSAttributeDetail);
                await GetCSAttributeDetailsAsync();
                await CloseCreateCSAttributeDetailModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditCSAttributeDetailModalAsync()
        {
            await EditCSAttributeDetailModal.Hide();
        }

        private async Task UpdateCSAttributeDetailAsync()
        {
            try
            {
                if (await EditingCSAttributeDetailValidations.ValidateAll() == false)
                {
                    return;
                }

                await CSAttributeDetailsAppService.UpdateAsync(EditingCSAttributeDetailId, EditingCSAttributeDetail);
                await GetCSAttributeDetailsAsync();
                await EditCSAttributeDetailModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }
        

        private async Task GetCSAttributeCollectionLookupAsync(string? newValue = null)
        {
            CSAttributesCollection = (await CSAttributeDetailsAppService.GetCSAttributeLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

    }
}
