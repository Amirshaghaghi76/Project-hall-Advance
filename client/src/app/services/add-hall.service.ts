import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Hall } from '../model/hall.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddHallService {

  constructor(private http: HttpClient) { }

  addHall(userInput: Hall): Observable<Hall | null> {
    return this.http.post<Hall>('http://localhost:5000/api/hallordinary/register',userInput)
   }
}
