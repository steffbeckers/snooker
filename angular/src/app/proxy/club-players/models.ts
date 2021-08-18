import type { EntityDto, FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { ClubListDto } from '../clubs/models';
import type { PlayerListDto } from '../players/models';

export interface ClubPlayerDto extends FullAuditedEntityDto<string> {
  clubId?: string;
  isPrimaryClubOfPlayer: boolean;
  playerId?: string;
}

export interface ClubPlayerListDto extends EntityDto<string> {
  club: ClubListDto;
  isPrimaryClubOfPlayer: boolean;
  player: PlayerListDto;
}

export interface GetClubPlayersInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  isPrimaryClubOfPlayer?: boolean;
}
