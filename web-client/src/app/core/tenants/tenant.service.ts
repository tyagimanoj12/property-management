import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITenantPagedResource, TenantViewModel, CreateTenantResponse } from './tenant.model';
import { SettingsService } from '../settings/settings.service';
import { Page } from '../common/page.model';
import { SelectItemModel } from '../common/select-item.model';
import { IBaseResponse } from '../common/response.model';


@Injectable()
export class TenantService {

    constructor(private http: HttpClient, private settings: SettingsService) {
    }

    getTenants(page: Page) {
        page.query = page.query ? page.query : '';
        // prepare url
        const url = `${this.settings.apiUrl}/tenants?pageNumber=${page.pageNumber + 1}&pageSize=${page.size}&searchQuery=${page.query}`;
        return this.http.get<ITenantPagedResource>(url);
    }

    getTenant(id: string) {
        const url = `${this.settings.apiUrl}/tenants/${id}`;
        return this.http.get<TenantViewModel>(url);
    }

    saveTenant(model: TenantViewModel) {
        console.log('tenant service');
        if (model.tenantId) {
            return this.http.put<CreateTenantResponse>(`${this.settings.apiUrl}/tenants`, model);
        } else {
            return this.http.post<CreateTenantResponse>(`${this.settings.apiUrl}/tenants`, model);
        }
    }

    deleteTenant(id: string) {
        const url = `${this.settings.apiUrl}/tenants/${id}`;
        return this.http.delete<IBaseResponse>(url);
    }
}
