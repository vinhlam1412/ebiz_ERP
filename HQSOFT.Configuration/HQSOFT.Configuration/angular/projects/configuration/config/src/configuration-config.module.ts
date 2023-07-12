import { ModuleWithProviders, NgModule } from '@angular/core';
import { CONFIGURATION_ROUTE_PROVIDERS } from './providers/route.provider';
import { CSATTRIBUTES_CSATTRIBUTE_ROUTE_PROVIDER } from './providers/csattribute-route.provider';
import { CSATTRIBUTE_DETAILS_CSATTRIBUTE_DETAIL_ROUTE_PROVIDER } from './providers/csattribute-detail-route.provider';

@NgModule()
export class ConfigurationConfigModule {
  static forRoot(): ModuleWithProviders<ConfigurationConfigModule> {
    return {
      ngModule: ConfigurationConfigModule,
      providers: [
        CONFIGURATION_ROUTE_PROVIDERS,
        CSATTRIBUTES_CSATTRIBUTE_ROUTE_PROVIDER,
        CSATTRIBUTE_DETAILS_CSATTRIBUTE_DETAIL_ROUTE_PROVIDER,
      ],
    };
  }
}
