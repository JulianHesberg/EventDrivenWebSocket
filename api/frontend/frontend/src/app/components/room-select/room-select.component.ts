import {Component, Input} from '@angular/core';
import {RoomSelect} from "../../models/interface/roomSelect";
import {WebSocketClientService} from "../../services/service.websocketclient";

@Component({
  selector: 'room-select',
  standalone: true,
  imports: [],
  templateUrl: './room-select.component.html',
  styleUrl: './room-select.component.css'
})
export class RoomSelectComponent {

  @Input() room: RoomSelect | undefined;

  constructor(private client: WebSocketClientService) {
  }
  joinRoom() {
    this.client.clientWantsToEnterRoom(<number>this.room?.id)
  }
}
