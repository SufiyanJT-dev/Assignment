// orders.component.ts
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { Apicommuncation } from '../../shared/Api/apicommuncation';
import { BookingDisplayDataDto, BookingStatus } from './type/BookingDisplayDataDto';
import { Navbar } from '../../core/Layout/navbar/navbar';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule,Navbar, MatCardModule, MatIconModule, MatButtonModule, MatDividerModule],
  templateUrl: './orders.html',
  styleUrls: ['./orders.scss']
})
export class Orders implements OnInit {
  token = '';
  storedUserID = '';
  userId = 0;
  bookingList: BookingDisplayDataDto[] = [];

  constructor(private router: Router, private api: Apicommuncation) {}

  ngOnInit() {
    this.token = localStorage.getItem('JwtAssesToken') || '';
    this.storedUserID = localStorage.getItem('userId') || '';

    if (!this.token) {
      this.router.navigate(['/login']);
      return;
    }
    if (this.storedUserID) this.userId = Number(this.storedUserID);

    this.api.GetBookingDataByUserID(this.userId).subscribe({
      next: (value:any) => {
        this.bookingList = Array.isArray(value) ? value : [value];
      },
      error: (err) => console.error('Failed to load bookings', err)
    });
  }

  getStatusLabel(status: BookingStatus | number): string {
    switch (status) {
      case BookingStatus.Pending: return 'Pending';
      case BookingStatus.Confirmed: return 'Confirmed';
      case BookingStatus.Cancelled: return 'Cancelled';
      case BookingStatus.Completed: return 'Completed';
      default: return 'Unknown';
    }
  }

  getStatusClass(status: BookingStatus | number): string {
    switch (status) {
      case BookingStatus.Pending: return 'status-pending';
      case BookingStatus.Confirmed: return 'status-confirmed';
      case BookingStatus.Cancelled: return 'status-cancelled';
      case BookingStatus.Completed: return 'status-completed';
      default: return '';
    }
  }

  onViewDetails(booking: BookingDisplayDataDto) {
    this.router.navigate(['/order-details', booking.bookingId]);
  }

  onCancelBooking(booking: BookingDisplayDataDto) {
    alert(`Cancel booking #${booking.bookingId} (implement API call)`);
  }
}
