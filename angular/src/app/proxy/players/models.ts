import type { EntityDto, FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetPlayersInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  firstName?: string;
  lastName?: string;
  userId?: string;
}

export interface PlayerCreateDto {
  email?: string;
  firstName: string;
  lastName: string;
  password?: string;
  userId?: string;
}

export interface PlayerDto extends FullAuditedEntityDto<string> {
  firstName?: string;
  lastName?: string;
  userId?: string;
}

export interface PlayerListDto extends EntityDto<string> {
  firstName?: string;
  lastName?: string;
}

export interface PlayerProfileClubDto extends EntityDto<string> {
  name?: string;
}

export interface PlayerProfileDto extends EntityDto<string> {
  club: PlayerProfileClubDto;
  firstName?: string;
  lastName?: string;
}

export interface PlayerUpdateDto {
  firstName: string;
  lastName: string;
  userId?: string;
}
