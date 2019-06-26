import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule, MatButtonModule } from '@angular/material';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { DeviceDetailsComponent } from './components/device-details/device-details.component';
import { DevicesComponent } from './components/devices/devices.component';
import { IndexComponent } from './components/index/index.component';
import { EventsComponent } from './components/events/events.component';
import { EditNameComponent } from './components/edit-name/edit-name.component';
import { WsClientService } from './services/ws-client.service';


@NgModule({
  declarations: [
    AppComponent,
    DeviceDetailsComponent,
    DevicesComponent,
    IndexComponent,
    EventsComponent,
    EditNameComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatDialogModule,
    MatButtonModule
  ],
  entryComponents: [EditNameComponent],
  providers: [WsClientService],
  bootstrap: [AppComponent]
})
export class AppModule { }
