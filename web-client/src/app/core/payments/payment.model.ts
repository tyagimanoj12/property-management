import { Page } from '../common/page.model';

export interface IPaymentPagedResource {
    data: PaymentViewModel[];
    page: Page;
}

export class PaymentViewModel {
    paymentId: string;
    propertyId: string;
    tenantId: string;
    amount: string;
    credit: string;
    debit: string;
    propertyName: string;
    propertyOwnerName: string;
    tenantName: string;
    paymentType?:any;
    propertyOwnerId:string;
    
}

export class CreatePaymentResponse {
    paymentId: string;
    success: boolean;
    message: string;
}
