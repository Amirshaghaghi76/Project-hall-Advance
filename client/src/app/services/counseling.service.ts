import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Counseling } from '../model/counseling.model ';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CounselingService {

  constructor(private http: HttpClient) { }

  counseling(counseling: Counseling):Observable<Counseling|null>{
    return this.http.post<Counseling>('http://localhost:5000/api/advice/register', counseling)
  }
}
