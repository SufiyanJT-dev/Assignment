import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Apicommuncation } from '../../../../../shared/Api/apicommuncation';
import { ActivatedRoute } from '@angular/router';

export interface BookingDetails {
  id?: number;
  customerId: number;
  roomId: number;
  checkInDate: string;
  checkOutDate: string;
  status: number;
  totalAmount: number;
}

@Component({
  selector: 'app-booking',
  imports: [CommonModule, FormsModule],
  templateUrl: './booking.html',
  styleUrl: './booking.scss',
})
export class BookingComponent {
  bookings: BookingDetails[] = [];
  showModal = false;
  isEditMode = false;
  currentBooking: BookingDetails = this.getEmptyBooking();
  roomId:number=0;
  statusOptions = [
    { value: 1, label: 'Pending' },
    { value: 2, label: 'Confirmed' },
    { value: 3, label: 'Checked In' },
    { value: 4, label: 'Checked Out' },
    { value: 5, label: 'Cancelled' }
  ];

  constructor(private api: Apicommuncation,private route:ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params=>{
      this.roomId=params['roomId']
    })
    this.loadBookings();
  }

  loadBookings() {
    this.api.GetBookinByRoomId(this.roomId).subscribe({
      next: (value) => {
        this.bookings = value;
        console.log('Bookings loaded:', value);
      },
      error: (err) => {
        console.error('Error loading bookings:', err);
        alert('Failed to load bookings');
      }
    });
  }

  openAddModal() {
    this.isEditMode = false;
    this.currentBooking = this.getEmptyBooking();
    this.showModal = true;
  }

  openEditModal(booking: BookingDetails) {
    this.isEditMode = true;
    this.currentBooking = { ...booking };
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.currentBooking = this.getEmptyBooking();
  }

  saveBooking() {
    if (this.isEditMode && this.currentBooking.id) {
      this.updateBooking();
    } else {
      this.addBooking();
    }
  }

  addBooking() {
    this.api.createBooking(this.currentBooking).subscribe({
      next: () => {
        this.loadBookings();
        this.closeModal();
        alert('Booking added successfully');
      },
      error: (err) => {
        console.error('Error adding booking:', err);
        console.log(this.currentBooking)
        alert('Failed to add booking');
      }
    });
  }

  updateBooking() {
    if (!this.currentBooking.id) return;
    
    this.api.updateBooking(this.currentBooking.id, this.currentBooking).subscribe({
      next: () => {
        this.loadBookings();
        this.closeModal();
        alert('Booking updated successfully');
      },
      error: (err) => {
        console.error('Error updating booking:', err);
        console.log(this.currentBooking);
        alert('Failed to update booking');
      }
    });
  }

  deleteBooking(id: number) {
    if (!confirm('Are you sure you want to delete this booking?')) return;

    this.api.deleteBooking(id).subscribe({
      next: () => {
        this.loadBookings();
        alert('Booking deleted successfully');
      },
      error: (err) => {
        console.error('Error deleting booking:', err);
        alert('Failed to delete booking');
      }
    });
  }

  getStatusLabel(status: number): string {
    const option = this.statusOptions.find(opt => opt.value === status);
    return option ? option.label : 'Unknown';
  }

  getStatusClass(status: number): string {
    const statusMap: { [key: number]: string } = {
      1: 'status-pending',
      2: 'status-confirmed',
      3: 'status-checkedin',
      4: 'status-checkedout',
      5: 'status-cancelled'
    };
    return statusMap[status] || '';
  }

  private getEmptyBooking(): BookingDetails {
    return {
      customerId: 0,
      roomId: 0,
      checkInDate: new Date().toISOString().slice(0, 16),
      checkOutDate: new Date().toISOString().slice(0, 16),
      status: 1,
      totalAmount: 0
    };
  }
}