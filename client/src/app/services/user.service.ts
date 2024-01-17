import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, take } from 'rxjs';
import { User } from '../model/user.model';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private accountService: AccountService) { }

  getAllUsers(): Observable<User[]> {
    let requestOptions;

    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: currentUser => {
        if (currentUser)
          requestOptions = {
            headers: new HttpHeaders({ 'Authorizaition': `bearer ${currentUser.token}` })
          }
      }
    })

    return this.http.get<User[]>('http://localhost:5000/api/user', requestOptions).pipe(
      map(users => {
        console.log(users)
        return users;
      })
    )
  }
}
