import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-basket',
  standalone: true,
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss',
  imports: [RouterLink],
})
export class BasketComponent {}
