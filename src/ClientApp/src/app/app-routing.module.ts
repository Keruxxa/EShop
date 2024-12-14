import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket/basket/basket.component';
import { LayoutComponent } from './components/layout/layout.component';
import { SignInComponent } from './features/sign-in/sign-in.component';
import { SignUpComponent } from './features/sign-up/sign-up.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [{ path: 'basket', component: BasketComponent }],
  },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'basket', component: BasketComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
