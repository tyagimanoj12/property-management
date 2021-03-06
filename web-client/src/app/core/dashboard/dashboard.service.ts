import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DashboardDataModel } from './dashboard.modal';
import { SettingsService } from '../settings/settings.service';


@Injectable()
export class DashboardService {

    constructor(private http: HttpClient, private settings: SettingsService) {
    }

    getDashboard() {
        const url = `${this.settings.apiUrl}/dashboard`;
        return this.http.get<DashboardDataModel>(url);
    }
}
