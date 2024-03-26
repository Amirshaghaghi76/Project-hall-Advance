import { Injectable, inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import { User } from '../model/user.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  private http = inject(HttpClient);

  private readonly baseApiUrl = environment.apiUrl + 'user/';
  
  getAllMembers(): Observable<User[] | null> {
    
  //#region Create requestOption like headers for each every http-request
  // accountService=inject( AccountService);
    //   let requestOptions;

    //   this.accountService.currentUser$.pipe(take(1)).subscribe({
    //     next: currentUser => {
    //       if (currentUser)
    //         requestOptions = {
    //           headers: new HttpHeaders({ 'Authorizaition': `bearer ${currentUser.token}` })
    //         }
    //     }
    //   })

    //   return this.http.get<User[]>('http://localhost:5000/api/user', requestOptions).pipe(
    //     map(users => {
    //       console.log(users)
    //       return users;
    //     })
    //   )
    // }
    //#endregion

    return this.http.get<User[]>(this.baseApiUrl).pipe(
      map((user: User[]) => {
        if (user)
          return user;

        return null;
      })
    )
  }

  getMemberById(): Observable<User | null> {
    return this.http.get<User>(this.baseApiUrl + '655ccfedc43beece8dcb9697').pipe(
      map((user: User | null) => {
        if (user)
          return user;
        return null;
      })
    )
  }
}


