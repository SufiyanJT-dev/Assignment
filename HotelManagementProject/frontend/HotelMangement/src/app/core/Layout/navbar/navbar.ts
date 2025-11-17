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
isLoggedInpro: boolean = false;
  constructor(private router: Router) {}

  ngOnInit() {
    this.jwttoken = localStorage.getItem('JwtAssesToken') || '';
    if (this.jwttoken) {
      this.buttonLabel = 'Logout';
      this.isLoggedIn = false;
      this.isLoggedInpro=true;
    }
  }
  goHome(){
    this.router.navigate(['/'])
  }
GotoProfile(){
this.router.navigate(['/Profile']);
}
GotoBooking(){
  this.router.navigate(['/Booking']);
}
  GotoLogin() {
    if (this.buttonLabel === 'Login') {
      
      this.router.navigate(['/login']);
    } else {
      
      localStorage.removeItem('JwtAssesToken');
      this.jwttoken = '';
      this.buttonLabel = 'Login';
      this.isLoggedIn=true;
      this.isLoggedInpro=false;
      console.log('Logged out');
    }
  }
}
