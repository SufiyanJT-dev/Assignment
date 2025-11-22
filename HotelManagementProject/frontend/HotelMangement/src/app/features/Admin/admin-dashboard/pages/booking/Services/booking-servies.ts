import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookingDetails } from '../type/BookingDetails';

@Injectable({
  providedIn: 'root',
})
export class BookingServies {
   constructor(private http: HttpClient) { }
  private api="https://localhost:7119/api/"
  GetBookinByRoomId(roomId:number):Observable<any>{
    return this.http.get(`${this.api}Booking/roomId${roomId}`);
  }
  
  createBooking(booking: BookingDetails): Observable<number> {
    return this.http.post<number>(`${this.api}Booking/`, booking);
  }
 
  updateBooking(id: number, booking: BookingDetails): Observable<boolean> {
    return this.http.patch<boolean>(`${this.api}Booking/${id}`, booking);
  }
  
 
  deleteBooking(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.api}Booking/${id}`);
  }
}
