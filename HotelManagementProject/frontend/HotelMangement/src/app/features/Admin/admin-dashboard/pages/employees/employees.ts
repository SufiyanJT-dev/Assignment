import { ApplicationModule, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EmployeeDetails } from './type/EmployeeDetails';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Router } from 'express';
import { error, log } from 'console';
import { employeeServies } from './Services/employeeServies';

@Component({
  selector: 'app-employees',
  imports: [FormsModule,CommonModule],
  templateUrl: './employees.html',
  styleUrl: './employees.scss',
})
export class Employees {
  constructor(private route:ActivatedRoute,private api:employeeServies){}
isEditMode: boolean = false;

showForm: boolean = false;
HotelId:number=0;
employeeList:EmployeeDetails[]=[];
formData:EmployeeDetails= this.createEmptyForm(); 
ngOnInit(){
  this.route.queryParams.subscribe(params => {
      this.HotelId = +params['hotelId'];
      
    });
    this.api.GetAllEmployeeByHotelId(this.HotelId).subscribe({
      next:(value)=>{
        this.employeeList=value;
      },
      error:(err)=>{
        console.log(err);
      }
    });
}
OnAddEmployee(){
  this.isEditMode=false;
  this.showForm = true;
   this.formData = {id:this.formData.id, hotelId: this.HotelId, fullName: '', email: '', role:'',password: '' };
   console.log(this.formData);
}
 createEmptyForm(): EmployeeDetails {
    return {id:0, hotelId: this.HotelId, fullName: '', email: '', role:'',password: '' };
  }
onSubmit(){
   if(this.isEditMode){
      this.api.UpdateEmploee(this.formData.id,this.formData).subscribe({
        next:(value)=>{
            console.log("Updated");

        },
        error:(error)=>{
          console.log(error);
          
        }
      });
   }
   else{
    const payload={
       hotelId: this.formData.hotelId,
  fullName: this.formData.fullName,
  role: this.formData.role,
  email:this.formData.email,
  password: this.formData.password
    }
     this.api.AddEmploee(payload).subscribe({
        next:(value)=>{
            console.log("Added");

        },
        error:(error)=>{
          console.log(error);
          
        }
      });
   }
}
onEdit(employee: EmployeeDetails){
  this.isEditMode=true;
  this.showForm = true;
    this.formData = { ...employee };
}
OnDeleteEmployee(id:number){
this.api.deleteEmplyee(id).subscribe({
  next:(value)=>{
    console.log("deleted")
  },
  error:(err)=>{
    console.log(err);
  }
})
}
onCancel(){
  this.isEditMode = false;
  this.showForm = false;
}
}
