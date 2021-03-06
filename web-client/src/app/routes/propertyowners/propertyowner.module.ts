import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';
import { PropertyOwnerComponent } from './propertyowner/propertyowner.component';
import { PropertyOwnerListComponent } from './propertyowner-list/propertyowner-list.component';

const routes: Routes = [
    { path: 'list', component: PropertyOwnerListComponent },
    { path: 'settings', component: PropertyOwnerComponent },
    { path: 'create', component: PropertyOwnerComponent },
    { path: 'edit/:id', component: PropertyOwnerComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        NgxDatatableModule
    ],
    declarations: [
        PropertyOwnerListComponent,
        PropertyOwnerComponent,
    ],
    exports: [
        RouterModule
    ]
})

export class PropertyOwnerModule { }