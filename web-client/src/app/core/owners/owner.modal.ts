import { Page } from '../common/page.model';

export interface IOwnerPagedResource {
    data: OwnerViewModel[];
    page: Page;
}

export class OwnerViewModel {
    id: string;
    username:string;
    password:string;
    name: string;
    email?: string;
    phone: string;
    address: string;    
}

export class CreateOwnerResponse {
    tenantId: string;
    success: boolean;
    message: string;
}