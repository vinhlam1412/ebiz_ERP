import type { ControlType } from '../control-type/control-type.enum';
import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CSAttributeCreateDto {
  attributeID: string;
  description: string;
  controlType?: ControlType;
  entryMask?: string;
  regExp?: string;
  list?: string;
  isInternal?: boolean;
  containsPersonalData?: boolean;
  objectName?: string;
  fieldName?: string;
}

export interface CSAttributeDto extends FullAuditedEntityDto<string> {
  attributeID: string;
  description: string;
  controlType?: ControlType;
  entryMask?: string;
  regExp?: string;
  list?: string;
  isInternal?: boolean;
  containsPersonalData?: boolean;
  objectName?: string;
  fieldName?: string;
  concurrencyStamp?: string;
}

export interface CSAttributeExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface CSAttributeUpdateDto {
  attributeID: string;
  description: string;
  controlType?: ControlType;
  entryMask?: string;
  regExp?: string;
  list?: string;
  isInternal?: boolean;
  containsPersonalData?: boolean;
  objectName?: string;
  fieldName?: string;
  concurrencyStamp?: string;
}

export interface GetCSAttributesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  attributeID?: string;
  description?: string;
  controlType?: ControlType;
  entryMask?: string;
  regExp?: string;
  list?: string;
  isInternal?: boolean;
  containsPersonalData?: boolean;
  objectName?: string;
  fieldName?: string;
}
