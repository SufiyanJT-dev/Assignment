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


  selectPayment(p: any) {
    this.selected = p;
  }

  clearSelection() {
    this.selected = null;
  }

  methodLabel(m: number | string) {
    switch (String(m)) {
      case '1': return 'Cash';
      case '2': return 'Card';
      case '3': return 'Razorpay';
      case '4': return 'UPI';
      default: return 'Other';
    }
  }

  prettyDate(iso?: string) {
    if (!iso) return '-';
    try {
      return formatDate(iso, 'dd MMM yyyy, HH:mm', 'en-IN');
    } catch {
      return iso;
    }
  }
}
