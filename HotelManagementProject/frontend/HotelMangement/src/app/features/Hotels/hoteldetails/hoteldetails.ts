import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Navbar } from '../../../core/Layout/navbar/navbar';
import { SerachServices } from '../../search-page/Shared/serach-services';
import { FormValues } from '../../search-page/compounents/right-sidecomponent/type/FormValues';
import { Apicommuncation } from '../../../shared/Api/apicommuncation';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

// Strongly typed Room interface
export interface Room {
  id: number;
  hotelId: number;
  roomNumber: string;
  roomTypeId: number;
  roomType?: string | null;
  pricePerNight: number;
  status: string;
  bookings?: Booking[];
}

export interface Booking {
  id: number;
  checkInDate: string;
  checkOutDate: string;
  status: string;
}

@Component({
  selector: 'app-hoteldetails',
  imports: [Navbar,CommonModule],
  templateUrl: './hoteldetails.html',
  styleUrls: ['./hoteldetails.scss'],
})
export class Hoteldetails {
  selectedHotelDetails: any = {};
  searchData: FormValues | null = null;
  hotelId: number = 0;

  rooms: Room[] = [];
  loading = false;
  error: string | null = null;
  token:string=''
  constructor(
    private route: ActivatedRoute,
    private serachServices: SerachServices,
    private api: Apicommuncation,
    private router:Router
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['hotelDetails']) {
        this.selectedHotelDetails = JSON.parse(params['hotelDetails']);
        this.hotelId = this.selectedHotelDetails?.id || 0;
      }
    });

    this.searchData = this.serachServices.getData();

    const payload = {
      hotelId: this.hotelId,
      checkInDate: this.searchData?.checkInDate,
      checkOutDate: this.searchData?.checkOutDate,
    };

    this.loading = true;
    this.api.getFilteredHotelRooms(payload).subscribe({
      next: (value: Room[]) => {
        this.rooms = value;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to fetch rooms';
        this.loading = false;
      }
    });
  }

  // Booking action
  bookRoom(roomid:number) {
   
   this.token=localStorage.getItem('JwtAssesToken') || ''; 
   if(this.token){
    this.router.navigate(['/BookingDeatils'],{queryParams:{Roomid:roomid}})
   }
   else{
     this.router.navigate(['/login'])
   }
  }
}

