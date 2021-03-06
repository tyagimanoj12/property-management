import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SettingsService } from '../settings/settings.service';
import { map } from 'rxjs/operators';
import { MenuService } from '../menu/menu.service';
import { adminMenu, userMenu } from '../../routes/menu';
import { Observable } from 'rxjs';
import { LoginResultModel, ChangePasswordResponse } from './administrator.model';

@Injectable()
export class AdministratorService {

    constructor(private http: HttpClient,
        private menuService: MenuService,
        private settings: SettingsService) {
    }

    login(username: string, password: string): Observable<LoginResultModel> {
        return this.http.post<any>(`${this.settings.apiUrl}/token`,
            { username: username, password: password })
            .pipe(map((user: LoginResultModel) => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    console.log(user);

                    this.menuService.menuItems = [];
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));

                    if (user.accountId == null) {
                        // admin
                        this.menuService.addMenu(adminMenu);
                    } else {
                        // user
                        userMenu.forEach(menuItem => {
                            if (menuItem.link) {
                                menuItem.link = menuItem.link.replace(':id', user.accountId);
                            }
                        });
                        this.menuService.addMenu(userMenu);
                    }
                }
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }

    getAccountId() {
        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel;
        return currentUser ? currentUser.accountId : null;
    }

    getAdministratorId() {
        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel;
        return currentUser ? currentUser.administratorId : null;
    }

    loginAs(accountId: string): Observable<LoginResultModel> {
        return this.http.post<any>(`${this.settings.apiUrl}/administrators/loginAs`,
            { accountId: accountId })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // remove admin from local storage to log user out
                    localStorage.removeItem('adminId');
                    localStorage.removeItem('currentUser');

                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    localStorage.setItem('adminId', user.administratorId);
                }
                return user;
            }));
    }

    LoginAsAdmin(administratorId: string): Observable<LoginResultModel> {
        console.log(administratorId);
        console.log('loginAsAdmin');
        return this.http.post<any>(`${this.settings.apiUrl}/administrators/LoginAsAdmin`,
            { administratorId: administratorId })
            .pipe(map(user => {
                console.log(user);
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // remove admin from local storage to log user out
                    localStorage.removeItem('currentUser');
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
                return user;
            }));
    }

    changePassword(ownerId: string , oldpassword: string, newpassword: string) {
        return this.http.put<ChangePasswordResponse>(`${this.settings.apiUrl}/token/UpdatePassword`,
            { ownerId: ownerId, oldpassword: oldpassword, newpassword: newpassword });
    }


}
