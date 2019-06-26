import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DevicesEventsService } from '../../services/devicesevents.service';
import { DevicesService } from '../../services/devices.service';
import { Event } from '../../event';
import { Device } from '../../device';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MatDialog } from '@angular/material';
import { EditNameComponent } from '../edit-name/edit-name.component';

@Component({
  selector: 'app-device-details',
  templateUrl: './device-details.component.html',
  styleUrls: ['./device-details.component.css']
})

export class DeviceDetailsComponent implements OnInit, OnDestroy {
  protected destroy$ = new Subject<void>();
  events: Event[] = [];
  id: string;
  device: Device = {name: "", os: "", version: "", id: ""};

  constructor(
    private eventService: DevicesEventsService,
    private devicesService: DevicesService,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.route.params
    .pipe(takeUntil(this.destroy$))
    .subscribe(params => {
      this.id = params['id'];
      this.getEvents();
      this.getDevice();
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  getEvents(): void {
    this.eventService.getEvents(this.id)
    .pipe(takeUntil(this.destroy$))
    .subscribe(events => this.events = events);
  }

  getDevice(): void {
    this.devicesService.getDevice(this.id)
    .pipe(takeUntil(this.destroy$))
    .subscribe(device => this.device = device);
  }

  deleteEvents(): void {
    this.eventService.deleteEvents(this.id)
    .pipe(takeUntil(this.destroy$))
    .subscribe({ complete: () => this.getEvents() });
  }

  openDialog(device: Device): void {
    let dialogRef = this.dialog.open(EditNameComponent, {
      data: { name: device.name, id: device.id }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getDevice();
    });
  }
}
