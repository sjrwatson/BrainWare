import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { OrderService } from 'src/app/services/order/order.service';

@Component({
  selector: 'web-app-order-display',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-display.component.html',
})
export class OrderDisplayComponent  implements OnInit {
  
  orders: any[] = [];

  constructor(readonly orderService: OrderService) {
  }

  ngOnInit(): void {
    this.orderService.getOrders().subscribe((orders) => {
      this.orders = orders;
    });
  }
}
