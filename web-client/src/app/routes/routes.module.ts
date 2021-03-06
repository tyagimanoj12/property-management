import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslatorService } from '../core/translator/translator.service';
import { MenuService } from '../core/menu/menu.service';
import { SharedModule } from '../shared/shared.module';
import { adminMenu, userMenu } from './menu';
import { routes } from './routes';
import { PagesModule } from './pages/pages.module';
import { LoginModule } from './login/login.module';
import { ChangePasswordModule } from './change-password/change-password.module';
import {TenantModule} from './tenants/tenant.module';
import {PropertyModule} from './properties/property.module';
import {AssignedPropertyModule} from './assignedproperties/assignedproperty.module';
import {PropertyOwnerModule} from './propertyowners/propertyowner.module';
import {PaymentModule} from './payments/payment.module';
import {ReportModule} from './reports/report.module';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forRoot(routes),
        PagesModule,
        LoginModule,
        ChangePasswordModule,
        TenantModule,
        PropertyModule,
        AssignedPropertyModule,
        PropertyOwnerModule,
        PaymentModule,
        ReportModule
    ],
    declarations: [],
    exports: [
        RouterModule
    ]
})

export class RoutesModule {
    constructor(public menuService: MenuService, tr: TranslatorService) {
        // this handles the case of page refresh
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser) {
            menuService.menuItems = [];
            if (currentUser.accountId == null) {
                // admin
                menuService.addMenu(adminMenu);
            } else {
                // user
                menuService.addMenu(userMenu);
            }
        }
    }
}
