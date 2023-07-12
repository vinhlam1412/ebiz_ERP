import { mapEnumToOptions } from '@abp/ng.core';

export enum ControlType {
  Text = 1,
  Combo = 2,
  MultiSelectCombo = 3,
  Checkbox = 4,
  Datetime = 5,
  Selector = 6,
}

export const controlTypeOptions = mapEnumToOptions(ControlType);
