export enum BugStatus {
  Open = 0,
  Closed = 1,
  WorkInProgress = 2,
  Hold = 3,
  Rejected = 4
}

export interface Bug {
  id?: number;
  title: string;
  description: string;
  status: BugStatus;
  createdAt?: string;
  updatedAt?: string;
}
