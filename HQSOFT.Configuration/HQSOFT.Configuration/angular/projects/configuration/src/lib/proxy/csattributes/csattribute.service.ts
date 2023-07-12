import type { CSAttributeCreateDto, CSAttributeDto, CSAttributeExcelDownloadDto, CSAttributeUpdateDto, GetCSAttributesInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class CSAttributeService {
  apiName = 'Configuration';
  

  create = (input: CSAttributeCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CSAttributeDto>({
      method: 'POST',
      url: '/api/configuration/csattributes',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/configuration/csattributes/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CSAttributeDto>({
      method: 'GET',
      url: `/api/configuration/csattributes/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/configuration/csattributes/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCSAttributesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CSAttributeDto>>({
      method: 'GET',
      url: '/api/configuration/csattributes',
      params: { filterText: input.filterText, attributeID: input.attributeID, description: input.description, controlType: input.controlType, entryMask: input.entryMask, regExp: input.regExp, list: input.list, isInternal: input.isInternal, containsPersonalData: input.containsPersonalData, objectName: input.objectName, fieldName: input.fieldName, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: CSAttributeExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/configuration/csattributes/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CSAttributeUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CSAttributeDto>({
      method: 'PUT',
      url: `/api/configuration/csattributes/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
