import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { API_BASE_URL } from '../config/api-base-url.token';

export type OpsInfo = {
  service: string;
  gitSha?: string;
  buildId?: string;
  requirementId?: string;
  pod?: {
    name?: string;
    uid?: string;
    node?: string;
    namespace?: string;
  };
};

@Injectable({ providedIn: 'root' })
export class OpsInfoService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = inject(API_BASE_URL);

  getInfo() {
    return this.http.get<OpsInfo>(`${this.baseUrl}/ops/info`);
  }
}

