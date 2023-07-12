using Blazorise;
using DevExpress.Blazor;
using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.CSAttributes;
using HQSOFT.Configuration.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.ObjectMapping;

namespace HQSOFT.Configuration.Blazor.Pages.Configuration.Attributes
{
    public partial class NewAttribute
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private readonly IUiMessageService _messageService;
        private Validations EditingAttributeValidations { get; set; }
        private CSAttributeUpdateDto EditingAttribute { get; set; }

        private Guid EditingAttributeId { get; set; }

        private IReadOnlyList<CSAttributeDetailDto> _attributeDetails { get; set; }
        Blazorise.Visibility isVisible;

        bool isDisabled;

        bool isVisibleEditMode;

        ControlType selectedValue;

        IEnumerable<CSAttributeDetailDto> DataSource { get; set; }

        [Parameter]
        public string Id { get; set; }

        IGrid Grid { get; set; }

        bool IsEditingInfo { get; set; }
        public NewAttribute(IUiMessageService messageService)
        {
            EditingAttribute = new CSAttributeUpdateDto();
            EditingAttribute.ConcurrencyStamp = string.Empty;
            _messageService = messageService;
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Attributes"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["Back"], () =>
            {
                GoToListPage();
                return Task.CompletedTask;
            },
            IconName.Undo,
            Color.Secondary);

            Toolbar.AddButton(L["Save"], async () =>
            {
                await SaveCsAttributeAsync(false);
            }, IconName.Save, requiredPolicyName: ConfigurationPermissions.CSAttributes.Create);
            Toolbar.AddButton(L["Delete"], async () =>
            {
                var confirmed = await _messageService.Confirm(L["DeleteConfirmationMessage"]);
                if (confirmed)
                {
                    await DeleteCSAttributeAsync(EditingAttributeId);
                }
            }, IconName.Delete,
            Color.Danger,
            requiredPolicyName: ConfigurationPermissions.CSAttributes.Delete);

            return ValueTask.CompletedTask;
        }
        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            if (EditingAttribute.ControlType == ControlType.MultiSelectCombo || EditingAttribute.ControlType == ControlType.Combo)
            {
                isVisibleEditMode = true;
            }
            else
            {
                isVisibleEditMode = false;
            }
            if (EditingAttribute.ControlType == ControlType.Selector)
            {
                isVisible = Visibility.Visible;
            }
            else
            {
                isVisible = Visibility.Invisible;
            }
            if (EditingAttribute.ControlType == ControlType.Text)
            {
                isDisabled = false;

            }
            else
            {
                isDisabled = true;

            }
            //await SetPermissionsAsync();

            isVisible = Visibility.Invisible;

            EditingAttributeId = Guid.Parse(Id);
            if (EditingAttributeId != Guid.Empty)
            {
                var attributeID = await CSAttributesAppService.GetAsync(EditingAttributeId);
                EditingAttribute = ObjectMapper.Map<CSAttributeDto, CSAttributeUpdateDto>(attributeID);
            }

            _attributeDetails = new List<CSAttributeDetailDto>();
            DataSource = await AttributeDetailsAppService.GetListAllAttriDetail(EditingAttributeId);

        }
        private async Task CreateCSAttributeAsync()
        {
            try
            {
                if (await EditingAttributeValidations.ValidateAll() == false)
                {
                    return;
                }

                //update and map in mapperprofile
                CSAttributeCreateDto csAttributeCreateDto = ObjectMapper.Map<CSAttributeUpdateDto, CSAttributeCreateDto>(EditingAttribute);
                var csAttribute = await CSAttributesAppService.CreateAsync(csAttributeCreateDto);


                EditingAttributeId = csAttribute.Id;
                EditingAttribute = ObjectMapper.Map<CSAttributeDto, CSAttributeUpdateDto>(csAttribute);
                NavigationManager.NavigateTo($"/Configuration/CSAttributes/edit/{EditingAttributeId}");

            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        private async Task UpdateCSAttributeAsync()
        {
            try
            {
                if (await EditingAttributeValidations.ValidateAll() == false)
                {
                    return;
                }

                await CSAttributesAppService.UpdateAsync(EditingAttributeId, EditingAttribute);
                var csAttribute = await CSAttributesAppService.GetAsync(EditingAttributeId);
                EditingAttribute = ObjectMapper.Map<CSAttributeDto, CSAttributeUpdateDto>(csAttribute);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        public async Task SaveCsAttributeAsync(bool isNewNext)
        {
            try
            {
                if (EditingAttributeId == Guid.Empty)
                {
                    await CreateCSAttributeAsync();
                }
                else
                {
                    await UpdateCSAttributeAsync();
                }
                if (isNewNext)
                {
                    NavigationManager.NavigateTo($"/Configuration/CSAttributes/edit/{Guid.Empty}", true);
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        private async Task DeleteCSAttributeAsync(Guid id)
        {
            await CSAttributesAppService.DeleteAsync(id);
            GoToListPage();
        }
        Task OnSelectedValueChanged(ControlType value)
        {
            selectedValue = value;
            EditingAttribute.ControlType = value;
            if (selectedValue == ControlType.MultiSelectCombo || selectedValue == ControlType.Combo)
            {
                isVisibleEditMode = true;
            }
            else
            {
                isVisibleEditMode = false;
            }
            if (selectedValue == ControlType.Selector)
            {
                isVisible = Visibility.Visible;
            }
            else
            {
                isVisible = Visibility.Invisible;
            }
            if (selectedValue == ControlType.Text)
            {
                isDisabled = false;
                
            }
            else
            {
                isDisabled = true;
                
            }
           
            return Task.CompletedTask;
        }
        private void GoToListPage()
        {
            NavigationManager.NavigateTo("/Configuration/CSAttributes");
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //        await Grid.StartEditNewRowAsync();
        //}
        async Task Grid_EditModelSaving(GridEditModelSavingEventArgs e)
        {
            var edittableDetail = (CSAttributeDetailDto)e.EditModel;
           
            if (e.IsNew)
            {
                var createtableDetail = ObjectMapper.Map<CSAttributeDetailDto, CSAttributeDetailCreateDto>(edittableDetail);
                createtableDetail.CSAttributeId = EditingAttributeId;
                await SaveCsAttributeAsync(false);
                await AttributeDetailsAppService.CreateAsync(createtableDetail);

            }
            else
            {
                var updatetableDetail = ObjectMapper.Map<CSAttributeDetailDto, CSAttributeDetailUpdateDto>(edittableDetail);
                await AttributeDetailsAppService.UpdateAsync(edittableDetail.Id, updatetableDetail);
            }
            await UpdateDataAsync();

        }
        async Task Grid_DataItemDeleting(GridDataItemDeletingEventArgs e)
        {
            var removetableDetail = (CSAttributeDetailDto)e.DataItem;
            await AttributeDetailsAppService.DeleteAsync(removetableDetail.Id);
            await UpdateDataAsync();
        }
        void Grid_CustomizeEditModel(GridCustomizeEditModelEventArgs e)
        {
            if (e.IsNew)
            {
                var attributeDetail = (CSAttributeDetailDto)e.EditModel;
                attributeDetail = new CSAttributeDetailDto();
            }
        }
        async Task UpdateDataAsync()
        {
            DataSource = await AttributeDetailsAppService.GetListAllAttriDetail(EditingAttributeId);
        }

    }
}
