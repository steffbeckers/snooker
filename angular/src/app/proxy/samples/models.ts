import type { IFormFile } from '../microsoft/asp-net-core/http/models';

export interface UploadFile3Dto {
  file: IFormFile;
  firstName?: string;
  test?: string;
}

export interface UploadFilesDto {
  files: IFormFile[];
  test: IFormFile;
  firstName?: string;
  lastName?: string;
}
