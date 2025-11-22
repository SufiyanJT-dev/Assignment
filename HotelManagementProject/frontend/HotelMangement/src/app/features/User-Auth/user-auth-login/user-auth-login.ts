import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Apicommuncation } from '../../../shared/Api/apicommuncation';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { Console } from 'console';


@Component({
  selector: 'app-user-auth-login',
  standalone: true, // only if you want standalone component
  imports: [FormsModule],
  templateUrl: './user-auth-login.html',
  styleUrls: ['./user-auth-login.scss'],
})
export class UserAuthLogin implements OnInit {
  returnUrl: string = '/';
  errormessage:string="";
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
        localStorage.setItem('JwtAccessToken', this.loginResponse.accessToken);
        localStorage.setItem('role',this.loginResponse.role)
         localStorage.setItem('userId',this.loginResponse.id)
       console.log(this.loginResponse)
        this.location.back();
      },
      error: (err) => {
        console.log(loginDetails);
        console.error('Login failed:', err);
         if(err.status===401){
              this.errormessage="Invalid Password or Email"
          }else{
            this.errormessage="something went wrong"
          }
      },
    });
  }
}
