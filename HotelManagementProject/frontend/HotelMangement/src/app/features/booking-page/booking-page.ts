import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SerachServices } from '../search-page/Shared/serach-services';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router'; // <-- Import Router
import { Apicommuncation } from '../../shared/Api/apicommuncation';
import { VerifyPaymentCommand } from "./type/VerifyPaymentCommand";
import { InitiatePaymentCommand } from "./type/InitiatePaymentCommand";
import { from } from 'rxjs';

// This tells TypeScript that a variable called 'Razorpay'
// exists in the global scope (from the script we added)
declare var Razorpay: any;

@Component({
  selector: 'app-booking-page',
  // imports: [FormsModule, CommonModule], // 'imports' is not a valid property here
  templateUrl: './booking-page.html',
  styleUrls: ['./booking-page.scss'],
})
export class BookingPage implements OnInit { // <-- Add OnInit
  constructor(
    private serachServices: SerachServices,
    private route: ActivatedRoute,
    private api: Apicommuncation,
    private router: Router // <-- Inject Router
  ) {}

  searchData: any;
  bookedRoomDeatils = {
    roomNumber: '',
    hotelId: 0,
    roomTypeId: 0,
    status: 0,
    pricePerNight: 0
  };

  booking = {
    customerId: 1, // <-- TODO: Get this from your auth service (e.g., logged-in user)
    roomId: 0,
    checkInDate: '',
    checkOutDate: '',
    status: 1,
    totalAmount: 0,
    nights: 0,
    tax: 0,
    grandTotal: 0
  };

  ngOnInit() {
    this.searchData = this.serachServices.getData();
    this.booking.checkInDate = this.searchData.checkInDate;
    this.booking.checkOutDate = this.searchData.checkOutDate;

    this.route.queryParams.subscribe(params => {
      this.booking.roomId = +params['Roomid'];
    });

    this.api.GetRoomById(this.booking.roomId).subscribe({
      next: (value) => {
        this.bookedRoomDeatils = value;
        this.calculateTotals();
      },
      error(err) {
        console.log(err);
      },
    });
  }

  calculateTotals() {
    const inDate = new Date(this.booking.checkInDate);
    const outDate = new Date(this.booking.checkOutDate);
    const diffTime = outDate.getTime() - inDate.getTime();
    const nights = diffTime / (1000 * 3600 * 24);

    if (nights > 0) {
      this.booking.nights = nights;
      const subtotal = nights * this.bookedRoomDeatils.pricePerNight;
      const tax = subtotal * 0.1; // 10% tax
      this.booking.totalAmount = subtotal;
      this.booking.tax = tax;
      // We will use the grandTotal for payment
      this.booking.grandTotal = subtotal + tax; 
    }
  }

  // This replaces your old onSubmit
  payNow() {
    // 1. Validation
    const inDate = new Date(this.booking.checkInDate);
    const outDate = new Date(this.booking.checkOutDate);
    if (outDate <= inDate) {
      alert('Check-out must be after check-in.');
      return;
    }
    this.calculateTotals(); // Recalculate just in case

    // 2. Create the command for our 'initiate' API
    const initiateCommand: InitiatePaymentCommand = {
      customerId: this.booking.customerId, // <-- TODO: Get this from your auth service
      roomId: this.booking.roomId,
      checkInDate: this.booking.checkInDate,
      checkOutDate: this.booking.checkOutDate
    };

    // 3. Call the 'initiate' API
    this.api.initiatePayment(initiateCommand).subscribe({
      next: (orderResponse) => {
        // --- STEP 4: We got the Order ID, now open Razorpay ---
        const options = {
          key: 'rzp_test_YOUR_KEY_ID_HERE', // <-- Add your Razorpay Key ID
          amount: orderResponse.amount * 100, // Amount is in paise
          currency: 'INR',
          name: 'Hotel Booking System',
          description: `Booking for Room ${this.bookedRoomDeatils.roomNumber}`,
          order_id: orderResponse.razorpayOrderId,
          
          // --- STEP 5: This function is called on payment success ---
          handler: (response: any) => {
            const verifyCommand: VerifyPaymentCommand = {
              razorpayOrderId: response.razorpay_order_id,
              razorpayPaymentId: response.razorpay_payment_id,
              razorpaySignature: response.razorpay_signature,
              bookingId: orderResponse.bookingId
            };

            // --- STEP 6: Call our 'verify' API ---
            this.api.verifyPayment(verifyCommand).subscribe({
              next: (verifyResponse) => {
                alert('Booking Confirmed!');
                this.router.navigate(['/booking-success']); // Navigate to success page
              },
              error: (err) => {
                alert('Payment verification failed. Please contact support.');
                console.error(err);
              }
            });
          },
          prefill: {
            name: 'Customer Name', // TODO: Get from user profile
            email: 'customer.email@example.com',
          },
          theme: {
            color: '#3399cc'
          }
        };
        
        // --- STEP 7: Create and open the Razorpay modal ---
        const rzp = new Razorpay(options);
        rzp.open();
      },
      error: (err) => {
        alert('Could not start payment. Please try again.');
        console.error(err);
      }
    });
  }
  onSubmit(formValue: any) {
    const inDate = new Date(formValue.checkInDate);
    const outDate = new Date(formValue.checkOutDate);
    if (outDate <= inDate) {
      alert('Check-out must be after check-in.');
      return;
    }

    this.calculateTotals();

    console.log('Booking payload:', {
      ...formValue,
      totalAmount: this.booking.totalAmount,
      tax: this.booking.tax,
      grandTotal: this.booking.grandTotal
    });
  }
}
