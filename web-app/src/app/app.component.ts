import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FooterComponent } from 'src/app/components/footer/footer.component';
import { OrderContainerComponent } from 'src/app/components/orders/order-container.component';

@Component({
  standalone: true,
  imports: [CommonModule, FooterComponent, OrderContainerComponent],
  selector: 'web-app-root',
  templateUrl: './app.component.html',
})
export class AppComponent { }
