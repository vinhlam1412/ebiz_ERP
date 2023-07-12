$(function () {
    var l = abp.localization.getResource("Configuration");
	
	var cSAttributeService = window.hQSOFT.configuration.cSAttributes.cSAttribute;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Configuration/CSAttributes/CreateModal",
        scriptUrl: "/Pages/Configuration/CSAttributes/createModal.js",
        modalClass: "cSAttributeCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Configuration/CSAttributes/EditModal",
        scriptUrl: "/Pages/Configuration/CSAttributes/editModal.js",
        modalClass: "cSAttributeEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            attributeID: $("#AttributeIDFilter").val(),
			description: $("#DescriptionFilter").val(),
			controlType: $("#ControlTypeFilter").val(),
			entryMask: $("#EntryMaskFilter").val(),
			regExp: $("#RegExpFilter").val(),
			list: $("#ListFilter").val(),
            isInternal: (function () {
                var value = $("#IsInternalFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            containsPersonalData: (function () {
                var value = $("#ContainsPersonalDataFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			objectName: $("#ObjectNameFilter").val(),
			fieldName: $("#FieldNameFilter").val()
        };
    };

    var dataTable = $("#CSAttributesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(cSAttributeService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Configuration.CSAttributes.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Configuration.CSAttributes.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    cSAttributeService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "attributeID" },
			{ data: "description" },
            {
                data: "controlType",
                render: function (controlType) {
                    if (controlType === undefined ||
                        controlType === null) {
                        return "";
                    }

                    var localizationKey = "Enum:ControlType." + controlType;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
			{ data: "entryMask" },
			{ data: "regExp" },
			{ data: "list" },
            {
                data: "isInternal",
                render: function (isInternal) {
                    return isInternal ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "containsPersonalData",
                render: function (containsPersonalData) {
                    return containsPersonalData ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
			{ data: "objectName" },
			{ data: "fieldName" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewCSAttributeButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        cSAttributeService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/configuration/c-sAttributes/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'attributeID', value: input.attributeID }, 
                            { name: 'description', value: input.description }, 
                            { name: 'controlType', value: input.controlType }, 
                            { name: 'entryMask', value: input.entryMask }, 
                            { name: 'regExp', value: input.regExp }, 
                            { name: 'list', value: input.list }, 
                            { name: 'isInternal', value: input.isInternal }, 
                            { name: 'containsPersonalData', value: input.containsPersonalData }, 
                            { name: 'objectName', value: input.objectName }, 
                            { name: 'fieldName', value: input.fieldName }
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
