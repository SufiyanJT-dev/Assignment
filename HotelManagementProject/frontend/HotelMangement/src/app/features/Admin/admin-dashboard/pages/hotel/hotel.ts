import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { Apicommuncation } from '../../../../../shared/Api/apicommuncation';
import { HotelDetails } from './Type/HotelDeatils';
import { addHotelDeatils } from './Type/AddHotelDeatils';
import { Router, RouterOutlet } from '@angular/router';
import { log } from 'console';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'app-hotel',
  imports: [FormsModule,MatToolbarModule,MatGridListModule,MatIconModule,MatInputModule, MatFormFieldModule,MatCardModule,MatButtonModule,CommonModule,RouterOutlet],
  templateUrl: './hotel.html',
  styleUrl: './hotel.scss',
})
export class Hotel {
  
  hotels: HotelDetails[] = [];
  newHotels: addHotelDeatils = {
    Name: '',
    Address: '',
    City: '',
    Country: '',
    Description: '',
    PhoneNumber: '',
    Path: ''
  };
  selectedHotel: any = {};
  toastMessage = "";
toastType: 'success' | 'error' = 'success';
showToast = false;
  selectedFile: File | null = null;
  showForm = false;
  searchTerm = '';
  
  constructor(private api: Apicommuncation, private router: Router) {}

  ngOnInit() {
    this.getAllHotel();
  }

  getAllHotel() {
    this.api.getAllHotel().subscribe({
      next: (data: any[]) => {
        this.hotels = data.map(hotel => ({
          id: hotel.id,
          name: hotel.name,
          address: hotel.address,
          city: hotel.city,
          country: hotel.country,
          description: hotel.description,
          phoneNumber: hotel.phoneNumber,
          path: 'https://localhost:7119' + hotel.path
        }));
      },
      error: err => console.error(err)
    });
  }

  toggleForm() {
    this.showForm = !this.showForm;
    this.selectedHotel = null; 
  }
  showToastMessage(message: string, type: 'success' | 'error') {
  this.toastMessage = message;
  this.toastType = type;
  this.showToast = true;

  setTimeout(() => {
    this.showToast = false;
  }, 3000);
}

  addHotel() {
    const formData = new FormData();
    formData.append('Name', this.newHotels.Name);
    formData.append('Address', this.newHotels.Address);
    formData.append('City', this.newHotels.City);
    formData.append('Country', this.newHotels.Country);
    formData.append('Description', this.newHotels.Description);
    formData.append('PhoneNumber', this.newHotels.PhoneNumber);
    if (this.selectedFile) {
      formData.append('Image', this.selectedFile);
    }

    this.api.AddHotel(formData).subscribe({
      
      next:(value)=> {
        console.log(value);
        this.ngOnInit()
        this.showToastMessage("Hotel added successfully!", "success");
      },
      error: err =>{
         console.log(formData);
       if (err.status === 400) {
      this.showToastMessage("You must enter all values.", "error");
    } else {
      this.showToastMessage("Something went wrong. Try again.", "error");
    }
      }
      
    });
 this.router.navigate([]);
    this.newHotels = { Name:'', Address:'', City:'', Country:'', Description:'', PhoneNumber:'', Path:'' };
    this.showForm = false;
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      if (this.selectedHotel) {
        this.selectedHotel.path = input.files[0].name;
      } else {
        this.newHotels.Path = input.files[0].name;
      }
    }
  }

  editHotel(hotel: HotelDetails) {
    this.selectedHotel = { ...hotel };
  }

  saveHotel() {
  if (!this.selectedHotel) return;

  const formData = new FormData();

  formData.append('Name', this.selectedHotel.name);
  formData.append('Address', this.selectedHotel.address);
  formData.append('City', this.selectedHotel.city);
  formData.append('Country', this.selectedHotel.country);
  formData.append('PhoneNumber', this.selectedHotel.phoneNumber);


  formData.append('Description', this.selectedHotel.description);

  if (this.selectedFile) {
    formData.append('Image', this.selectedFile);
  }


  this.api.UpdateHotel(this.selectedHotel.id, formData).subscribe({
    next: () => {
       this.showToastMessage("Hotel Updated successfully!", "success")
      this.getAllHotel();
      this.selectedHotel = null;
      this.selectedFile = null;
     
    },
    error: err => {console.error(err)
      for (let [key, value] of formData.entries()) {
  console.log(key, value);
    this.showToastMessage("Something went wrong. Try again.", "error");
  
}
    }
  });
}


  deleteHotel(id: number) {
    this.api.DeleteHotel(id).subscribe({
     
      
      next: () => this.getAllHotel(),
      error: err => {
        
        this.showToastMessage("Hotel Deleted successfully!", "success");
            if (err.status === 400) {
      
      this.showToastMessage("Something went wrong. Try again.", "error");
    }
        console.error(err)}
    });
  }
  GotoEmployee(hotelId: number){
this.router.navigate(['Admin-DashBoard/employees'], { queryParams: { hotelId } });
  }
goToRoomList(hotelId: number) {
    this.router.navigate(['Admin-DashBoard/room'], { queryParams: { hotelId } });
  }
  filteredHotels() {
    return this.hotels.filter(h =>
      h.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}
