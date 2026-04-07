import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Bug, BugStatus } from '../../models/bug.model';
import { BugService } from '../../services/bug.service';

@Component({
  selector: 'app-bug-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './bug-modal.component.html',
  styleUrls: ['./bug-modal.component.css']
})
export class BugModalComponent {
  @Input() bug: Bug = { title: '', description: '', status: BugStatus.Open };
  @Output() onClose = new EventEmitter<boolean>();

  statuses = [
    { value: BugStatus.Open, label: 'Open' },
    { value: BugStatus.Closed, label: 'Closed' },
    { value: BugStatus.WorkInProgress, label: 'Work In Progress' },
    { value: BugStatus.Hold, label: 'Hold' },
    { value: BugStatus.Rejected, label: 'Rejected' }
  ];

  constructor(private bugService: BugService) {}

  saveBug(): void {
    // Construct internal payload to match backend model EXACTLY
    const bugPayload = {
      id: this.bug.id,
      title: this.bug.title,
      description: this.bug.description,
      status: Number(this.bug.status)
    };
    
    // Mandatory debug log
    console.log("UPDATE PAYLOAD:", bugPayload);

    if (this.bug.id) {
      this.bugService.updateBug(this.bug.id, bugPayload as Bug).subscribe({
        next: () => this.onClose.emit(true),
        error: (err) => console.error('Update Failed:', err)
      });
    } else {
      this.bugService.createBug(bugPayload as Bug).subscribe({
        next: () => this.onClose.emit(true),
        error: (err) => console.error('Create Failed:', err)
      });
    }
  }

  close(): void {
    this.onClose.emit(false);
  }
}
