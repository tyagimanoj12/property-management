import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';
import { PropertyComponent } from './property/property.component';
import { PropertyListComponent } from './property-list/property-list.component';

const routes: Routes = [
    { path: 'list', component: PropertyListComponent },
    { path: 'settings', component: PropertyComponent },
    { path: 'create', component: PropertyComponent },
    { path: 'edit/:id', component: PropertyComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        NgxDatatableModule
    ],
    declarations: [
        PropertyListComponent,
        PropertyComponent,
    ],
    exports: [
        RouterModule
    ]
})

export class PropertyModule { }