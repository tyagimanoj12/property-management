import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { ToasterService, ToasterConfig } from 'angular2-toaster/angular2-toaster';
import { SettingsService } from '../../core/settings/settings.service';
import { AdministratorService } from '../../core/administrators/administrator.service';
import { Router } from '@angular/router';
import { MessageService } from '../../core/common/message.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    valForm: FormGroup;
    loader = false;
    toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });

    constructor(public settings: SettingsService,
        private authService: AdministratorService,
        private messageService: MessageService,
        private router: Router,
        private toasterService: ToasterService,
        fb: FormBuilder) {

        this.valForm = fb.group({
            'username': [null, Validators.required],
            'password': [null, Validators.required]
        });

    }

    submitForm($ev, value: any) {
        $ev.preventDefault();

        Object.keys(this.valForm.controls).forEach(key => {
            this.valForm.controls[key].markAsTouched();
        });
        // for (let c in this.valForm.controls) {
        //     this.valForm.controls[c].markAsTouched();
        // }
        if (this.valForm.valid) {
            console.log('Valid!');
            console.log(value);
            this.loader = true;

            // localStorage.removeItem('messageTypes');
            // localStorage.removeItem('languages');
            // localStorage.removeItem('responseTypes');
            localStorage.clear();

            this.authService.login(value.username, value.password)
                .subscribe(
                    data => {
                        console.log(data.accountId);
                        const role = data.accountId ? 'User' : 'Admin';
                        this.loader = false;
                        console.log(role);
                        this.messageService.sendMessage(role);
                        this.router.navigate(['/home']);
                    },
                    err => {
                        this.loader = false;
                        this.toasterService.pop('error', 'Server Error!', err.error);
                    });
        }
    }

    ngOnInit() {
    }
}
