import { AsyncPipe, JsonPipe } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { shareReplay } from 'rxjs';
import { OpsInfoService } from './core/api/ops-info.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, AsyncPipe, JsonPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  readonly info$;

  constructor(private readonly ops: OpsInfoService) {
    this.info$ = this.ops.getInfo().pipe(shareReplay({ bufferSize: 1, refCount: true }));
  }
}
