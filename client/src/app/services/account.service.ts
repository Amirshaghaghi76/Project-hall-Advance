import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from '../model/register-user.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../model/user.model';
import { LoginUser } from '../model/login.user.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSourse = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSourse.asObservable();

  constructor(private http: HttpClient) { }

  registerUser(userInput: RegisterUser): Observable<User | null> {
    return this.http.post<User>('http://localhost:5000/api/account/register', userInput).pipe(
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
    return this.http.post<User>('http://localhost:5000/api/account/login', userInput).pipe(
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

  setCurrentUser(user: User): void {
    this.currentUserSourse.next(user);
    localStorage.setItem('user', JSON.stringify(user));
  }

  LogoutUser(): void {
    this.currentUserSourse.next(null);

    localStorage.clear();
  }
}
