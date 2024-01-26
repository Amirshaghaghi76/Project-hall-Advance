import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
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
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [MatFormFieldModule, CommonModule, MatCardModule,
    MatIconModule, FormsModule, MatButtonModule,
    ReactiveFormsModule, FooterComponent]
})
export class HomeComponent {
  counselingRes: Counseling | undefined;

  private fb = inject(FormBuilder);
  private http = inject(HttpClient);
  private userService = inject(UserService);
  private counselingSeervice = inject(CounselingService)
  alllUsers: User[] | null | undefined;
  allUsers$: Observable<User[] | null> | undefined;

  // constructor(private fb: FormBuilder, private http: HttpClient,
  //   private userSerevice: UserService, private counselingService: CounselingService) { } 
  // old version use from constructer to add service and form 

  counselingFg = this.fb.group({
    phoneNumberCtrl: ['', [Validators.minLength(11), Validators.maxLength(11), Validators.required]]
  });

  registerCounseling(): void {
    console.log(this.counselingFg.value);

    let counseling: Counseling = {
      phoneNumber: this.PhoneNumberCtrl.value
    }

    this.counselingSeervice.counseling(counseling).subscribe({
      next: counseling => {
        console.log(counseling);
      }
    })

  }

  get PhoneNumberCtrl(): FormControl {
    return this.counselingFg.get('phoneNumberCtrl') as FormControl;
  }

  showAllUsers() {
    this.userService.getAllUsers().subscribe({
      next: ussers => this.alllUsers = ussers,
      error: err => console.log(err)
    });
  }
}





