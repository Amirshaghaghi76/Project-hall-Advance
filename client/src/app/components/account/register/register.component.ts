import { CommonModule, KeyValuePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterUser } from '../../../model/register-user.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { AccountService } from '../../../services/account.service';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
@Component({
  standalone: true,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  imports: [MatFormFieldModule, MatIconModule, CommonModule,
    ReactiveFormsModule, FormsModule, MatInputModule,MatButtonModule]
})
export class RegisterComponent {
  passwordNotMatch: boolean | undefined;
  apiErrorMessage: string | undefined;

  constructor(private accountService: AccountService, private fb: FormBuilder, private router: Router) { }
  // #region FormGroup
  registerFg = this.fb.group({
    nameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
    passwordCtrl: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
    confrimPasswordCtrl: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
    emailCtrl: ['', [Validators.required, Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]],
    ageCtrl: ['', [Validators.required, Validators.min(18), Validators.max(99)]]
  })

  get NameCtrl(): FormControl {
    return this.registerFg.get('nameCtrl') as FormControl
  }
  get PasswordCtrl(): FormControl {
    return this.registerFg.get('passwordCtrl') as FormControl;
  }
  get ConfrimPasswordCtrl(): FormControl {
    return this.registerFg.get('confrimPasswordCtrl') as FormControl;
  }
  get EmailCtrl(): FormControl {
    return this.registerFg.get('emailCtrl') as FormControl;
  }
  get AgeCtrl(): FormControl {
    return this.registerFg.get('ageCtrl') as FormControl;
  }
  // #endregion FormGroup

  register(): void {

    this.apiErrorMessage = undefined;

    if (this.PasswordCtrl.value === this.ConfrimPasswordCtrl.value) {
      this.passwordNotMatch = false;

      let user: RegisterUser = {
        name: this.NameCtrl.value,
        password: this.PasswordCtrl.value,
        confrimPassword: this.ConfrimPasswordCtrl.value,
        email: this.EmailCtrl.value,
        age: this.AgeCtrl.value
      }

      this.accountService.registerUser(user).subscribe({
        next: user => {
          console.log(user);
          this.router.navigateByUrl('/');
        },
        error: err => this.apiErrorMessage = err.error
      })
    }
    else {
      this.passwordNotMatch = true;
    }
  }

  ClearForm(): void {
    this.registerFg.reset();
  }
}
