export interface BookingDisplayDataDto {
  bookingId: number;
  checkInDate: string;   
  checkOutDate: string;  
  name: string;
  address: string;
  phoneNumber: string;
  roomNumber: string;
  pricePerNight: string;
  path:string;
  status:BookingStatus;
}
export enum BookingStatus {
   
 
     Pending=1,
     Confirmed=2,
     Cancelled=3,
     Completed=4
 
}