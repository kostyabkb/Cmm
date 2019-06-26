import { Injectable } from '@angular/core';
import { Device } from '../device';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DevicesService {
  private url = 'api/devices/';

  getDevice(id: string): Observable<Device> {
    return this.http.get<Device>(this.url + id);
  }

  getDevices(): Observable<Device[]> {
    return this.http.get<Device[]>(this.url);
  }

  updateName(id: string, name: string): Observable<void> {
    return this.http.put<void>(this.url, {id, name});
  }

  constructor(private http: HttpClient) { }
}
