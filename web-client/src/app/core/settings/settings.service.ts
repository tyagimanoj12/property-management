import { Injectable } from '@angular/core';
import { SelectItemModel } from '../common/select-item.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

declare var $: any;

@Injectable()
export class SettingsService {

    private user: any;
    private app: any;
    private layout: any;
    public apiUrl: any;

    constructor(private http: HttpClient) {

        // local
          this.apiUrl = 'http://localhost:5000/api';

         // this.apiUrl = 'http://localhost:57736/api';

          //this.apiUrl='http://7e124334.ngrok.io/api'

        // dev/test
          //this.apiUrl = 'http://property.dslenglish.com/api';

       

        // User Settings
        // -----------------------------------
        this.user = {
            name: 'John',
            job: 'ng-developer',
            picture: 'assets/img/user/02.jpg'
        };

        // App Settings
        // -----------------------------------
        this.app = {
            name: 'Property Management',
            description: 'Property-Management',
            year: ((new Date()).getFullYear())
        };

        // Layout Settings
        // -----------------------------------
        this.layout = {
            isFixed: true,
            isCollapsed: false,
            isBoxed: false,
            isRTL: false,
            horizontal: false,
            isFloat: false,
            asideHover: false,
            theme: null,
            asideScrollbar: false,
            isCollapsedText: false,
            useFullLayout: false,
            hiddenFooter: false,
            offsidebarOpen: false,
            asideToggled: false,
            viewAnimation: 'ng-fadeInUp'
        };
    }

    getAppSetting(name) {
        return name ? this.app[name] : this.app;
    }
    getUserSetting(name) {
        return name ? this.user[name] : this.user;
    }
    getLayoutSetting(name) {
        return name ? this.layout[name] : this.layout;
    }

    setAppSetting(name, value) {
        if (typeof this.app[name] !== 'undefined') {
            this.app[name] = value;
        }
    }
    setUserSetting(name, value) {
        if (typeof this.user[name] !== 'undefined') {
            this.user[name] = value;
        }
    }
    setLayoutSetting(name, value) {
        if (typeof this.layout[name] !== 'undefined') {
            return this.layout[name] = value;
        }
    }

    toggleLayoutSetting(name) {
        return this.setLayoutSetting(name, !this.getLayoutSetting(name));
    }

}
