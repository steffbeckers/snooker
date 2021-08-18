import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { PlayerDto } from '../players/models';

@Injectable({
  providedIn: 'root',
})
export class MyPlayerService {
  apiName = 'Default';

  get = () =>
    this.restService.request<any, PlayerDto>({
      method: 'GET',
      url: '/api/app/my-player',
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
