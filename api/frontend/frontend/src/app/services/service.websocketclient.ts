import {Injectable} from "@angular/core";
import {ClientWantsToSignIn} from "../models/dto/clientWantsToSignIn";
import {ClientWantsToEnterRoom} from "../models/dto/clientWantsToEnterRoom";
import ReconnectingWebSocket from "reconnecting-websocket";

@Injectable({providedIn: 'root'})
export class WebSocketClientService{
  public webSocket: ReconnectingWebSocket

  constructor() {
    this.webSocket = new ReconnectingWebSocket('ws://localhost:8181');
  }

  clientWantsToSignIn(username: string){
    var message = new ClientWantsToSignIn({
      Username: username
    })
    console.log(message);
    try {
      this.webSocket.send(JSON.stringify(message));
    } catch (e) {
      setTimeout(() => this.clientWantsToSignIn(username), 2000)
    }

  }

  clientWantsToEnterRoom(roomId: number){
    var message = new ClientWantsToEnterRoom({
      RoomId: roomId
    })
    console.log(message);
    this.webSocket.send(JSON.stringify(message));
  }
}
