import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { BasketComponent } from './basket/basket/basket.component';
import { LayoutComponent } from './components/layout/layout.component';
import { SignInComponent } from './features/sign-in/sign-in.component';
import { SignUpComponent } from './features/sign-up/sign-up.component';
import { ProfileComponent } from './account/profile/profile.component';
import { NotificationsComponent } from './account/notifications/notifications.component';
import { OrdersComponent } from './account/orders/orders.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: 'basket', component: BasketComponent },
      {
        path: 'account',
        component: AccountComponent,
        children: [
          { path: 'profile', component: ProfileComponent },
          { path: 'notifications', component: NotificationsComponent },
          { path: 'orders', component: OrdersComponent },
          { path: '', redirectTo: 'profile', pathMatch: 'full' },
        ],
      },
    ],
  },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
