import type { CSAttributeDetailCreateDto, CSAttributeDetailDto, CSAttributeDetailExcelDownloadDto, CSAttributeDetailUpdateDto, CSAttributeDetailWithNavigationPropertiesDto, GetCSAttributeDetailsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class CSAttributeDetailService {
  apiName = 'Configuration';
  

  create = (input: CSAttributeDetailCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CSAttributeDetailDto>({
      method: 'POST',
      url: '/api/configuration/csattribute-details',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/configuration/csattribute-details/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CSAttributeDetailDto>({
      method: 'GET',
      url: `/api/configuration/csattribute-details/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getCSAttributeLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/configuration/csattribute-details/csattribute-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/configuration/csattribute-details/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCSAttributeDetailsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CSAttributeDetailWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/configuration/csattribute-details',
      params: { filterText: input.filterText, valueID: input.valueID, description: input.description, sortOrderMin: input.sortOrderMin, sortOrderMax: input.sortOrderMax, disabled: input.disabled, csAttributeId: input.csAttributeId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: CSAttributeDetailExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/configuration/csattribute-details/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CSAttributeDetailWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/configuration/csattribute-details/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CSAttributeDetailUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CSAttributeDetailDto>({
      method: 'PUT',
      url: `/api/configuration/csattribute-details/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
