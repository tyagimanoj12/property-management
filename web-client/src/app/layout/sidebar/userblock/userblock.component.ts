import { Component, OnInit } from '@angular/core';

import { LoginResultModel } from '../../../core/administrators/administrator.model';
import { UserblockService } from './userblock.service';

@Component({
    selector: 'app-userblock',
    templateUrl: './userblock.component.html',
    styleUrls: ['./userblock.component.scss']
})
export class UserblockComponent implements OnInit {
    user: any;
    
    constructor(public userblockService: UserblockService) {
        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel;
        this.user = {
            picture: 'assets/img/user/01.jpg',
            practiceName: currentUser.practiceName
        };
    }

    ngOnInit() {
    }

    userBlockIsVisible() {
        return this.userblockService.getVisibility();
    }

}
