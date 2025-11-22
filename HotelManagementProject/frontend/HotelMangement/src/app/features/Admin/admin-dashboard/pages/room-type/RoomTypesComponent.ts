import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Apicommuncation } from '../../../../../shared/Api/apicommuncation'; // Adjust path if needed
import { Router } from '@angular/router';
import { log } from 'node:console';
import { RoomTypeForAddData } from './Type/RoomType';

// Define the RoomType interface based on your model
export interface RoomType {
  id: number;
  typeName: string;
  description: string;
  capacity: number;
}


@Component({
  selector: 'app-room-types', // The selector for this component
  standalone: true, // Assuming Angular 17+ with standalone
  imports: [CommonModule, FormsModule],
  templateUrl: './room-type.html',
  styleUrls: ['./room-type.scss'],
})
export class RoomTypesComponent implements OnInit {
  roomTypes: RoomType[] = [];
  showForm: boolean = false;
  isEditMode: boolean = false;
  FormDataForAddRoomType:RoomTypeForAddData[]=[];
  
  formData: RoomType = this.createEmptyForm(); 

  constructor(private api: Apicommuncation, private router: Router) {}

  ngOnInit() {
    
    this.loadRoomTypes();
  }

  
  loadRoomTypes() {
    
    this.api.getAllRoomType().subscribe({
      next: (res) => {
        this.roomTypes = res;
      },
      error: (err) => console.error('Error loading room types:', err)
    });
  }


  createEmptyForm(): RoomType {
    return { id: 0, typeName: '', description: '', capacity: 1 };
  }
  createEmptyFormDataForAdd():RoomTypeForAddData{
    return {  typeName: '', description: '', capacity: 1 };
  }

  
  onAddRoomType() {
    this.isEditMode = false;
    this.FormDataForAddRoomType = [this.createEmptyFormDataForAdd()]
    this.showForm = true; 
  }

  
  onEditRoomType(roomType: RoomType) {
    this.isEditMode = true;
    this.formData = { ...roomType }; 
    this.showForm = true;
  }

 
  onDeleteRoomType(id: number) {
    if (confirm('Are you sure you want to delete this room type?')) {
      
      this.api.DeleteRoomType(id).subscribe({
        next: () => {

          this.loadRoomTypes();
        },
        error: (err: any) => console.error('Error deleting room type:', err)
      });
    }
  }

 
  onCancel() {
    this.showForm = false;
  }

 
  onSubmit() {
    
    this.formData.capacity = +this.formData.capacity; 

    if (this.isEditMode) {
     
      this.api.UpdateRoomType(this.formData.id, this.formData).subscribe({
        next: () => {
          this.showForm = false;
          this.loadRoomTypes();
        },
        error: (err: any) =>{ 
          console.log(this.formData);
          console.error('Error updating room type:', err)}
      });
    } else {
      
       const addPayload = {
        typeName: this.formData.typeName,
        description: this.formData.description,
        capacity: this.formData.capacity,
      };
      this.api.AddRoomType(addPayload).subscribe({
        next: () => {
          this.showForm = false;
          this.loadRoomTypes();
          console.log(addPayload);
        },
        error: (err: any) => {console.error('Error adding room type:', err)
            console.log(addPayload);
        }
      });
    }
  }
}