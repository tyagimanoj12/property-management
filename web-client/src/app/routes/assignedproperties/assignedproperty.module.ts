import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';
import { AssignedPropertyComponent } from './assignedproperty/assignedproperty.component';
import { AssignedPropertyListComponent } from './assignedproperty-list/assignedproperty-list.component';

const routes: Routes = [
    { path: 'list', component: AssignedPropertyListComponent },
    { path: 'settings', component: AssignedPropertyComponent },
    { path: 'create', component: AssignedPropertyComponent },
    { path: 'edit/:id', component: AssignedPropertyComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        NgxDatatableModule
    ],
    declarations: [
        AssignedPropertyListComponent,
        AssignedPropertyComponent,
    ],
    exports: [
        RouterModule
    ]
})

export class AssignedPropertyModule { }