import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPropertyOwnerPagedResource, PropertyOwnerViewModel, CreatePropertyOwnerResponse } from './propertyowner.model';
import { SettingsService } from '../settings/settings.service';
import { Page } from '../common/page.model';
import { SelectItemModel } from '../common/select-item.model';
import { IBaseResponse } from '../common/response.model';


@Injectable()
export class PropertyOwnerService {

    constructor(private http: HttpClient, private settings: SettingsService) {
    }

    getPropertyOwners(page: Page) {
        page.query = page.query ? page.query : '';
        // prepare url
        const url = `${this.settings.apiUrl}/propertyowners?pageNumber=${page.pageNumber + 1}&pageSize=${page.size}&searchQuery=${page.query}`;
        return this.http.get<IPropertyOwnerPagedResource>(url);
    }

    getPropertyOwner(id: string) {
        const url = `${this.settings.apiUrl}/propertyowners/${id}`;
        return this.http.get<PropertyOwnerViewModel>(url);
    }

    savePropertyOwner(model: PropertyOwnerViewModel) {
        console.log('PropertyOwner service');
        if (model.propertyOwnerId) {
            return this.http.put<CreatePropertyOwnerResponse>(`${this.settings.apiUrl}/propertyowners`, model);
        } else {
            return this.http.post<CreatePropertyOwnerResponse>(`${this.settings.apiUrl}/propertyowners`, model);
        }
    }

    deletePropertyOwner(id: string) {
        const url = `${this.settings.apiUrl}/propertyowners/${id}`;
        return this.http.delete<IBaseResponse>(url);
    }
}