import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Ng2TableModule } from 'ng2-table/ng2-table';

import { SharedModule } from '../../shared/shared.module';
import { LoginComponent } from './login.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
];

@NgModule({
    imports: [
        SharedModule,
        Ng2TableModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        LoginComponent
    ],
    exports: [
        RouterModule
    ]
})
export class LoginModule { }
