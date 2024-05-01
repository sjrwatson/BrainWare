import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(readonly http: HttpClient) {
  }

  getOrders()  {
    return this.http.get<any>('/api/order/1');
  }
}
