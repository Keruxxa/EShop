import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
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
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';
import { RippleModule } from 'primeng/ripple';
import { Subscription } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { SignUpUserModel } from './models/sign-up-user-model';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss',
  imports: [
    RouterLink,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RippleModule,
    ButtonModule,
    InputTextModule,
    MessageModule,
    MessagesModule,
  ],
  providers: [MessageService],
})
export class SignUpComponent implements OnDestroy {
  public formGroup: FormGroup;
  public isOpened: boolean = false;
  public isEmailInvalid: boolean = false;
  public isPasswordInvalid: boolean = false;
  public isLoading: boolean = false;
  public subscribtions: Subscription | undefined;

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router,
    private readonly messageService: MessageService,
  ) {
    this.formGroup = new FormGroup({
      firstName: new FormControl(null),
      lastName: new FormControl(null),
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
    });
  }

  public emailChange(): void {
    this.isEmailInvalid = this.formGroup.controls['email'].invalid;
  }

  public passwordChange(): void {
    this.isPasswordInvalid = this.formGroup.controls['password'].invalid;
  }

  public onSignUp(): void {
    if (this.formGroup.invalid) {
      this.isEmailInvalid = this.formGroup.controls['email'].invalid;
      this.isPasswordInvalid = this.formGroup.controls['password'].invalid;
      return;
    }

    this.isLoading = true;

    const signUpUserModel: SignUpUserModel = {
      firstName: this.formGroup.controls['firstName'].value,
      lastName: this.formGroup.controls['lastName'].value,
      email: this.formGroup.controls['email'].value,
      password: this.formGroup.controls['password'].value,
    };

    this.authService.signUp(signUpUserModel).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (errorMessage: string) => {
        if (!this.isOpened) {
          this.messageService.add({
            severity: 'error',
            detail: errorMessage,
          });
          this.isOpened = true;
        }

        this.isLoading = false;
        console.log('Completed');
      },
    });
  }

  public changeVisibility(): void {
    this.isOpened = false;
  }

  ngOnDestroy(): void {
    this.subscribtions?.unsubscribe();
  }
}
