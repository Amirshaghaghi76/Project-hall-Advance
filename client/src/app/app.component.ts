import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { User } from './model/user.model';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { AccountService } from './services/account.service';
import { take } from 'rxjs';

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
    // let userString: string | null = null;  // before dte expire token (before video 21)

    if (isPlatformBrowser(this.platformId)) { // ValidateLifetime = truethis code is ran on the browser now
      console.log('Platform in method:', this.platformId);

      // userString = localStorage.getItem('user'); // before dte expire token (before video 21)

      this.accountService.getUser().pipe(take(1)).subscribe(user => {
        if (user)
          this.accountService.setCurrentUser(user);
        else {
          console.log('You are being logged out. Pelese to login again!')
          this.accountService.LogoutUser();
        }
      });
    }
  
//     if (userString) {  // before dte expire token (before video 21)
//       const user: User = JSON.parse(userString); //convert to json before string

//       this.accountService.setCurrentUser(user);
//     }
//   }
  }
}
