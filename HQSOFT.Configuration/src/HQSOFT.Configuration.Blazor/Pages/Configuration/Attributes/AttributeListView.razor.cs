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
using HQSOFT.Configuration.CSAttributes;
using HQSOFT.Configuration.Permissions;
using HQSOFT.Configuration.Shared;
using Volo.Abp.AspNetCore.Components.Messages;

namespace HQSOFT.Configuration.Blazor.Pages.Configuration.Attributes
{
    public partial class AttributeListView
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private IReadOnlyList<CSAttributeDto> CSAttributeList { get; set; }

        private List<CSAttributeDto> selectedAttributes { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateCSAttribute { get; set; }
        private bool CanEditCSAttribute { get; set; }
        private bool CanDeleteCSAttribute { get; set; }
        private CSAttributeCreateDto NewCSAttribute { get; set; }
        private Validations NewCSAttributeValidations { get; set; } = new();
        private CSAttributeUpdateDto EditingCSAttribute { get; set; }
        private Validations EditingCSAttributeValidations { get; set; } = new();
        private Guid EditingCSAttributeId { get; set; }
        private Modal CreateCSAttributeModal { get; set; } = new();
        private Modal EditCSAttributeModal { get; set; } = new();
        private GetCSAttributesInput Filter { get; set; }

        private readonly IUiMessageService _uiMessageService;
        private DataGridEntityActionsColumn<CSAttributeDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "cSAttribute-create-tab";
        protected string SelectedEditTab = "cSAttribute-edit-tab";

        public AttributeListView(IUiMessageService uiMessageService)
        {
            NewCSAttribute = new CSAttributeCreateDto();
            EditingCSAttribute = new CSAttributeUpdateDto();
            Filter = new GetCSAttributesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            CSAttributeList = new List<CSAttributeDto>();
            selectedAttributes = new List<CSAttributeDto>();
            _uiMessageService = uiMessageService;
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:CSAttributes"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () => { await DownloadAsExcelAsync(); }, IconName.Download);

            Toolbar.AddButton(L["New"], () =>
            {
                NavigationManager.NavigateTo($"/Configuration/CSAttributes/edit/{Guid.Empty}");
                return Task.CompletedTask;
            },
            IconName.Add, requiredPolicyName: ConfigurationPermissions.CSAttributes.Create);
            Toolbar.AddButton(L["Delete"], async () =>
            {
                if (selectedAttributes.Count > 0)
                {
                    var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
                    if (confirmed)
                    {
                        foreach (var attr in selectedAttributes)
                        {
                            await CSAttributesAppService.DeleteAsync(attr.Id);
                        }
                        await GetCSAttributesAsync();
                    }
                }
            }, IconName.Delete,
            Color.Danger,
            requiredPolicyName: ConfigurationPermissions.CSAttributes.Delete);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateCSAttribute = await AuthorizationService
                .IsGrantedAsync(ConfigurationPermissions.CSAttributes.Create);
            CanEditCSAttribute = await AuthorizationService
                            .IsGrantedAsync(ConfigurationPermissions.CSAttributes.Edit);
            CanDeleteCSAttribute = await AuthorizationService
                            .IsGrantedAsync(ConfigurationPermissions.CSAttributes.Delete);
        }

        private async Task GetCSAttributesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await CSAttributesAppService.GetListAsync(Filter);
            CSAttributeList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetCSAttributesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task DownloadAsExcelAsync()
        {
            var token = (await CSAttributesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Configuration") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/configuration/c-sAttributes/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CSAttributeDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetCSAttributesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateCSAttributeModalAsync()
        {
            NewCSAttribute = new CSAttributeCreateDto
            {


            };
            await NewCSAttributeValidations.ClearAll();
            await CreateCSAttributeModal.Show();
        }

        private async Task CloseCreateCSAttributeModalAsync()
        {
            NewCSAttribute = new CSAttributeCreateDto
            {


            };
            await CreateCSAttributeModal.Hide();
        }

        private async Task OpenEditCSAttributeModalAsync(CSAttributeDto input)
        {
            var cSAttribute = await CSAttributesAppService.GetAsync(input.Id);

            EditingCSAttributeId = cSAttribute.Id;
            EditingCSAttribute = ObjectMapper.Map<CSAttributeDto, CSAttributeUpdateDto>(cSAttribute);
            await EditingCSAttributeValidations.ClearAll();
            await EditCSAttributeModal.Show();
        }

        private async Task DeleteCSAttributeAsync(CSAttributeDto input)
        {
            await CSAttributesAppService.DeleteAsync(input.Id);
            await GetCSAttributesAsync();
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }

        private void GoToEditPage(CSAttributeDto attribute)
        {
            NavigationManager.NavigateTo($"/Configuration/CSAttributes/edit/{attribute.Id}");
        }


    }
}
