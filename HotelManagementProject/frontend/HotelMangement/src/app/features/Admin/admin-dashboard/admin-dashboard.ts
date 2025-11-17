import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Apicommuncation } from '../../../shared/Api/apicommuncation';
import { error } from 'console';
import { Sidebar } from './components/sidebar/sidebar';

@Component({
  selector: 'app-admin-dashboard',
  imports: [Sidebar,RouterOutlet],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.scss',
})
export class AdminDashboard {

  constructor(private router: Router, private api: Apicommuncation) { }
  ngOnInit() {
    const storedToken = localStorage.getItem('JwtToken');
    if (storedToken == null) {
      this.router.navigate(['/login']);
    }
  }
  
}
