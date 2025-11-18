import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Apicommuncation } from '../../../../shared/Api/apicommuncation';
import { FormValues } from './type/FormValues';
import { FormsModule } from '@angular/forms';

interface Hotel {
  id: number;
  name: string;
  city: string;
  pricePerNight: number;
  path: string;
  lowestPrice: number;
}

@Component({
  selector: 'app-right-sidecomponent',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './right-sidecomponent.html',
  styleUrls: ['./right-sidecomponent.scss'],
})
export class RightSidecomponent implements OnInit {
  formValues: FormValues = {
    maxPrice: 3000,
    location: '',
    checkInDate: '',
    checkOutDate: '',
  };

  results: any[] = [];
  HotelValues: Hotel[] = [];
  selectedSort: string = 'recommended';

  constructor(
    private api: Apicommuncation,
    private location: Location,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.formValues = {
        maxPrice: +params['maxPrice'] ? +params['maxPrice']:300000,
        location: params['location'] || '',
        checkInDate: params['checkInDate'] || '',
        checkOutDate: params['checkOutDate'] || '',
      };

      this.loadHotels();
    });
  }

  loadHotels() {
    this.api.filtering(this.formValues).subscribe({
      next: (response) => {
        this.results = response;
        console.log('Raw API results:', this.results);

        const hotelMap = new Map<number, Hotel>();

        response.forEach((room: any) => {
          const hotel = room.hotel;
          if (!hotel) return; // skip invalid rooms

          const existingHotel = hotelMap.get(hotel.id);

          if (!existingHotel) {
            // Add hotel first time
            hotelMap.set(hotel.id, {
              ...hotel,
              lowestPrice: room.pricePerNight,
            });
          } else {
            // Update lowest price safely
            if (room.pricePerNight < existingHotel.lowestPrice) {
              existingHotel.lowestPrice = room.pricePerNight;
              hotelMap.set(hotel.id, existingHotel); // update map
            }
          }
        });

        // ✅ Filter by maxPrice
    this.HotelValues = Array.from(hotelMap.values()).filter(
  hotel => hotel.lowestPrice <= (this.formValues?.maxPrice ?? 3000)
);


        // ✅ Apply initial sort if needed
        this.onSortChange();
      },
      error: (err) => {
        console.error('API error:', err);
      },
    });
  }

  onSortChange() {
    console.log('Selected sort:', this.selectedSort);

    if (this.selectedSort === 'lowToHigh') {
      this.HotelValues.sort((a, b) => a.lowestPrice - b.lowestPrice);
    } else if (this.selectedSort === 'highToLow') {
      this.HotelValues.sort((a, b) => b.lowestPrice - a.lowestPrice);
    } else {
      // "recommended" or default → no sorting, keep API order
    }
  }

  getImageUrl(path: string): string {
    if (!path) return 'assets/no-image.png';
    return `https://localhost:7119${path}`;
  }

  openHotelDetails(id: number) {
    const hotelDetails = this.HotelValues.find((item) => item.id === id);
    if (!hotelDetails) return;

    const encodedDetails = JSON.stringify(hotelDetails);

    this.router.navigate(['/HotelDetails'], {
      queryParams: { hotelDetails: encodedDetails },
    });
  }
}
