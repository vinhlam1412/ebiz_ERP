import { ABP, downloadBlob, ListService, PagedResultDto, TrackByService } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { DateAdapter } from '@abp/ng.theme.shared/extensions';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { filter, finalize, switchMap, tap } from 'rxjs/operators';
import { controlTypeOptions } from '../../../proxy/control-type/control-type.enum';
import type { GetCSAttributesInput, CSAttributeDto } from '../../../proxy/csattributes/models';
import { CSAttributeService } from '../../../proxy/csattributes/csattribute.service';
@Component({
  selector: 'lib-csattribute',
  changeDetection: ChangeDetectionStrategy.Default,
  providers: [ListService, { provide: NgbDateAdapter, useClass: DateAdapter }],
  templateUrl: './csattribute.component.html',
  styles: [],
})
export class CSAttributeComponent implements OnInit {
  data: PagedResultDto<CSAttributeDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetCSAttributesInput;

  form: FormGroup;

  isFiltersHidden = true;

  isModalBusy = false;

  isModalOpen = false;

  isExportToExcelBusy = false;

  selected?: CSAttributeDto;

  controlTypeOptions = controlTypeOptions;

  constructor(
    public readonly list: ListService,
    public readonly track: TrackByService,
    public readonly service: CSAttributeService,
    private confirmation: ConfirmationService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    const getData = (query: ABP.PageQueryParams) =>
      this.service.getList({
        ...query,
        ...this.filters,
        filterText: query.filter,
      });

    const setData = (list: PagedResultDto<CSAttributeDto>) => (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetCSAttributesInput;
  }

  buildForm() {
    const {
      attributeID,
      description,
      controlType,
      entryMask,
      regExp,
      list,
      isInternal,
      containsPersonalData,
      objectName,
      fieldName,
    } = this.selected || {};

    this.form = this.fb.group({
      attributeID: [attributeID ?? null, [Validators.required, Validators.maxLength(10)]],
      description: [description ?? null, [Validators.required, Validators.maxLength(60)]],
      controlType: [controlType ?? null, []],
      entryMask: [entryMask ?? null, [Validators.maxLength(60)]],
      regExp: [regExp ?? null, [Validators.maxLength(255)]],
      list: [list ?? null, []],
      isInternal: [isInternal ?? false, []],
      containsPersonalData: [containsPersonalData ?? false, []],
      objectName: [objectName ?? null, [Validators.maxLength(512)]],
      fieldName: [fieldName ?? null, [Validators.maxLength(512)]],
    });
  }

  hideForm() {
    this.isModalOpen = false;
    this.form.reset();
  }

  showForm() {
    this.buildForm();
    this.isModalOpen = true;
  }

  submitForm() {
    if (this.form.invalid) return;

    const request = this.selected
      ? this.service.update(this.selected.id, {
          ...this.form.value,
          concurrencyStamp: this.selected.concurrencyStamp,
        })
      : this.service.create(this.form.value);

    this.isModalBusy = true;

    request
      .pipe(
        finalize(() => (this.isModalBusy = false)),
        tap(() => this.hideForm())
      )
      .subscribe(this.list.get);
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: CSAttributeDto) {
    this.selected = record;
    this.showForm();
  }

  delete(record: CSAttributeDto) {
    this.confirmation
      .warn('Configuration::DeleteConfirmationMessage', 'Configuration::AreYouSure', {
        messageLocalizationParams: [],
      })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.service.delete(record.id))
      )
      .subscribe(this.list.get);
  }

  exportToExcel() {
    this.isExportToExcelBusy = true;
    this.service
      .getDownloadToken()
      .pipe(
        switchMap(({ token }) =>
          this.service.getListAsExcelFile({ downloadToken: token, filterText: this.list.filter })
        ),
        finalize(() => (this.isExportToExcelBusy = false))
      )
      .subscribe(result => {
        downloadBlob(result, 'CSAttribute.xlsx');
      });
  }
}
