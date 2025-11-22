import { RoomType } from "./RoomType";

export interface Room {
  Bookings: any | null;      
  Hotel: any | null;          
  hotelId: number;
  id: number;
  pricePerNight: number;
  roomNumber: string;
  
  roomTypeId: number;
  status: number;
}
