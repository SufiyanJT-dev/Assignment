import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Apicommuncation } from '../../shared/Api/apicommuncation';
import { UserData } from './type/UserData';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { CommonModule } from '@angular/common';
import { Navbar } from '../../core/Layout/navbar/navbar';

@Component({
  selector: 'app-profile',
  imports: [CommonModule,Navbar, MatCardModule, MatIconModule, MatButtonModule, MatDividerModule],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
})
export class Profile {
  token:string='';
  storedUserID:string='';
  userId:number=0;
  userData!:UserData;
  constructor(private router:Router,private api:Apicommuncation){}
ngOnInit(){
   this.storedUserID=localStorage.getItem('userId')||''
   this.token=localStorage.getItem('JwtAccessToken') || ''; 
   if(!this.storedUserID){
    this.router.navigate(['/Profile'])
   }
    if(this.storedUserID){
      this.userId=Number(this.storedUserID);
    }
  
   else{
     this.router.navigate(['/login'])
   }
   
   this.api.GetUserDataById(this.userId).subscribe({
    next:(value:any)=>{
      this.userData=value;
      console.log(this.userData)
    },
    error:(err)=>{

    }
   })

   
}
Logout(){
  localStorage.removeItem('JwtAssesToken');
  localStorage.removeItem('userId');
  localStorage.removeItem('role');
  this.router.navigate(['/']);
}
}
