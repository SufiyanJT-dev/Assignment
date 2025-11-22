import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.scss'],
})
export class Navbar implements OnInit {
  jwttoken: string = '';
  buttonLabel: string = 'Login';
isLoggedIn: boolean = true;

  constructor(private router: Router) {}

  ngOnInit() {
    this.jwttoken = localStorage.getItem('JwtAssesToken') || '';
    if (this.jwttoken) {
      this.buttonLabel = 'Logout';
      this.isLoggedIn = false;
    
    }
  }
goHome(){
    this.router.navigate(['/'])
  }
gotoProfile(){
this.router.navigate(['/Profile']);
}
gotoBooking(){
  this.router.navigate(['/Booking']);
}
gotoLogin() {
    if (this.buttonLabel === 'Login') {
      
      this.router.navigate(['/login']);
    } else {
      
      localStorage.removeItem('JwtAssesToken');
      this.jwttoken = '';
      this.buttonLabel = 'Login';
      this.isLoggedIn=true;
     
      console.log('Logged out');
    }
  }
}
