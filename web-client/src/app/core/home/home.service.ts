import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DashboardDataModel } from './home.model';
import { SettingsService } from '../settings/settings.service';


@Injectable()
export class HomeService {

    constructor(private http: HttpClient, private settings: SettingsService) {
    }

    getDashboard() {
        const url = `${this.settings.apiUrl}/dashboard`;
        return this.http.get<DashboardDataModel>(url);
    }
}
