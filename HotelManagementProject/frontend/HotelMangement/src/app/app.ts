import { Component, signal } from '@angular/core';
import { Router,NavigationEnd , RouterOutlet } from '@angular/router';
import { Navbar } from './core/Layout/navbar/navbar';

import { Footer } from './core/Layout/footer/footer';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  standalone:true,
  styleUrl: './app.scss'
})
export class App {
    
  constructor(public router: Router) {
  }
  protected readonly title = signal('HotelMangement');


    
      
    
}
