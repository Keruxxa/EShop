import { CommonModule } from '@angular/common';
import { Component, inject, output, signal } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { TabsModule } from 'primeng/tabs';
import { UserService } from '../../modules/user/services/user.service';
import { FloatLabel } from 'primeng/floatlabel';
import { Skeleton } from 'primeng/skeleton';
import { MessageService } from 'primeng/api';
import { userIdKey } from '../../core/constants';
import { take } from 'rxjs';
import { Toast } from 'primeng/toast';

interface ProfileFormGroup {
  email: FormControl<string>;
  firstName: FormControl<string | null>;
  lastName: FormControl<string | null>;
  phone: FormControl<string | null>;
}

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    RippleModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    InputMaskModule,
    RouterModule,
    CommonModule,
    TabsModule,
    FloatLabel,
    Skeleton,
    Toast,
  ],
  providers: [MessageService, UserService],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent {
  private readonly userService = inject(UserService);
  private readonly messageService = inject(MessageService);

  protected formGoup: FormGroup<ProfileFormGroup>;
  public saveEvent = output<void>();
  protected isLoading = signal<boolean>(true);

  constructor() {
    this.formGoup = new FormGroup<ProfileFormGroup>({
      email: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      firstName: new FormControl<string | null>(null),
      lastName: new FormControl<string | null>(null),
      phone: new FormControl<string | null>(null),
    });

    const userId = localStorage.getItem(userIdKey);

    if (!userId) {
      throw new Error('UserId is not defined');
    }

    this.userService.getUserById(userId).subscribe(user => {
      this.formGoup.patchValue({
        email: user.email,
        firstName: user.firstName,
        lastName: user.lastName,
        phone: user.phone,
      });
      this.isLoading.set(false);
    });
  }

  onSave(): void {
    if (this.formGoup.invalid) {
      return;
    }
    if (this.formGoup.pristine) {
      return;
    }

    const userId = localStorage.getItem(userIdKey);

    if (!userId) {
      throw new Error('UserId is not defined');
    }

    this.userService
      .updateUser(userId, {
        firstName: this.formGoup.controls['firstName'].value,
        lastName: this.formGoup.controls['lastName'].value,
      })
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Сохранено!',
          });
          this.formGoup.markAsPristine();
        },
        error: error => {
          console.log('An error ocured while onSave: ', error);
        },
      });

    this.saveEvent.emit();
  }
}
