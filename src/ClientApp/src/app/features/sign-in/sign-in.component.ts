import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';
import { RippleModule } from 'primeng/ripple';
import { AuthService } from '../../core/services/auth.service';
import { SignInUserModel } from './models/sign-in-user-model';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    RippleModule,
    CheckboxModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    MessageModule,
    MessagesModule,
    RouterLink,
  ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss',
  providers: [MessageService],
})
export class SignInComponent {
  public readonly authService = inject(AuthService);
  public readonly router = inject(Router);
  public readonly messageService = inject(MessageService);
  private readonly destroyRef = inject(DestroyRef);

  public formGroup: FormGroup;
  public isOpened: boolean = false;
  public isEmailInvalid: boolean = false;
  public isPasswordInvalid: boolean = false;
  public isLoading: boolean = false;

  constructor() {
    this.formGroup = new FormGroup({
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
    });
  }

  public onSignIn(): void {
    if (this.formGroup.invalid) {
      this.isEmailInvalid = this.formGroup.controls['email'].invalid;
      this.isPasswordInvalid = this.formGroup.controls['password'].invalid;
      return;
    }

    const signInUserModel: SignInUserModel = {
      email: this.formGroup.controls['email'].value,
      password: this.formGroup.controls['password'].value,
    };

    this.isLoading = true;

    this.authService
      .signIn(signInUserModel)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(authResponse => {
        this.isLoading = false;

        if (authResponse.isSuccess) {
          this.router.navigate(['/']);
        }

        if (!this.isOpened) {
          this.messageService.add({
            severity: 'error',
            detail: authResponse.errorMessage,
          });
          this.isOpened = true;
        }
      });
  }

  public changeVisibility(): void {
    this.isOpened = false;
  }

  public emailChange(): void {
    this.isEmailInvalid = this.formGroup.controls['email'].invalid;
  }

  public passwordChange(): void {
    this.isPasswordInvalid = this.formGroup.controls['password'].invalid;
  }
}
