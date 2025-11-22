import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Apicommuncation } from '../../../../shared/Api/apicommuncation';
import { Console, error } from 'console';
import { Router } from '@angular/router';
import { SearchFormData } from '../Type/SerachInterface';

@Component({
  selector: 'app-banner',
  imports: [CommonModule,FormsModule],
  templateUrl: './banner.html',
  styleUrl: './banner.scss',
})
export class Banner {
 
  constructor(private router:Router ,private api:Apicommuncation){}
  todaydate!: string;
tomorrow!: string;

ngOnInit() {
  const today = new Date();
  const tomorrow = new Date(today);
  tomorrow.setDate(today.getDate() + 1);


}

onSearch(form: any) {
    const formValues: SearchFormData={
      location: form.value.location || '',
    checkInDate: form.value.checkInDate ||  new Date() ,
    checkOutDate: form.value.checkOutDate || '',
  };
    this.router.navigate(['/result-page'],{
      queryParams:formValues,
      
    });         
     
}
}
