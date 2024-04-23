import {Component, Input} from '@angular/core';
import {ChatRoom} from "../../models/interface/chatRoom";
import {WebSocketClientService} from "../../services/service.websocketclient";

@Component({
  selector: 'app-chat-room',
  standalone: true,
  imports: [],
  templateUrl: './chat-room.component.html',
  styleUrl: './chat-room.component.css'
})
export class ChatRoomComponent {
  @Input() room: ChatRoom | undefined;

  constructor(private client: WebSocketClientService) {

  }
}
