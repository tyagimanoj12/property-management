import { LayoutComponent } from '../layout/layout.component';
import { AuthGuard } from '../core/auth.gaurd';

export const routes = [

    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: 'login', loadChildren: './login/login.module#LoginModule' },
            { path: 'user', loadChildren: './change-password/change-password.module#ChangePasswordModule', canActivate: [AuthGuard] },
            { path: 'home', loadChildren: './home/home.module#HomeModule', canActivate: [AuthGuard] },
            { path: 'tenants', loadChildren: './tenants/tenant.module#TenantModule', canActivate: [AuthGuard] },
            { path: 'properties', loadChildren: './properties/property.module#PropertyModule', canActivate: [AuthGuard] },
            { path: 'assignedproperties', loadChildren: './assignedproperties/assignedproperty.module#AssignedPropertyModule', canActivate: [AuthGuard] },
            { path: 'propertyowners', loadChildren: './propertyowners/propertyowner.module#PropertyOwnerModule', canActivate: [AuthGuard] },
            { path: 'payments', loadChildren: './payments/payment.module#PaymentModule', canActivate: [AuthGuard] },
            { path: 'reports', loadChildren: './reports/report.module#ReportModule', canActivate: [AuthGuard] },
            { path: '', redirectTo: 'home', pathMatch: 'full', canActivate: [AuthGuard] }
        ]
    },

    // Not found
    { path: '**', redirectTo: 'home' }

];
