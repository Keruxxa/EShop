import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  standalone: true,
  selector: 'app-empty-basket',
  templateUrl: './empty-basket.component.html',
  styleUrl: './empty-basket.component.scss',
  imports: [RouterLink],
})
export class EmptyBasketComponent {
  private readonly authService = inject(AuthService);

  public readonly isLoggedIn: boolean;

  constructor() {
    this.isLoggedIn = this.authService.isAuthenticated;
  }
}
