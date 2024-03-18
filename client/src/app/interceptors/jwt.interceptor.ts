// import { inject } from '@angular/core'; // // before dte expire token (before video 21)
// import { take } from 'rxjs';
// import { User } from '../model/user.model';
// import { AccountService } from '../services/account.service'; 
// import { json } from 'node:stream/consumers';

import { HttpInterceptorFn } from '@angular/common/http';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token');

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `beare ${token}`
      }
    })
  }
  
  return next(req);
};

// before dte expire token (before video 21)
// export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
//   inject(AccountService).currentUser$.pipe(take(1)).subscribe({
//   next: (currentUser: User | null) => {  // // before dte expire token (before video 21)
//     if(currentUser){
//       req=req.clone({
//         setHeaders:{
//           Authorization:`Beare ${currentUser?.token}`
//         }
//       });
//     }
//   }
// });

// return next(req);
// }