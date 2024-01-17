import { Component, OnInit } from '@angular/core';
import { User } from '../../model/user.model';
import { AccountService } from '../../services/account.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatCommonModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  imports: [MatToolbarModule, MatMenuModule, CommonModule, MatButtonModule,
    MatDividerModule, MatListModule, MatIconModule, RouterLink],
})
export class NavbarComponent {
  user: User | null | undefined;
  constructor(private accountService: AccountService) {

    this.accountService.currentUser$.subscribe({
      next: response => this.user = response
    })
  }


  Logout(): void {
    this.accountService.LogoutUser();
  }
}
