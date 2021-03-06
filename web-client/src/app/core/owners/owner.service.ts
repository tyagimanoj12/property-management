import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IOwnerPagedResource, OwnerViewModel, CreateOwnerResponse } from './owner.modal';
import { SettingsService } from '../settings/settings.service';
import { Page } from '../common/page.model';
import { SelectItemModel } from '../common/select-item.model';
import { IBaseResponse } from '../common/response.model';


@Injectable()
export class OwnerService {

    constructor(private http: HttpClient, private settings: SettingsService) {
    }

    getOwners(page: Page) {
        page.query = page.query ? page.query : '';
        // prepare url
        const url = `${this.settings.apiUrl}/owners?pageNumber=${page.pageNumber + 1}&pageSize=${page.size}&accountId=${page.accountId}&searchQuery=${page.query}`;
        return this.http.get<IOwnerPagedResource>(url);
    }

    getOwner(id: string) {
        const url = `${this.settings.apiUrl}/owners/${id}`;
        return this.http.get<OwnerViewModel>(url);
    }

    saveOwner(model: OwnerViewModel) {        
        if (model.id) {
            return this.http.put<CreateOwnerResponse>(`${this.settings.apiUrl}/owners/UpdateOwner`, model);
        } else {
            return this.http.post<CreateOwnerResponse>(`${this.settings.apiUrl}/owners/CreateOwner`, model);
        }
    }

    deleteOwner(id: string) {
        const url = `${this.settings.apiUrl}/owners/${id}`;
        return this.http.delete<IBaseResponse>(url);
    }
}