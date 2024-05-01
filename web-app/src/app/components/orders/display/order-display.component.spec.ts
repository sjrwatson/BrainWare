import { ComponentFixture, TestBed } from '@angular/core/testing';
import { OrderDisplayComponent } from './order-display.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('OrderDisplayComponent', () => {
  let component: OrderDisplayComponent;
  let fixture: ComponentFixture<OrderDisplayComponent>;
  let mockOrderService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderDisplayComponent, HttpClientTestingModule],
    }).compileComponents();

    fixture = TestBed.createComponent(OrderDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call order service', () => {
    const spy = jest.spyOn(component.orderService, 'getOrders');
    component.ngOnInit();
    expect(spy).toHaveBeenCalledTimes(1);
  });
});
