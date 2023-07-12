import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44333/',
  redirectUri: baseUrl,
  clientId: 'Configuration_App',
  responseType: 'code',
  scope: 'offline_access Configuration',
  requireHttps: true
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Configuration',
    logoUrl: '',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44333',
      rootNamespace: 'HQSOFT.Configuration',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
    Configuration: {
      url: 'https://localhost:44385',
      rootNamespace: 'HQSOFT.Configuration',
    },
  },
} as Environment;
