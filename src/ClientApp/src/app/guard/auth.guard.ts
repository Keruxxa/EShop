import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AuthService } from '../core/services/auth.service';

export const AuthGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  return authService.isAuthenticated;
};
