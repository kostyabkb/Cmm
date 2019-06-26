import { Injectable } from '@angular/core';
import { Event } from '../event';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DevicesEventsService {
  private url = 'api/DevicesEvents/';

  constructor(private http: HttpClient) { }

  getEvents(id: string): Observable<Event[]> {
    return this.http.get<Event[]>(this.url + id);
  }

  deleteEvents(id: string): Observable<void> {
    return this.http.delete<void>(this.url + id);
  }
}
