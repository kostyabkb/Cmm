import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeviceDetailsComponent } from './components/device-details/device-details.component';
import { IndexComponent } from './components/index/index.component';
import { EventsComponent } from './components/events/events.component';

const routes: Routes = [
  { path: 'devices', component: IndexComponent },
  { path: 'devices/:id', component: DeviceDetailsComponent },
  { path: 'events', component: EventsComponent },
  { path: '', redirectTo: '/devices', pathMatch: 'full' }
]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
