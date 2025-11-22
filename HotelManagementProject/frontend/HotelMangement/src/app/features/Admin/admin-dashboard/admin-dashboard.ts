import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Apicommuncation } from '../../../shared/Api/apicommuncation';
import { error } from 'console';
import { Sidebar } from './components/sidebar/sidebar';
import { NavbarAdmin } from './components/navbar-admin/navbar-admin';

@Component({
  selector: 'app-admin-dashboard',
  imports: [Sidebar,RouterOutlet,NavbarAdmin],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.scss',
})
export class AdminDashboard {

  constructor(private router: Router, private api: Apicommuncation) { }
  ngOnInit() {
    const storedToken = localStorage.getItem('JwtAccessToken');
    const userRrole=localStorage.getItem('role')
    if(userRrole=='Customer'){
      this.router.navigate(['/']);
    }
    if (storedToken == null) {
      this.router.navigate(['/AdminLogin']);
    }
  }
  
}
