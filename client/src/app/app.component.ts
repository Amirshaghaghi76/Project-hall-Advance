import { Component, OnInit } from '@angular/core';
import { Hall } from './model/hall.model';
import { UserService } from './services/user.service';
import { AccountService } from './services/account.service';
import { RegisterUser } from './model/register-user.model';
import { User } from './model/user.model';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RouterModule } from '@angular/router';

@Component({
  standalone:true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports:[NavbarComponent,RouterModule],
})
export class AppComponent implements OnInit {

  constructor(private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.getLocalStorageValues();
  }
  
  getLocalStorageValues(): void {
    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user: User = JSON.parse(userString); //convert to json before string

      this.accountService.setCurrentUser(user);
    }
  }
}
