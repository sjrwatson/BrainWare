import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderHeaderComponent } from './header/order-header.component';
import { OrderDisplayComponent } from './display/order-display.component';

@Component({
  selector: 'web-app-order-container',
  standalone: true,
  imports: [CommonModule, OrderHeaderComponent, OrderDisplayComponent],
  templateUrl: './order-container.component.html',
})
export class OrderContainerComponent {}
