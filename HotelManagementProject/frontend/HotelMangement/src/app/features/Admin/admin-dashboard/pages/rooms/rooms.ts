import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Apicommuncation } from '../../../../../shared/Api/apicommuncation';
import { ActivatedRoute, Router } from '@angular/router';
import { Room } from './type/Room';
import { FormsModule } from '@angular/forms';
import { RoomType } from './type/RoomType';
import { RoomUpdate } from './type/RoomUpdate';

@Component({
  selector: 'app-rooms',
  imports: [CommonModule, FormsModule],
  templateUrl: './rooms.html',
  styleUrls: ['./rooms.scss'],
})
export class Rooms {
  constructor(
    private api: Apicommuncation,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  HotelId: number = 0;
  roomsList: Room[] = [];
  roomTypes: RoomType[] = [];
  roomTypeMap: { [id: number]: RoomType } = {};
  showForm: boolean = false;
  isEditMode: boolean = false;
  formData: any = {};

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.HotelId = +params['hotelId'];
      this.loadRooms();
      this.loadRoomTypes();
    });
  }

  // ✅ Load rooms with fresh array reference
  loadRooms() {
    this.api.GetAllRoomsByHotelId(this.HotelId).subscribe({
      next: (res) => {
        this.roomsList = [...res]; // spread ensures Angular detects changes
        console.log("Rooms reloaded", this.roomsList);
      },
      error: (err) => console.log(err)
    });
  }

  loadRoomTypes() {
    this.api.getAllRoomType().subscribe({
      next: (res) => {
        this.roomTypes = res;
        this.roomTypeMap = {};
        res.forEach((rt: RoomType) => this.roomTypeMap[rt.id] = rt);
      },
      error: (err) => console.log(err)
    });
  }

  onAddRoom() {
    this.showForm = true;
    this.isEditMode = false;
    this.formData = {
      roomNumber: '',
      roomTypeId: 0,
      pricePerNight: 0,
      status: 0,
      hotelId: this.HotelId
    };
  }

  // ✅ No mutation of original object
  onEditRoom(roomUpdate: RoomUpdate) {
    this.showForm = true;
    this.isEditMode = true;
    this.formData = { ...roomUpdate, hotelId: this.HotelId };
  }

  onDeleteRoom(roomId: number) {
    this.api.DeleteRoom(roomId).subscribe({
      next: () => {
        this.loadRooms(); // refresh list only
      },
      error: (err) => console.log(err)
    });
  }

  onBooking(roomId: number) {
    this.router.navigate(['Admin-DashBoard/booking'], { queryParams: { roomId } });
  }

  GoToRoomType(id: number) {
    this.router.navigate(['Admin-DashBoard/RoomType'], { queryParams: { id } });
  }

  onSubmitRoom() {
    this.formData.roomTypeId = +this.formData.roomTypeId;
    this.formData.status = +this.formData.status;
    this.formData.hotelId = this.HotelId;

    const request$ = this.isEditMode
      ? this.api.UpdateRoom(this.formData.id, this.formData)
      : this.api.AddRoom(this.formData);

    request$.subscribe({
      next: () => {
        this.showForm = false;
        this.loadRooms(); // refresh list
      },
      error: (err) => {
        console.log(err);
        console.log(this.formData);
      }
    });
  }

  onCancel() {
    this.showForm = false;
  }
}
