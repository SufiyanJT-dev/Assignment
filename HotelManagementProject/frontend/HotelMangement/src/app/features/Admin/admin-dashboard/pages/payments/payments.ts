import { Component, NgModule } from '@angular/core';
import { Apicommuncation } from '../../../../../shared/Api/apicommuncation';
import { CommonModule, formatDate } from '@angular/common';
import { FormsModule, NgModel } from '@angular/forms';

@Component({
  selector: 'app-payments',
  templateUrl: './payments.html',
  styleUrls: ['./payments.scss'],
  imports:[CommonModule,
    FormsModule]
})
export class Payments {
  payments: any[] = [];
  filtered: any[] = [];
  search = '';
  selected: any = null;
  loading = false;
  error: string | null = null;

  constructor(private api: Apicommuncation) {}

  ngOnInit() {
    this.loadPayments();
  }

  loadPayments() {
    this.loading = true;
    this.error = null;
    this.api.getAllPayment().subscribe({
      next: (value: any[]) => {
        this.payments = Array.isArray(value) ? value : [];
        this.filtered = this.payments.slice().reverse();
        this.loading = false;
      },
      error: (err) => {
        console.log(err);
        this.error = 'Failed to load payments';
        this.loading = false;
      },
    });
  }

  applySearch() {
    const q = this.search.trim().toLowerCase();
    if (!q) {
      this.filtered = this.payments.slice().reverse();
      return;
    }
    this.filtered = this.payments.filter(p =>
      String(p.id).includes(q) ||
      String(p.bookingId ?? '').toLowerCase().includes(q) ||
      String(p.razorpayOrderId ?? '').toLowerCase().includes(q) ||
      String(p.razorpayPaymentId ?? '').toLowerCase().includes(q) ||
      String(p.amount).includes(q)
    ).reverse();
  }

  selectPayment(p: any) {
    this.selected = p;
  }

  clearSelection() {
    this.selected = null;
  }

  // small helper to present method name
  methodLabel(m: number | string) {
    switch (String(m)) {
      case '1': return 'Cash';
      case '2': return 'Card';
      case '3': return 'Razorpay';
      case '4': return 'UPI';
      default: return 'Other';
    }
  }

  // format ISO date into readable format
  prettyDate(iso?: string) {
    if (!iso) return '-';
    try {
      return formatDate(iso, 'dd MMM yyyy, HH:mm', 'en-IN');
    } catch {
      return iso;
    }
  }
}
