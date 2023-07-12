import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { CSAttributeDto } from '../csattributes/models';

export interface CSAttributeDetailCreateDto {
  valueID: string;
  description: string;
  sortOrder?: number;
  disabled?: boolean;
  csAttributeId?: string;
}

export interface CSAttributeDetailDto extends FullAuditedEntityDto<string> {
  valueID: string;
  description: string;
  sortOrder?: number;
  disabled?: boolean;
  csAttributeId?: string;
  concurrencyStamp?: string;
}

export interface CSAttributeDetailExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface CSAttributeDetailUpdateDto {
  valueID: string;
  description: string;
  sortOrder?: number;
  disabled?: boolean;
  csAttributeId?: string;
  concurrencyStamp?: string;
}

export interface CSAttributeDetailWithNavigationPropertiesDto {
  csAttributeDetail: CSAttributeDetailDto;
  csAttribute: CSAttributeDto;
}

export interface GetCSAttributeDetailsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  valueID?: string;
  description?: string;
  sortOrderMin?: number;
  sortOrderMax?: number;
  disabled?: boolean;
  csAttributeId?: string;
}
