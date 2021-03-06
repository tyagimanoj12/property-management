import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';
import { ReportComponent } from './report/report.component';
import { TenantPaymentHistoryComponent } from './tenantpaymenthistory/tenantpaymenthistory.component';
import { TenantPropertyHistoryComponent } from './tenantpropertyhistory/tenantpropertyhistory.component';
import { OwnerPaymentHistoryComponent } from './ownerpaymenthistory/ownerpaymenthistory.component';
import { OwnerPropertyHistoryComponent } from './ownerpropertyhistory/ownerpropertyhistory.component';
import { AssignedPropertyHistoryComponent } from './assignedpropertyhistory/assignedpropertyhistory.component';


const routes: Routes = [
    { path: 'list', component: ReportComponent },
    { path: 'tenant-detail/:id', component: TenantPaymentHistoryComponent },
    { path: 'propertyhistory/:id', component: TenantPropertyHistoryComponent },
    { path: 'property-owner-detail/:id', component: OwnerPaymentHistoryComponent },
    { path: 'ownerpropertyhistory/:id', component: OwnerPropertyHistoryComponent },
    { path: 'assignedpropertyhistory/:id', component: AssignedPropertyHistoryComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        NgxDatatableModule
    ],
    declarations: [
        ReportComponent,
        TenantPaymentHistoryComponent,
        TenantPropertyHistoryComponent,
        OwnerPaymentHistoryComponent,
        OwnerPropertyHistoryComponent,
        AssignedPropertyHistoryComponent
        ],
    exports: [
        RouterModule
    ]
})

export class ReportModule { }