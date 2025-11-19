import { Component, OnInit } from '@angular/core';
import { Apicommuncation } from '../../../../shared/Api/apicommuncation';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Search } from './type/SerachInterface';

@Component({
  selector: 'app-left-sidecomponent',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './left-sidecomponent.html',
  styleUrls: ['./left-sidecomponent.scss'],
})
export class LeftSidecomponent implements OnInit {
  isEditing: boolean = false;
priceFilter: number = 30000;
  Search: Search = {
    location: '',
    checkInDate: '',
    checkOutDate: '',
   
  };

  constructor(
    
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.Search = {
        
        location: params['location'] || '',
        checkInDate: params['checkInDate'] || '',
        checkOutDate: params['checkOutDate'] || ''
      };
    });
    
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
    
  }
onPriceChange() {
  console.log("Price selected:", this.priceFilter);
}
 OnApplyFilter(){
    this.priceFilter
      this.router.navigate([], {
      queryParams: {
        maxPrice:  this.priceFilter,
        location: this.Search.location,
        checkInDate: this.Search.checkInDate,
        checkOutDate: this.Search.checkOutDate
      },
      queryParamsHandling: 'merge'
    });

 }

  applyChanges() {
    this.isEditing = false;
    console.log('Updated search:', this.Search);

    this.router.navigate([], {
      queryParams: {
        maxPrice:  this.priceFilter,
        location: this.Search.location,
        checkInDate: this.Search.checkInDate,
        checkOutDate: this.Search.checkOutDate
      },
      queryParamsHandling: 'merge'
    });
  }
}
