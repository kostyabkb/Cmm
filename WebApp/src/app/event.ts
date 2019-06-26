import { EventLevels } from './event-levels.enum';

export interface Event {
    name: string;
    date: Date;
    description: string;
    level: EventLevels;
  }
