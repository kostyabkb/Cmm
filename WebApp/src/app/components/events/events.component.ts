import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { EventsService } from '../../services/events.service';
import { Event } from '../../event';
import { Subject, from } from 'rxjs';
import { takeUntil, first } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { EventLevels } from 'src/app/event-levels.enum';


@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit, OnDestroy {
  myForm = new FormControl('');
  protected destroy$ = new Subject<void>();
  events: Event[] = [];
  keys = [];

  constructor( private deviceEventsService: EventsService ) {
this.keys = Object.keys(EventLevels)
.filter(k => typeof EventLevels[k] === 'number') as string[];
   }

  ngOnInit() {
    this.getEventsDescriptions();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  getEventsDescriptions(): void {
    this.deviceEventsService.getDescriptions()
    .pipe(takeUntil(this.destroy$))
    .subscribe(events => this.events = events);
  }

  saveEventDescription(name: string, description: string, level: string): void {
    this.deviceEventsService.saveDescription(name, description, EventLevels[level]);
  }

  checkChanges(newDescription: string, newLevel: number, event: Event): boolean {
    if ((newDescription.length === 0 || newDescription.length > 1000 || newDescription === event.description) && newLevel === this.keys[event.level]) {
      return true;
    }
    return false;
  }
}

