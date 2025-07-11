import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true, // STANDALONE TRUE
  imports: [CommonModule, RouterOutlet],
  template: '<router-outlet></router-outlet>', // Sadece router-outlet
  styleUrls: ['./app.css']
})
export class AppComponent {
  title = 'BillingSystem.Frontend';
}