import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';
import { PaymentComponent } from './payment/payment.component';
import { PaymentListComponent } from './payment-list/payment-list.component';

const routes: Routes = [
    { path: 'list', component: PaymentListComponent },
    { path: 'settings', component: PaymentComponent },
    { path: 'create', component: PaymentComponent },
    { path: 'edit/:id', component: PaymentComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        NgxDatatableModule
    ],
    declarations: [
        PaymentListComponent,
        PaymentComponent
    ],
    exports: [
        RouterModule
    ]
})

export class PaymentModule { }