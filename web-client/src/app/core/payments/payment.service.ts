import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import {
  IPaymentPagedResource,
  PaymentViewModel,
  CreatePaymentResponse
} from "./payment.model";
import { SettingsService } from "../settings/settings.service";
import { Page } from "../common/page.model";
import { SelectItemModel } from "../common/select-item.model";
import { IBaseResponse } from "../common/response.model";

@Injectable()
export class PaymentService {
  constructor(private http: HttpClient, private settings: SettingsService) {}

  getPayments(page: Page) {
    page.query = page.query ? page.query : "";
    // prepare url
    // tslint:disable-next-line:max-line-length
    var tenantId = page.tenantId != null ? page.tenantId : "";
    tenantId = tenantId == undefined ? "" : tenantId;
    var propertyOwnerId = page.propertyOwnerId != null ? page.propertyOwnerId : "";    
    propertyOwnerId = propertyOwnerId == undefined ? "" : propertyOwnerId;
    console.log(tenantId);
    console.log(propertyOwnerId);
    const url = `${this.settings.apiUrl}/payments?pageNumber=${page.pageNumber + 1}&pageSize=${page.size}&tenantId=${tenantId}&propertyOwnerId=${propertyOwnerId}&searchQuery=${page.query}`;
    return this.http.get<IPaymentPagedResource>(url);
  }

  getPayment(id: string) {
    const url = `${this.settings.apiUrl}/payments/${id}`;
    return this.http.get<PaymentViewModel>(url);
  }

  savePayment(model: PaymentViewModel) {
    if (model.paymentId) {
      return this.http.put<CreatePaymentResponse>(
        `${this.settings.apiUrl}/payments`,
        model
      );
    } else {
      return this.http.post<CreatePaymentResponse>(
        `${this.settings.apiUrl}/payments`,
        model
      );
    }
  }

  deletePayment(id: string) {
    const url = `${this.settings.apiUrl}/payments/${id}`;
    return this.http.delete<IBaseResponse>(url);
  }
}
