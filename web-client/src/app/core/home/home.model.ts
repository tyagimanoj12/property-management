export class TransactionDetailModel {
    sent: number;
    responses: number;
    confirmations: number;
    message: string;
    success: boolean;

    constructor() {
        this.sent = 0;
        this.responses = 0;
        this.confirmations = 0;
    }
}

export class DashboardDataModel {
    phoneCalls: TransactionDetailModel;
    textMessages: TransactionDetailModel;
    emails: TransactionDetailModel;
    total: TransactionDetailModel;

    constructor() {
        this.phoneCalls = new TransactionDetailModel();
        this.textMessages = new TransactionDetailModel();
        this.emails = new TransactionDetailModel();
        this.total = new TransactionDetailModel();
    }
}

