import type { UploadFile3Dto, UploadFilesDto } from './models';
import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';

@Injectable({
  providedIn: 'root',
})
export class SamplesService {
  apiName = 'Default';

  queueManyEmails = () =>
    this.restService.request<any, void>({
      method: 'GET',
      url: '/api/app/samples/queue-many-emails',
    },
    { apiName: this.apiName });

  queueManyEmailsWithDefaultEmailSender = () =>
    this.restService.request<any, void>({
      method: 'GET',
      url: '/api/app/samples/queue-many-emails-with-default-email-sender',
    },
    { apiName: this.apiName });

  throwErrorFromDomainEntity = () =>
    this.restService.request<any, void>({
      method: 'GET',
      url: '/api/app/samples/throw-error-from-domain-entity',
    },
    { apiName: this.apiName });

  uploadFile2ByFirstNameAndTestAndFile = (firstName: string, test: string, file: IFormFile) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/samples/upload-file-2',
    },
    { apiName: this.apiName });

  uploadFile3ByInput = (input: UploadFile3Dto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/samples/upload-file-3',
    },
    { apiName: this.apiName });

  uploadFileByFile = (file: IFormFile) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/samples/upload-file',
    },
    { apiName: this.apiName });

  uploadFilesByInput = (input: UploadFilesDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/samples/upload-files',
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
