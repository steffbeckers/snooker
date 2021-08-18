import type { EntityDto, FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface ClubCreateDto {
  name: string;
}

export interface ClubDto extends FullAuditedEntityDto<string> {
  name?: string;
}

export interface ClubListDto extends EntityDto<string> {
  name?: string;
}

export interface ClubUpdateDto {
  name: string;
}

export interface GetClubsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
}
