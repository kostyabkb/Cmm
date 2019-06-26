import { Injectable, OnDestroy } from '@angular/core';
import { Event } from '../event';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventsService implements OnDestroy {
  private url = 'api/events/';
  protected destroy$ = new Subject<void>();

  constructor(private http: HttpClient) { }

  getDescriptions(): Observable<Event[]> {
    return this.http.get<Event[]>(this.url);
  }

  saveDescription(name: string, description: string, level: number): void {
    this.http.put(this.url, {description, name, level})
    .pipe(takeUntil(this.destroy$))
    .subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
