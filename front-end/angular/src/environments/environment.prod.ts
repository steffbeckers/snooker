import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Snooker',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44326',
    redirectUri: baseUrl,
    clientId: 'Snooker_App',
    responseType: 'code',
    scope: 'offline_access Snooker',
  },
  apis: {
    default: {
      url: 'https://localhost:44326',
      rootNamespace: 'Snooker',
    },
  },
} as Environment;
