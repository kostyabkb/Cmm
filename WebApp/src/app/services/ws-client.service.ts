import { Injectable, OnInit, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Observable, Subject, observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WsClientService implements OnInit, OnDestroy {
  private url = '/chathub';
  private connection: HubConnection;
  public notifications: Subject<string> = new Subject();

  constructor() {
    this.startConnect();
    this.connection.onclose(() => this.startConnect());
   }

  ngOnInit() {
  }

  startConnect() {
    this.connection = new HubConnectionBuilder().withUrl(this.url).build();
    this.connection.start()
    .then(() => {
      console.log('Connected to ', this.url);
      this.connection.on('Notify', notification => {
        this.notifications.next(notification);
        console.log(notification);
      });
    });
  }

  ngOnDestroy() {
    this.connection.stop();
  }
}
