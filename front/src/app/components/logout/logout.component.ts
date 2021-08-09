import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { EventEmitterService } from 'src/app/services/event-emitter.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss']
})
export class LogoutComponent {

  constructor(
    private sessionService: SessionService,
    private router: Router,
    private eventEmitterService: EventEmitterService
  ) {
    this.logout();
  }

  logout(): void {
    this.sessionService.deleteLogin();
    this.eventEmitterService.refresh();
    this.router.navigate(['/login']);
  }
}
