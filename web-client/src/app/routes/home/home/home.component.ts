import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { DashboardService } from '../../../core/dashboard/dashboard.service';
import { AdministratorService } from '../../../core/administrators/administrator.service';
import { Router } from '@angular/router';
import { DashboardDataModel } from '../../../core/dashboard/dashboard.modal';
import { ColorsService } from '../../../shared/colors/colors.service';


@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

   

    dashboardData: DashboardDataModel = new DashboardDataModel();

    barColors = [
        {
            backgroundColor: this.colors.byName('gray-lighter'),
            borderColor: this.colors.byName('gray-lighter'),
            pointHoverBackgroundColor: this.colors.byName('gray-lighter'),
            pointHoverBorderColor: this.colors.byName('gray-lighter')
        }, {
            backgroundColor: this.colors.byName('danger'),
            borderColor: this.colors.byName('danger'),
            pointHoverBackgroundColor: this.colors.byName('danger'),
            pointHoverBorderColor: this.colors.byName('danger')
        }];

    barOptions = {
        scaleShowVerticalLines: false,
        responsive: true,
        scales: {
            yAxes: [{
                ticks: {
                    fontColor: 'white'
                },
            }],
            xAxes: [{
                ticks: {
                    beginAtZero: true,
                    fontColor: 'white',
                    stepSize: 1
                }
            }]
        }
    };

    callsSentVsResponsesBarData = {
        labels: ['Sent', 'Responses'],
        datasets: [
            { data: [] }
        ]
    };

    callsSentVsConfirmationBarData = {
        labels: ['Sent', 'Confirmation'],
        datasets: [
            { data: [] }
        ]
    };

    smsSentVsResponsesBarData = {
        labels: ['Sent', 'Responses'],
        datasets: [
            { data: [] }
        ]
    };

    smsSentVsConfirmationBarData = {
        labels: ['Sent', 'Confirmation'],
        datasets: [
            { data: [] }
        ]
    };

    emailsSentVsResponsesBarData = {
        labels: ['Sent', 'Responses'],
        datasets: [
            { data: [] }
        ]
    };

    emailsSentVsConfirmationBarData = {
        labels: ['Sent', 'Confirmation'],
        datasets: [
            { data: [] }
        ]
    };

    totalSentVsResponsesBarData = {
        labels: ['Sent', 'Responses'],
        datasets: [
            { data: [] }
        ]
    };

    totalSentVsConfirmationBarData = {
        labels: ['Sent', 'Confirmation'],
        datasets: [
            { data: [] }
        ]
    };

    constructor(private dashboardService: DashboardService,
        private authService: AdministratorService,
        private colors: ColorsService,
        private router: Router) {
        this.dashboardService.getDashboard()
            .subscribe(x => {
                this.dashboardData = x; 
                console.log(this.dashboardData);

                    // //call sent vs. responses data for bar chart
                    // this.callsSentVsResponsesBarData.datasets = [
                    // { data: [ x.payments.credit , x.payments.debit ] }
                    // ];

                    //call sent vs. confirmations data for bar chart
                    // this.callsSentVsConfirmationBarData.datasets = [
                    // { data: [ x.properties.assigned , x.properties.vacant ] }
                    // ];

                    // //sms sent vs. responses data for bar chart
                    // this.smsSentVsResponsesBarData.datasets = [
                    // { data: [ x.textMessages.sent , x.textMessages.responses ] }
                    // ];

                    // //sms sent vs. confirmations data for bar chart 
                    // this.smsSentVsConfirmationBarData.datasets = [
                    // { data: [ x.textMessages.sent , x.textMessages.confirmations ] }
                    // ]; 

                    // //emails sent vs. responses data for bar chart
                    // this.emailsSentVsResponsesBarData.datasets = [
                    // { data: [ x.emails.sent , x.emails.responses ] }
                    // ];

                    // //emails sent vs. confirmations data for bar chart
                    // this.emailsSentVsConfirmationBarData.datasets = [
                    // { data: [ x.emails.sent , x.emails.confirmations ] }
                    // ];

                    // //total sent vs. responses data for bar chart
                    // this.totalSentVsResponsesBarData.datasets = [
                    // { data: [ x.total.sent , x.total.responses ] }
                    // ];

                    // //total sent vs. confirmations data for bar chart
                    // this.totalSentVsConfirmationBarData.datasets = [
                    // { data: [ x.total.sent , x.total.confirmations ] }
                    // ];
            });
    }

    ngOnInit() {
    }

}
