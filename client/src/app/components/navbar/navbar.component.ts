import { Component, OnInit, inject } from '@angular/core';
import { User } from '../../model/user.model';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatCommonModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AccountService } from '../../services/account.service';
import { Observable } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  imports: [MatToolbarModule, MatMenuModule,
    MatButtonModule, CommonModule, RouterModule,
    MatDividerModule, MatListModule, MatIconModule],

})
export class NavbarComponent {
  user: User | null | undefined;
  constructor(private accountService: AccountService) {

    this.accountService.currentUser$.subscribe({
      next: response => this.user = response
    })
  }
  // accountService = inject(AccountService);

  // user$: Observable<User | null> | undefined;

  // ngOnInit(): void {
  //   this.user$ = this.accountService.currentUser$;
  // }

  Logout(): void {
    this.accountService.LogoutUser();
  }
}
