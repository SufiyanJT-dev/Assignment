import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Apicommuncation } from '../../../shared/Api/apicommuncation';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login-auth',
  imports: [FormsModule],
  templateUrl: './login-auth.html',
  styleUrl: './login-auth.scss',
})
export class LoginAuth {
  errorMessage:string="";
  constructor(private api:Apicommuncation,private router: Router){}
  
onSubmit(form:any){
  const loginDetails=form.value;
  this.errorMessage="";
  this.api.Validation(loginDetails).subscribe({
    next:(res)=>{
     console.log(res)
      localStorage.setItem('JwtAccessToken',res.accessToken);
      localStorage.setItem('userId',res.id)
      const token = sessionStorage.getItem('JwtAccessToken');
      
      console.log( res)
       this.router.navigate(['/Admin-DashBoard'])
    },
    error:(err)=>{
       if (err.status === 401) {
          this.errorMessage = "Invalid email or password.";
        } else {
          this.errorMessage = "Something went wrong. Try again.";
        }
    }
  })
}
}
