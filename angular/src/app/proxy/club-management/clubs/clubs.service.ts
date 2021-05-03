import type { ClubDto, CreateClubDto, UpdateClubDto } from './dto/models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ClubsService {
  apiName = 'Default';

  create = (input: CreateClubDto) =>
    this.restService.request<any, ClubDto>({
      method: 'POST',
      url: '/api/app/club-management/clubs',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/club-management/clubs/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ClubDto>({
      method: 'GET',
      url: `/api/app/club-management/clubs/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ClubDto>>({
      method: 'GET',
      url: '/api/app/club-management/clubs',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateClubDto) =>
    this.restService.request<any, ClubDto>({
      method: 'PUT',
      url: `/api/app/club-management/clubs/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
