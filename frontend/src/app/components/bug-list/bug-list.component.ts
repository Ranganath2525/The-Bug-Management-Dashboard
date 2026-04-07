import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Bug, BugStatus } from '../../models/bug.model';
import { BugService } from '../../services/bug.service';
import { BugModalComponent } from '../bug-modal/bug-modal.component';

@Component({
  selector: 'app-bug-list',
  standalone: true,
  imports: [CommonModule, FormsModule, BugModalComponent],
  templateUrl: './bug-list.component.html',
  styleUrls: ['./bug-list.component.css']
})
export class BugListComponent implements OnInit {
  bugs: Bug[] = [];
  filteredBugs: Bug[] = [];
  searchTerm: string = '';
  statusFilter: string | number = '';
  showModal: boolean = false;
  selectedBug?: Bug;

  constructor(private bugService: BugService) {}

  ngOnInit(): void {
    this.loadBugs();
  }

  loadBugs(): void {
    this.bugService.getBugs().subscribe({
      next: (data) => {
        this.bugs = data;
        this.applyFilters();
      },
      error: (err) => console.error('Error loading bugs:', err)
    });
  }

  applyFilters(): void {
    const search = this.searchTerm.toLowerCase();
    this.filteredBugs = this.bugs.filter(bug => {
      const matchesSearch = (bug.title?.toLowerCase().includes(search) || false) ||
                            (bug.description?.toLowerCase().includes(search) || false);
      const matchesStatus = this.statusFilter === '' || bug.status === Number(this.statusFilter);
      return matchesSearch && matchesStatus;
    });
  }

  getStatusClass(status: BugStatus): string {
    switch (status) {
      case BugStatus.Open: return 'badge-red';
      case BugStatus.Closed: return 'badge-green';
      case BugStatus.WorkInProgress: return 'badge-orange';
      case BugStatus.Hold: return 'badge-gray';
      case BugStatus.Rejected: return 'badge-darkred';
      default: return '';
    }
  }

  getStatusLabel(status: BugStatus): string {
    switch (status) {
      case BugStatus.Open: return 'Open';
      case BugStatus.Closed: return 'Closed';
      case BugStatus.WorkInProgress: return 'Work In Progress';
      case BugStatus.Hold: return 'Hold';
      case BugStatus.Rejected: return 'Rejected';
      default: return 'Unknown';
    }
  }

  openAddModal(): void {
    this.selectedBug = undefined;
    this.showModal = true;
  }

  openEditModal(bug: Bug): void {
    this.selectedBug = { ...bug };
    this.showModal = true;
  }

  deleteBug(id: number): void {
    if (confirm('Are you sure you want to delete this bug report?')) {
      this.bugService.deleteBug(id).subscribe(() => this.loadBugs());
    }
  }

  handleModalClose(success: boolean): void {
    this.showModal = false;
    if (success) this.loadBugs();
  }
}
