import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Counseling } from '../../model/counseling.model ';
import { User } from '../../model/user.model';
import { CounselingService } from '../../services/counseling.service';
import { UserService } from '../../services/user.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { FooterComponent } from "../footer/footer.component";
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [MatFormFieldModule, MatCardModule, MatIconModule, FormsModule, MatButtonModule,
    ReactiveFormsModule, FooterComponent]
})
export class HomeComponent {
  counselingRes: Counseling | undefined;

  allUsers: User[] | undefined

  constructor(private fb: FormBuilder, private http: HttpClient, private userSerevice: UserService, private counselingService: CounselingService) { }

  counselingFg = this.fb.group({
    phoneNumberCtrl: ['', [Validators.minLength(11), Validators.maxLength(11), Validators.required]]
  });

  registerCounseling(): void {
    console.log(this.counselingFg.value);

    let counseling: Counseling = {
      phoneNumber: this.PhoneNumberCtrl.value
    }

    this.counselingService.counseling(counseling).subscribe({
      next: counseling => {
        console.log(counseling);
      }
    })

  }

  get PhoneNumberCtrl(): FormControl {
    return this.counselingFg.get('phoneNumberCtrl') as FormControl;
  }

  showAllUsers() {
    this.userSerevice.getAllUsers().subscribe({
      next: users => this.allUsers = users,
      error: err => console.log(err)
    });
  }
}





