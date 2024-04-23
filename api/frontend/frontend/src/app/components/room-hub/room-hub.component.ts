import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {WebSocketClientService} from "../../services/service.websocketclient";
import {RoomSelectComponent} from "../room-select/room-select.component";
import {RoomSelect} from "../../models/interface/roomSelect";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-room-hub',
  standalone: true,
  imports: [
    RoomSelectComponent,
    NgForOf
  ],
  templateUrl: './room-hub.component.html',
  styleUrl: './room-hub.component.css'
})
export class RoomHubComponent {

  chatRooms: RoomSelect[] = [];
  constructor(private router: Router, private clientService: WebSocketClientService) {
    let room1: RoomSelect = {
      id: 1,
      name: 'Pickle'
    }
    let room2: RoomSelect = {
      id: 2,
      name: 'Pineapple'
    }
    let room3: RoomSelect = {
      id: 3,
      name: 'Pistachio'
    }
    this.chatRooms.push(room1, room2, room3);
  }

}
