import { Component, OnInit } from '@angular/core';
import { WsClientService } from 'src/app/services/ws-client.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  constructor(private wsClient: WsClientService) { }

  ngOnInit( ) {}
}
