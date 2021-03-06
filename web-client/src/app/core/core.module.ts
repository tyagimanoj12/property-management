import { NgModule, Optional, SkipSelf } from '@angular/core';

import { SettingsService } from './settings/settings.service';
import { ThemesService } from './themes/themes.service';
import { TranslatorService } from './translator/translator.service';
import { MenuService } from './menu/menu.service';

import { throwIfAlreadyLoaded } from './module-import-guard';
import { AdministratorService } from './administrators/administrator.service';
import { AuthGuard } from './auth.gaurd';
import { ToasterService } from 'angular2-toaster/angular2-toaster';
import { MessageService } from './common/message.service';
import { HomeService } from './home/home.service';
import { TenantService } from './tenants/tenant.service';
import { PropertyService } from './properties/property.service';
import { AssignedPropertyService } from './assignedproperties/assignedproperty.service';
import { PropertyOwnerService } from './propertyowners/propertyowner.service';
import { PaymentService } from './payments/payment.service';
import { DashboardService } from './dashboard/dashboard.service';



@NgModule({
    imports: [
    ],
    providers: [
        SettingsService,
        ThemesService,
        TranslatorService,
        MenuService,
        AdministratorService,
        AuthGuard,
        ToasterService,
        MessageService,
        HomeService,
        TenantService,
        PropertyService,
        AssignedPropertyService,
        PropertyOwnerService,
        PaymentService,
        DashboardService

        ],
    declarations: [
    ],
    exports: [
    ]
})
export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, 'CoreModule');
    }
}
