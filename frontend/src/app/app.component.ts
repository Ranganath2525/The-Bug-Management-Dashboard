import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BugListComponent } from './components/bug-list/bug-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, BugListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'bug-dashboard';
}
