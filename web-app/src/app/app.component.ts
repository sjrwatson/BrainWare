import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule],
  selector: 'web-app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  private http = inject(HttpClient);
  private cd = inject(ChangeDetectorRef);

  orders: any[] = [];

  constructor() {
    this.http.get<any>('/api/order/1').subscribe((orders) => {
      this.orders = orders;
      this.cd.detectChanges();
    });
  }
}
