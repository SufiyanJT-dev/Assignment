import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Apicommuncation } from '../../../shared/Api/apicommuncation';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
// Define the expected response type
interface LoginResponse {
  accessToken: string;
  // add other fields if your API returns them
}

@Component({
  selector: 'app-user-auth-login',
  standalone: true, // only if you want standalone component
  imports: [FormsModule],
  templateUrl: './user-auth-login.html',
  styleUrls: ['./user-auth-login.scss'],
})
export class UserAuthLogin implements OnInit {
  returnUrl: string = '/';

  constructor(
    private api: Apicommuncation,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit() {
    }
loginResponse:any;
  onSubmit(form: any) {
    const loginDetails = form.value;
    this.api.CustomerAuthLogin(loginDetails).subscribe({
       next: (res) => {
        this.loginResponse = res;
        localStorage.setItem('JwtAssesToken', this.loginResponse.accessToken);
        localStorage.setItem('userId',this.loginResponse.id)
        
       
        this.location.back();
      },
      error: (err) => {
        console.log(loginDetails);
        console.error('Login failed:', err);
      },
    });
  }
}
