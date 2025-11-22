import { Component } from '@angular/core';
import { Banner } from './banner/banner';
import { Popularlocation } from '../popularlocation/popularlocation';
import { Navbar } from '../../../core/Layout/navbar/navbar';
import { Router, RouterOutlet } from '@angular/router';
import { Footer } from '../../../core/Layout/footer/footer';

@Component({
  selector: 'app-home-page',
  standalone:true,
  imports: [Banner,Popularlocation,Navbar,Footer],
  templateUrl: './home-page.html',
  styleUrl: './home-page.scss',
})
export class HomePage {

}
