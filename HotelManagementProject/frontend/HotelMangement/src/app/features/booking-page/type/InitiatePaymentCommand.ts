export interface InitiatePaymentCommand {
  customerId: number;
  roomId: number;
  checkInDate: string;
  checkOutDate: string;
}
