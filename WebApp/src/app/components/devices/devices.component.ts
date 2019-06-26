import { Component, OnInit, OnDestroy } from '@angular/core';
import { Device } from '../../device';
import { DevicesService } from '../../services/devices.service';
import { Subject } from 'rxjs';
import { takeUntil, filter } from 'rxjs/operators';
import { WsClientService } from 'src/app/services/ws-client.service';
import { Notifies } from 'src/app/notifies.enum';

@Component({
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.css']
})
export class DevicesComponent implements OnInit, OnDestroy {
  protected destroy$ = new Subject<void>();
  devices: Device[];
  selectedDevice: Device;

  constructor( private deviceService: DevicesService,
               private wsService: WsClientService ) {}

  getDevices(): void {
    this.deviceService.getDevices()
    .pipe(takeUntil(this.destroy$))
    .subscribe(devices => this.devices = devices);
  }

  ngOnInit() {
    this.getDevices();

    this.wsService.notifications
    .pipe(filter(x => x === Notifies.UpdateDevice), takeUntil(this.destroy$))
    .subscribe(notifies => this.getDevices());
  }

  notifyHandler(notify: string) {
    console.log('hello from devicesComponent');
    if (notify === 'UpdateDevice') {
      this.getDevices();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSelect(device: Device): void {
    this.selectedDevice = device;
  }
}
