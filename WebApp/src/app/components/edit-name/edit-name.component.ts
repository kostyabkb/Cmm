import { Component, OnInit, Inject, OnDestroy} from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { MatDialogRef } from '@angular/material';
import { Device } from 'src/app/device';
import { DevicesService } from 'src/app/services/devices.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-edit-name',
  templateUrl: './edit-name.component.html',
  styleUrls: ['./edit-name.component.css']
})
export class EditNameComponent implements OnInit, OnDestroy {
  protected destroy$ = new Subject<void>();
  device: Device;

  constructor(
    private service: DevicesService,
    private matDialogRef: MatDialogRef<EditNameComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
     }

  ngOnInit() {
  }

  updateName(newName: string): void {
    this.service.updateName(this.data.id, newName)
    .pipe(takeUntil(this.destroy$))
    .subscribe(() => this.close());
  }

  close(): void {
    this.matDialogRef.close();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
