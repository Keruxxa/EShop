import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnDestroy, Output } from '@angular/core';
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
import { Subscription } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { SignInUserModel } from './models/sign-in-user-model';
import { SignInUserResponseModel } from './models/sign-in-user-response-model';

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
export class SignInComponent implements OnDestroy {
  @Output() signIn = new EventEmitter<SignInUserResponseModel>();
  public formGroup: FormGroup;
  public isOpened: boolean = false;
  public isEmailInvalid: boolean = false;
  public isPasswordInvalid: boolean = false;
  public isLoading: boolean = false;
  public subscribtions: Subscription | undefined;

  constructor(
    private readonly authService: AuthService,
    private readonly messageService: MessageService,
    private readonly router: Router,
  ) {
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

    this.isLoading = true;

    const signInUserModel: SignInUserModel = {
      email: this.formGroup.controls['email'].value,
      password: this.formGroup.controls['password'].value,
    };

    this.subscribtions = this.authService
      .signIn(signInUserModel)
      .subscribe(isAuthenticated => {
        this.isLoading = false;

        if (isAuthenticated) {
          this.router.navigate(['/']);
        }

        if (!this.isOpened) {
          this.messageService.add({
            severity: 'error',
            detail: 'Неверный Email адрес или пароль',
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

  ngOnDestroy(): void {
    this.subscribtions?.unsubscribe();
  }
}
