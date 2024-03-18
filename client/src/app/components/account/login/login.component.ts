import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoginUser } from '../../../model/login.user.model';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { AccountService } from '../../../services/account.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [MatFormFieldModule, CommonModule, FormsModule,
    ReactiveFormsModule, MatInputModule, MatButtonModule]
})
export class LoginComponent implements OnDestroy {
  apiErrorMassage: string | undefined;
  subscribed: Subscription | undefined;

  constructor(private accountService: AccountService, private fb: FormBuilder, http: HttpClient, private router: Router) {
  }
  ngOnDestroy(): void {
    this.subscribed?.unsubscribe;
  }

  loginFg = this.fb.group({
    emailCtrl: ['', [Validators.required, Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]],
    passwordCtrl: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]]
  })


  get EmailCtrl(): FormControl {
    return this.loginFg.get('emailCtrl') as FormControl;
  }

  get PasswordCtrl(): FormControl {
    return this.loginFg.get('passwordCtrl') as FormControl;
  }

  login(): void {
    this.apiErrorMassage = undefined;

    let user: LoginUser = {
      email: this.EmailCtrl.value,
      password: this.PasswordCtrl.value,
    }

    this.subscribed = this.accountService.loginUser(user).subscribe({
      next: user => {
        console.log(user),
          this.router.navigateByUrl('/');
      },
      error: err => this.apiErrorMassage = err.error
    })
  }

  gerState(): void {
    console.log(this.loginFg);
  }
}

