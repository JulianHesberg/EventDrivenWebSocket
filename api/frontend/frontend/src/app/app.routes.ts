import { Routes } from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {RoomHubComponent} from "./components/room-hub/room-hub.component";
import {ChatRoomComponent} from "./components/chat-room/chat-room.component";

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'Home'
  },
  {
    path: 'room-hub',
    component: RoomHubComponent,
    title: 'Select a Chat Room'
  },
  {
    path: 'chat-room:roomId',
    component: ChatRoomComponent,
    title: ':name'
  }
];
