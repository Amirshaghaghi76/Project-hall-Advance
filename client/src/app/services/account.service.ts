import { HttpClient } from '@angular/common/http';
import { Injectable, PLATFORM_ID, inject } from '@angular/core';
import { RegisterUser } from '../model/register-user.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../model/user.model';
import { LoginUser } from '../model/login.user.model';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.development';
import { Token } from '@angular/compiler';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root' 
})
export class AccountService {
  private currentUserSourse = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSourse.asObservable();

  http = inject(HttpClient);
  router = inject(Router);
  platformId = inject(PLATFORM_ID);

  // private readonly baseApiUrl: string = 'http://localhost:5000/api/account/';
  private readonly baseApiUrl = environment.apiUrl + 'account/';

  // constructor(private http: HttpClient) { }

  registerUser(userInput: RegisterUser): Observable<User | null> {
    return this.http.post<User>(this.baseApiUrl + 'register', userInput).pipe(
      map(userResponse => {
        if (userResponse) {
          // this.currentUserSourse.next(userResponse);
          this.setCurrentUser(userResponse);
          // localStorage.setItem('user', JSON.stringify(userResponse));
          return userResponse;
        }

        return null
      })
    );
  }

  loginUser(userInput: LoginUser): Observable<User | null> {
    return this.http.post<User>(this.baseApiUrl + 'login', userInput).pipe(
      map(userResponse => {
        if (userResponse) {
          this.setCurrentUser(userResponse);
          // localStorage.setItem('user', JSON.stringify(userResponse));
          return userResponse;
        }

        return null;
      })
    );
  }
  // TODO add method getUser To get user from api
  

  setCurrentUser(user: User): void {
    this.currentUserSourse.next(user);

    // localStorage.setItem('user', JSON.stringify(user)); // before date expire token (video 21)
    localStorage.setItem('token', user.token);

    this.router.navigateByUrl('');
  }

  LogoutUser(): void {
    this.currentUserSourse.next(null);
    if (isPlatformBrowser(this.platformId))
      localStorage.clear(); // remove token in localStorage
    // localStorage.removeItem('user'); // before date expire token (video 21)
    localStorage.removeItem('token');
    this.router.navigateByUrl('account/login');
  }
}
