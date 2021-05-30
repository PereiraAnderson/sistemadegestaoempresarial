import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EventEmitterService } from 'src/app/services/event-emitter.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  constructor(
    private sessionService: SessionService,
    private router: Router,
    private eventEmitterService: EventEmitterService
  ) { }

  ngOnInit(): void {
  }

  logout(): void {
    this.sessionService.deleteLogin();
    this.eventEmitterService.refresh();
    this.router.navigate(['/login']);
  }

}
