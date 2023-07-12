import { ABP, downloadBlob, ListService, PagedResultDto, TrackByService } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { DateAdapter } from '@abp/ng.theme.shared/extensions';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { filter, finalize, switchMap, tap } from 'rxjs/operators';
import type {
  GetCSAttributeDetailsInput,
  CSAttributeDetailWithNavigationPropertiesDto,
} from '../../../proxy/csattribute-details/models';
import { CSAttributeDetailService } from '../../../proxy/csattribute-details/csattribute-detail.service';
@Component({
  selector: 'lib-csattribute-detail',
  changeDetection: ChangeDetectionStrategy.Default,
  providers: [ListService, { provide: NgbDateAdapter, useClass: DateAdapter }],
  templateUrl: './csattribute-detail.component.html',
  styles: [],
})
export class CSAttributeDetailComponent implements OnInit {
  data: PagedResultDto<CSAttributeDetailWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetCSAttributeDetailsInput;

  form: FormGroup;

  isFiltersHidden = true;

  isModalBusy = false;

  isModalOpen = false;

  isExportToExcelBusy = false;

  selected?: CSAttributeDetailWithNavigationPropertiesDto;

  constructor(
    public readonly list: ListService,
    public readonly track: TrackByService,
    public readonly service: CSAttributeDetailService,
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

    const setData = (list: PagedResultDto<CSAttributeDetailWithNavigationPropertiesDto>) =>
      (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetCSAttributeDetailsInput;
  }

  buildForm() {
    const { valueID, description, sortOrder, disabled, csAttributeId } =
      this.selected?.csAttributeDetail || {};

    this.form = this.fb.group({
      valueID: [valueID ?? null, [Validators.required, Validators.maxLength(10)]],
      description: [description ?? null, [Validators.required, Validators.maxLength(60)]],
      sortOrder: [sortOrder ?? null, []],
      disabled: [disabled ?? false, []],
      csAttributeId: [csAttributeId ?? null, []],
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
      ? this.service.update(this.selected.csAttributeDetail.id, {
          ...this.form.value,
          concurrencyStamp: this.selected.csAttributeDetail.concurrencyStamp,
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

  update(record: CSAttributeDetailWithNavigationPropertiesDto) {
    this.selected = record;
    this.showForm();
  }

  delete(record: CSAttributeDetailWithNavigationPropertiesDto) {
    this.confirmation
      .warn('Configuration::DeleteConfirmationMessage', 'Configuration::AreYouSure', {
        messageLocalizationParams: [],
      })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.service.delete(record.csAttributeDetail.id))
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
        downloadBlob(result, 'CSAttributeDetail.xlsx');
      });
  }
}
