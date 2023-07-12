$(function () {
    var l = abp.localization.getResource("Configuration");
	
	var cSAttributeDetailService = window.hQSOFT.configuration.cSAttributeDetails.cSAttributeDetail;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: "/Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Configuration/CSAttributeDetails/CreateModal",
        scriptUrl: "/Pages/Configuration/CSAttributeDetails/createModal.js",
        modalClass: "cSAttributeDetailCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Configuration/CSAttributeDetails/EditModal",
        scriptUrl: "/Pages/Configuration/CSAttributeDetails/editModal.js",
        modalClass: "cSAttributeDetailEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            valueID: $("#ValueIDFilter").val(),
			description: $("#DescriptionFilter").val(),
			sortOrderMin: $("#SortOrderFilterMin").val(),
			sortOrderMax: $("#SortOrderFilterMax").val(),
            disabled: (function () {
                var value = $("#DisabledFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			cSAttributeId: $("#CSAttributeIdFilter").val()
        };
    };

    var dataTable = $("#CSAttributeDetailsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(cSAttributeDetailService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Configuration.CSAttributeDetails.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.cSAttributeDetail.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Configuration.CSAttributeDetails.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    cSAttributeDetailService.delete(data.record.cSAttributeDetail.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "csAttributeDetail.valueID" },
			{ data: "csAttributeDetail.description" },
			{ data: "csAttributeDetail.sortOrder" },
            {
                data: "csAttributeDetail.disabled",
                render: function (disabled) {
                    return disabled ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "csAttribute.attributeID",
                defaultContent : ""
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewCSAttributeDetailButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        cSAttributeDetailService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/configuration/c-sAttribute-details/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'valueID', value: input.valueID }, 
                            { name: 'description', value: input.description },
                            { name: 'sortOrderMin', value: input.sortOrderMin },
                            { name: 'sortOrderMax', value: input.sortOrderMax }, 
                            { name: 'disabled', value: input.disabled }, 
                            { name: 'cSAttributeId', value: input.cSAttributeId }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
