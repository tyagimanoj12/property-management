import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';
import { TenantComponent } from './tenant/tenant.component';
import { TenantListComponent } from './tenant-list/tenant-list.component';
import { MatDatepickerModule } from '@angular/material';


const routes: Routes = [
    { path: 'list', component: TenantListComponent },
    { path: 'settings', component: TenantComponent },
    { path: 'create', component: TenantComponent },
    { path: 'edit/:id', component: TenantComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        NgxDatatableModule,
        MatDatepickerModule
    ],
    declarations: [
        TenantListComponent,
        TenantComponent,
    ],
    exports: [
        RouterModule
    ]
})

export class TenantModule { }