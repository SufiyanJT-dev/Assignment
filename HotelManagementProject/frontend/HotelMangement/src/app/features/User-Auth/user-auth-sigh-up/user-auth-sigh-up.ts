import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Apicommuncation } from '../../../shared/Api/apicommuncation';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-auth-sign-up',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-auth-sigh-up.html',
  styleUrls: ['./user-auth-sigh-up.scss'],
})
export class UserAuthSignUp implements OnInit {
  signupForm!: FormGroup;
  errormessage:string="";
  constructor(private fb: FormBuilder,private router:Router,private api:Apicommuncation) {}

  ngOnInit(): void {
    this.signupForm = this.fb.group(
      {
        fullName: ['', [Validators.required, Validators.minLength(3)]],
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
        idProofNumber: ['', [Validators.required]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: this.passwordMatchValidator }
    );
  }

 
  passwordMatchValidator(control: AbstractControl) {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.signupForm.valid) {
      const { confirmPassword, ...formData } = this.signupForm.value;
      console.log('Form submitted:', formData);
      this.api.SignUpUser(formData).subscribe({
        next:(value)=>{
          
          this.router.navigate(['/login'])
        },
        error:(err)=>{
          console.log(err);
         
        }
      })

    } else {
      console.log('Form invalid');
    }
  }
}
