export class PropertyDetailModel {
    total: number;
    vacant: number;
    assigned: number;
    message: string;
    success: boolean;

    constructor() {
        this.total = 0;
        this.vacant = 0;
        this.assigned = 0;
    }
}

export class PaymentDetailModel {
    total: number;
    debit: number;
    credit: number;
    message: string;
    success: boolean;

    constructor() {
        this.total = 0;
        this.debit = 0;
        this.credit = 0;
    }
}

export class DashboardDataModel {
    assignedProperties:any;
    vacantProperties:any;
    totalProperties:any;
    creditPayments:any;
    debitPayments:any;
    totalPayments:any;
    payments: PaymentDetailModel;
    properties: PropertyDetailModel;

    constructor() {
        this.payments = new PaymentDetailModel();
        this.properties = new PropertyDetailModel();     
    }
}

