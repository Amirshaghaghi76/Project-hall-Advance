import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, map, take } from 'rxjs';
import { User } from '../model/user.model';
import { environment } from '../../environments/environment.development';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  http = inject(HttpClient);

  private readonly baseApiUrl = environment.apiUrl + 'user/';

  getUser(): Observable<User | null> {
    return this.http.get<User>(environment.apiUrl + 'user/get-by-id').pipe(
      map((userResponce: User | null) => {

        if (userResponce)
          return userResponce

        return null;
      })
    );
  }
}
 
