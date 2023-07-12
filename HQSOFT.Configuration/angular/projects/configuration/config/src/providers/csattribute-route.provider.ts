import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eConfigurationRouteNames } from '../enums/route-names';

export const CSATTRIBUTES_CSATTRIBUTE_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/configuration/csattributes',
        parentName: eConfigurationRouteNames.Configuration,
        name: 'Configuration::Menu:CSAttributes',
        layout: eLayoutType.application,
        requiredPolicy: 'Configuration.CSAttributes',
      },
    ]);
  };
}
