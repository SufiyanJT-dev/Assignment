import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar-admin',
  templateUrl: './navbar-admin.html',
  styleUrls: ['./navbar-admin.scss']
})
export class NavbarAdmin {
  isMenuOpen = false;
  
  constructor(private router: Router) {}

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  navigate(path: string) {
    this.isMenuOpen = false;
    this.router.navigate([path]);
  }

  onLogout() {
   
    
  localStorage.removeItem('userId');
  localStorage.removeItem('role');
    localStorage.removeItem('JwtAccessToken')
    this.router.navigate(['/AdminLogin']);
  }
}
