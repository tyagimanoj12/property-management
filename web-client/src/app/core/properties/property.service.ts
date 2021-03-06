import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPropertyPagedResource, PropertyViewModel, CreatePropertyResponse } from './property.model';
import { SettingsService } from '../settings/settings.service';
import { Page } from '../common/page.model';
import { SelectItemModel } from '../common/select-item.model';
import { IBaseResponse } from '../common/response.model';


@Injectable()
export class PropertyService {

    constructor(private http: HttpClient, private settings: SettingsService) {
    }

    getProperties(page: Page) {
        page.query = page.query ? page.query : '';
        // prepare url
        // tslint:disable-next-line:max-line-length
        const url = `${this.settings.apiUrl}/properties?pageNumber=${page.pageNumber + 1}&pageSize=${page.size}&ownerId=${''}&searchQuery=${page.query}`;
        return this.http.get<IPropertyPagedResource>(url);
    }

    getProperty(id: string) {
        const url = `${this.settings.apiUrl}/properties/${id}`;
        return this.http.get<PropertyViewModel>(url);
    }

    saveProperty(model: PropertyViewModel) {
        if (model.propertyId) {
            return this.http.put<CreatePropertyResponse>(`${this.settings.apiUrl}/properties`, model);
        } else {
            return this.http.post<CreatePropertyResponse>(`${this.settings.apiUrl}/properties`, model);
        }
    }

    deleteProperty(id: string) {
        const url = `${this.settings.apiUrl}/properties/${id}`;
        return this.http.delete<IBaseResponse>(url);
    }
}

