import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';

export interface ClubDto extends FullAuditedEntityDto<string> {
  name?: string;
}

export interface CreateClubDto extends EntityDto<string> {
  name: string;
}

export interface UpdateClubDto extends EntityDto<string> {
  name: string;
}
