import { Component, OnInit, ViewChild } from '@angular/core';
const screenfull = require('screenfull');
const browser = require('jquery.browser');
declare var $: any;

import { UserblockService } from '../sidebar/userblock/userblock.service';
import { SettingsService } from '../../core/settings/settings.service';
import { MenuService } from '../../core/menu/menu.service';
import { AdministratorService } from '../../core/administrators/administrator.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    isAdminLogin:boolean=false;
    navCollapsed = true; // for horizontal layout
    menuItems = []; // for horizontal layout

    isNavSearchVisible: boolean;
    @ViewChild('fsbutton') fsbutton;  // the fullscreen button

    constructor(public menu: MenuService,
        public userblockService: UserblockService,
        public settings: SettingsService,
        public administratorService: AdministratorService,
        private router: Router) {

        // show only a few items on demo
        this.menuItems = menu.getMenu().slice(0, 4); // for horizontal layout

    }

    logout() {
        this.administratorService.logout();
        localStorage.removeItem('adminId');
        this.router.navigate(['/login']);
    }

    loginAsAdmin() {

        console.log(this.adminId);    
        
        this.administratorService.LoginAsAdmin(this.adminId)
            .subscribe(x => {
                console.log('Result', x);
                this.router.navigate(['/Home']);
                this.isAdminLogin=false;
                location.reload();
                this.router.navigate(['/accounts/list']);
                console.log(this.isAdminLogin);                

            });
    }    

    get adminId(): string {
        return localStorage.getItem('adminId');
    }

    ngOnInit() {

        //console.log('header'+this.adminId);

        if(this.adminId)
        {
            this.isAdminLogin=true;
        }
        else{
            this.isAdminLogin=false;
        }
        
        this.isNavSearchVisible = false;
        if (browser.msie) { // Not supported under IE
            this.fsbutton.nativeElement.style.display = 'none';
        }
    }

    toggleUserBlock(event) {
        event.preventDefault();
        this.userblockService.toggleVisibility();
    }

    openNavSearch(event) {
        event.preventDefault();
        event.stopPropagation();
        this.setNavSearchVisible(true);
    }

    setNavSearchVisible(stat: boolean) {
        // console.log(stat);
        this.isNavSearchVisible = stat;
    }

    getNavSearchVisible() {
        return this.isNavSearchVisible;
    }

    toggleOffsidebar() {
        this.settings.toggleLayoutSetting('offsidebarOpen');
    }

    toggleCollapsedSideabar() {
        this.settings.toggleLayoutSetting('isCollapsed');
    }

    isCollapsedText() {
        return this.settings.getLayoutSetting('isCollapsedText');
    }

    toggleFullScreen(event) {

        if (screenfull.enabled) {
            screenfull.toggle();
        }
        // Switch icon indicator
        let el = $(this.fsbutton.nativeElement);
        if (screenfull.isFullscreen) {
            el.children('em').removeClass('fa-expand').addClass('fa-compress');
        }
        else {
            el.children('em').removeClass('fa-compress').addClass('fa-expand');
        }
    }
}
