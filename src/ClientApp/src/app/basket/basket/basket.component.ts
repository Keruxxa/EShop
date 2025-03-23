import { Component } from '@angular/core';
import { EmptyBasketComponent } from './empty-basket/empty-basket/empty-basket.component';

@Component({
  standalone: true,
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss',
  imports: [EmptyBasketComponent],
})
export class BasketComponent {
  public readonly orders: any[] = [];
}
