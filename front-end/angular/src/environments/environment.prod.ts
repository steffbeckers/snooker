import { Environment } from '@abp/ng.core';

const baseUrl = 'https://snooker.steffbeckers.eu';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Snooker',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://api.snooker.steffbeckers.eu',
    redirectUri: baseUrl,
    clientId: 'Snooker_App',
    responseType: 'code',
    scope: 'offline_access Snooker',
  },
  apis: {
    default: {
      url: 'https://api.snooker.steffbeckers.eu',
      rootNamespace: 'Snooker',
    },
  },
} as Environment;
