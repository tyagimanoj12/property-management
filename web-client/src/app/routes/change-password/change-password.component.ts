import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToasterService, ToasterConfig } from 'angular2-toaster/angular2-toaster';
import { SettingsService } from '../../core/settings/settings.service';
import { AdministratorService } from '../../core/administrators/administrator.service';
import { LoginResultModel } from '../../core/administrators/administrator.model';
import { Router } from '@angular/router';

@Component({
    selector: 'change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
    ownerId: string;

    public changePasswordForm: FormGroup;
    loader = false;
    submitted = false;
    toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
   

    constructor(public settings: SettingsService,
        
        private authService: AdministratorService,
        private router: Router,
        private administratorService: AdministratorService,
        private toasterService: ToasterService,
        private fb: FormBuilder) {
        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel;
        console.log(currentUser);
        this.ownerId = currentUser.ownerId;
    }
    
    ngOnInit() {
        this.changePasswordForm = this.fb.group({
            'oldpassword': ['', Validators.required],
            'newpassword': ['', Validators.required],
            'confirmpassword': ['', Validators.required],
            ownerId: this.ownerId
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.changePasswordForm.controls; }

    save(model: any, isValid: boolean, e: any) {
        this.submitted = true;
        e.preventDefault();

        console.log(isValid);
        console.log(model);

        if (this.changePasswordForm.valid) {

            if (model.newpassword !== model.confirmpassword) {
                this.toasterService.pop('error', 'Error!', 'New password and confirm password do not match.');
                return;
            }

            console.log('Valid!');

            this.loader = true;
            // this.toasterService.pop('success', 'Edit service for password change', 'Change password api call.');

            this.authService.changePassword(model.ownerId, model.oldpassword, model.newpassword)
                .subscribe(
                    res => {
                        this.loader = false;
                        if (res.success) {
                            this.toasterService.pop('success', 'Success!', 'Password has been successfully changed.');
                            setTimeout(() => {
                                this.router.navigate(['/home']);
                            }, 2000);
                        }
                    },
                    err => {
                        this.loader = false;
                        this.toasterService.pop('error', 'Server Error!', err.error);
                    });
        }
    }
    
}
