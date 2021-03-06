import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LoginResultModel } from './administrators/administrator.model';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (localStorage.getItem('currentUser')) {
            // logged in so return true
            const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel;
            // check if token is expired or not
            const date1 = new Date(currentUser.expireAt).getTime();
            const date2 = new Date().getTime();
            console.log('expireAt', date1);
            console.log('currentDate', date2);
            if (date1 > date2) {
                return true;
            }
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}
