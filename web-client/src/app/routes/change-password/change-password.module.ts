import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SharedModule } from '../../shared/shared.module';
import { ChangePasswordComponent } from './change-password.component';

const routes: Routes = [
    { path: 'change-password', component: ChangePasswordComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        ChangePasswordComponent
    ],
    exports: [
        RouterModule
    ]
})

export class ChangePasswordModule { }
