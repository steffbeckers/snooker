import type { GetPlayersInput, PlayerCreateDto, PlayerDto, PlayerListDto, PlayerProfileDto, PlayerUpdateDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ClubPlayerListDto, GetClubPlayersInput } from '../club-players/models';
import type { ClubDto } from '../clubs/models';

@Injectable({
  providedIn: 'root',
})
export class PlayersService {
  apiName = 'Default';

  create = (input: PlayerCreateDto) =>
    this.restService.request<any, PlayerDto>({
      method: 'POST',
      url: '/api/app/players',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/players/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, PlayerDto>({
      method: 'GET',
      url: `/api/app/players/${id}`,
    },
    { apiName: this.apiName });

  getClub = (id: string) =>
    this.restService.request<any, ClubDto>({
      method: 'GET',
      url: `/api/app/players/${id}/club`,
    },
    { apiName: this.apiName });

  getClubsList = (id: string, input: GetClubPlayersInput) =>
    this.restService.request<any, PagedResultDto<ClubPlayerListDto>>({
      method: 'GET',
      url: `/api/app/players/${id}/clubs`,
      params: { filterText: input.filterText, isPrimaryClubOfPlayer: input.isPrimaryClubOfPlayer, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  getList = (input: GetPlayersInput) =>
    this.restService.request<any, PagedResultDto<PlayerListDto>>({
      method: 'GET',
      url: '/api/app/players',
      params: { filterText: input.filterText, firstName: input.firstName, lastName: input.lastName, userId: input.userId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  getProfile = (id: string) =>
    this.restService.request<any, PlayerProfileDto>({
      method: 'GET',
      url: `/api/app/players/${id}/profile`,
    },
    { apiName: this.apiName });

  update = (id: string, input: PlayerUpdateDto) =>
    this.restService.request<any, PlayerDto>({
      method: 'PUT',
      url: `/api/app/players/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
