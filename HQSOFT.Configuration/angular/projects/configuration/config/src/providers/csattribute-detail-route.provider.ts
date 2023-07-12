import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eConfigurationRouteNames } from '../enums/route-names';

export const CSATTRIBUTE_DETAILS_CSATTRIBUTE_DETAIL_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/configuration/csattribute-details',
        parentName: eConfigurationRouteNames.Configuration,
        name: 'Configuration::Menu:CSAttributeDetails',
        layout: eLayoutType.application,
        requiredPolicy: 'Configuration.CSAttributeDetails',
      },
    ]);
  };
}
