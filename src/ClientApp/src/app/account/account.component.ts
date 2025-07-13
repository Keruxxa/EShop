import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { MenuModule } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';
import { InputMaskModule } from 'primeng/inputmask';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'primeng/tabs';

export interface AccountTab {
  label: string;
  route: string;
  icon?: string;
}

@Component({
  standalone: true,
  imports: [
    RippleModule,
    InputTextModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    MenuModule,
    ButtonModule,
    InputMaskModule,
    RouterModule,
    CommonModule,
    TabsModule,
  ],
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrl: './account.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AccountComponent {
  protected formGoup: FormGroup;

  protected tabs: AccountTab[];

  constructor() {
    this.formGoup = new FormGroup({
      firstName: new FormControl(null),
      lastName: new FormControl(null),
      email: new FormControl(null),
      phone: new FormControl(null),
    });

    this.tabs = [
      {
        label: 'Персональные данные',
        route: 'profile',
        icon: 'pi pi-user',
      },
      {
        label: 'Уведомления',
        route: 'notifications',
        icon: 'pi pi-bell',
      },
      {
        label: 'Заказы',
        route: 'orders',
        icon: 'pi pi-shopping-cart',
      },
    ];
  }
}
