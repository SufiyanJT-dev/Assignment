import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { addHotelDeatils } from '../../features/Admin/admin-dashboard/pages/hotel/Type/AddHotelDeatils';
import { RoomType } from '../../features/Admin/admin-dashboard/pages/room-type/RoomTypesComponent';
import { BookingDetails } from '../../features/Admin/admin-dashboard/pages/booking/type/BookingDetails';
import { RoomTypeForAddData } from '../../features/Admin/admin-dashboard/pages/room-type/Type/RoomType';
import { EmployeeDetails } from '../../features/Admin/admin-dashboard/pages/employees/type/EmployeeDetails';
import { PaymentOrderResponse } from "../../features/booking-page/type/PaymentOrderResponse";
import { VerifyPaymentCommand } from "../../features/booking-page/type/VerifyPaymentCommand";
import { InitiatePaymentCommand } from "../../features/booking-page/type/InitiatePaymentCommand";
@Injectable({
  providedIn: 'root',
})
export class Apicommuncation {
  constructor(private http: HttpClient) { }


  private api = "https://localhost:7119/api/"
  
  Validation(loginDetails: any): Observable<any> {
    return this.http.post(this.api + 'Auth/validate-login-Employee', loginDetails, { withCredentials: true });
  }
  filtering(FilterQuery: any): Observable<any> {
    const params = new HttpParams({ fromObject: FilterQuery })
    return this.http.get(this.api + 'Hotel/filter', { params: params });
  }
  getAllEmployee(): Observable<any> {
    const token = sessionStorage.getItem('JwtToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    console.log(headers)
    return this.http.get(this.api + 'Employee', { headers });
  }
  getAllHotel(): Observable<any> {
  
     
    return this.http.get(this.api + 'Hotel');
  }
getFilteredHotelRooms(serach:any):Observable<any>{
  return this.http.post(this.api+"Room/room",serach)
}
  getRefershToken(): Observable<any> {
    
    return this.http.post(this.api + 'auth/refresh-token',{}, { withCredentials: true }  );
    
  }
  AddHotel(formData: FormData): Observable<any> {
    return this.http.post(this.api + 'Hotel', formData)
  }
  DeleteHotel(id: number): Observable<any> {
    return this.http.delete(`${this.api}Hotel/${id}`);
  }
  UpdateHotel(id: number, formData: FormData): Observable<any> {
    return this.http.patch(`${this.api}Hotel/${id}`, formData);
  }
GetAllRoomsByHotelId(HotelId: number): Observable<any> {
    return this.http.get(`${this.api}Room/hotel/${HotelId}`);
  }
getroomtypesbyId(id: number): Observable<any> {
    return this.http.get(`${this.api}RoomType/${id}`);
}
GetRoomById(id:number):Observable<any>{
  return this.http.get(`${this.api}Room/${id}`)
}
getAllRoomType():Observable<any>{
   return this.http.get(`${this.api}RoomType`);
}
DeleteRoom(id:number):Observable<any>{
  return this.http.delete(`${this.api}Room/${id}`)
}
UpdateRoom(id:number,formData:FormData):Observable<any>{
  return this.http.patch(`${this.api}Room/${id}`,formData)
}
AddRoom(formData:FormData):Observable<any>{
return this.http.post(`${this.api}Room`,formData)
}
getAllBooking():Observable<any>{
return this.http.get(`${this.api}Booking`);
}

DeleteRoomType(id:number):Observable<any>{
return this.http.delete(`${this.api}RoomType/${id}`,)
}
AddRoomType(formData:RoomTypeForAddData):Observable<any>{
  return this.http.post(`${this.api}RoomType`,formData);
}
UpdateRoomType(id:number,formData:RoomType):Observable<any>{
  return this.http.patch(`${this.api}RoomType/${id}`,formData)
}

  initiatePayment(command: InitiatePaymentCommand): Observable<PaymentOrderResponse> {
    return this.http.post<PaymentOrderResponse>(`${this.api}payment/initiate`, command);
  }


 
  verifyPayment(command: VerifyPaymentCommand): Observable<any> {
    return this.http.post<any>(`${this.api}payment/verify`, command);
  }

getBookingById(id: number): Observable<BookingDetails> {
  return this.http.get<BookingDetails>(`${this.api}/${id}`);
}

bookRoom(bookingPayload:any):Observable<any>{
return this.http.post(`${this.api}`,bookingPayload)
}
CustomerAuthLogin(loginDetails:any):Observable<any>{
return this.http.post(`${this.api}Auth/validate-login-Customer`,loginDetails, { withCredentials: true })
}
SignUpUser(SignUp:FormData):Observable<any>{
  return this.http.post(`${this.api}Customer`,SignUp);
}
GetUserDataById(id:number):Observable<any>{
  return this.http.get(`${this.api}Customer/${id}`);
}
GetBookingDataByUserID(id:number):Observable<any>{
  return this.http.get(`${this.api}Booking/GetByCustomer${id}`)
}
getAllPayment():Observable<any>{
  return this.http.get(`${this.api}Payment`);
}
getAllCustomer():Observable<any>{
  return this.http.get(`${this.api}Customer`);
}
}