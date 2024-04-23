import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {WebSocketClientService} from "../../services/service.websocketclient";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent {
  username = new FormControl('', Validators.required);
  formGroup = new FormGroup({
    username: this.username
  });

  constructor(private router: Router, private clientService: WebSocketClientService) {
  }

  clickStartChatting(username: string) {
    this.clientService.clientWantsToSignIn(username);
    this.router.navigate(['/room-hub'])
  }

}
