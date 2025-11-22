
export interface BookingDetails {
  id?: number;
  customerId: number;
  roomId: number;
  checkInDate: string;
  checkOutDate: string;
  status: number;
  totalAmount: number;
}