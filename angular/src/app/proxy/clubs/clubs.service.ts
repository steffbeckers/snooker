import type { ClubCreateDto, ClubDto, ClubListDto, ClubUpdateDto, GetClubsInput } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ClubPlayerDto, ClubPlayerListDto, GetClubPlayersInput } from '../club-players/models';

@Injectable({
  providedIn: 'root',
})
export class ClubsService {
  apiName = 'Default';

  addPlayer = (id: string, playerId: string) =>
    this.restService.request<any, ClubPlayerDto>({
      method: 'POST',
      url: `/api/app/clubs/${id}/players`,
      params: { playerId },
    },
    { apiName: this.apiName });

  create = (input: ClubCreateDto) =>
    this.restService.request<any, ClubDto>({
      method: 'POST',
      url: '/api/app/clubs',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/clubs/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ClubDto>({
      method: 'GET',
      url: `/api/app/clubs/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetClubsInput) =>
    this.restService.request<any, PagedResultDto<ClubListDto>>({
      method: 'GET',
      url: '/api/app/clubs',
      params: { filterText: input.filterText, name: input.name, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  getPlayersList = (id: string, input: GetClubPlayersInput) =>
    this.restService.request<any, PagedResultDto<ClubPlayerListDto>>({
      method: 'GET',
      url: `/api/app/clubs/${id}/players`,
      params: { filterText: input.filterText, isPrimaryClubOfPlayer: input.isPrimaryClubOfPlayer, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: ClubUpdateDto) =>
    this.restService.request<any, ClubDto>({
      method: 'PUT',
      url: `/api/app/clubs/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
