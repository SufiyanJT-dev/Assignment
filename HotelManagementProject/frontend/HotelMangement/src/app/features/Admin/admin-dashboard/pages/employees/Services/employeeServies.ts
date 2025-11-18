import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeDetails } from '../type/EmployeeDetails';

@Injectable({
  providedIn: 'root',
})
export class employeeServies {
   constructor(private http: HttpClient) { }
  private api="https://localhost:7119/api/"

GetAllEmployeeByHotelId(HotelId:number):Observable<any>{
  return this.http.get(`${this.api}Employee/hotel/${HotelId}`);
}
AddEmploee(formData:any):Observable<any>{
  return this.http.post(`${this.api}Employee/`,formData);
}
UpdateEmploee(id:number,formData:EmployeeDetails):Observable<any>{
  return this.http.patch(`${this.api}Employee/${id}`,formData);
}
deleteEmplyee(id:number):Observable<any>{
  return this.http.delete(`${this.api}Employee/${id}`);
}
}
