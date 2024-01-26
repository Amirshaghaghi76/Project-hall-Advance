import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { User } from './model/user.model';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { AccountService } from './services/account.service';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [NavbarComponent, RouterModule],
})

export class AppComponent implements OnInit {

  accountService = inject(AccountService);
  platformId = inject(PLATFORM_ID);

  ngOnInit(): void {
    this.getLocalStorageValues();
  }

  getLocalStorageValues(): void {
    let userString: string | null = null;

    if (isPlatformBrowser(this.platformId))
      userString = localStorage.getItem('user');

    if (userString) {
      const user: User = JSON.parse(userString); //convert to json before string

      this.accountService.setCurrentUser(user);
    }
  }
}
