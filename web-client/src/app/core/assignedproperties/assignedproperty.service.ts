import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IAssignedPropertyPagedResource, AssignedPropertyViewModel, CreateAssignedPropertyResponse } from './assignedproperty.model';
import { SettingsService } from '../settings/settings.service';
import { Page } from '../common/page.model';
import { SelectItemModel } from '../common/select-item.model';
import { IBaseResponse } from '../common/response.model';


@Injectable()
export class AssignedPropertyService {

    constructor(private http: HttpClient, private settings: SettingsService) {
    }

    getAssignedProperties(page: Page) {
        page.query = page.query ? page.query : '';
        var tenantId = page.tenantId != null ? page.tenantId : "";
        tenantId = tenantId == undefined ? "" : tenantId;
        var propertyOwnerId = page.propertyOwnerId != null ? page.propertyOwnerId : "";    
        propertyOwnerId = propertyOwnerId == undefined ? "" : propertyOwnerId;
        // prepare url
        const url = `${this.settings.apiUrl}/assignedproperties?pageNumber=${page.pageNumber + 1}&pageSize=${page.size}&tenantId=${tenantId}&propertyOwnerId=${propertyOwnerId}&ownerId=${''}&searchQuery=${page.query}`;
        return this.http.get<IAssignedPropertyPagedResource>(url);
    }

    getAssignedProperty(id: string) {
        const url = `${this.settings.apiUrl}/assignedproperties/${id}`;
        return this.http.get<AssignedPropertyViewModel>(url);
    }

    saveAssignedProperty(model: AssignedPropertyViewModel) {
 console.log(model.formdata);
        if (model.assignedPropertyId) {
            return this.http.put<CreateAssignedPropertyResponse>(`${this.settings.apiUrl}/assignedproperties`, model);
        } else {
            return this.http.post<CreateAssignedPropertyResponse>(`${this.settings.apiUrl}/assignedproperties`, model);
        }
    }

    upload(formData: any) {        
      return  this.http.post(`${this.settings.apiUrl}/upload`, formData, {reportProgress: true, observe: 'events'});          
    }    

    deleteAssignedProperty(id: string) {
        const url = `${this.settings.apiUrl}/assignedproperties/${id}`;
        return this.http.delete<IBaseResponse>(url);
    }
}