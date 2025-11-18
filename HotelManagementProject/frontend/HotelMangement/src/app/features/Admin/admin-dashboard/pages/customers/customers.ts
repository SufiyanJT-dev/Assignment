import { Component, OnInit } from '@angular/core';
import { Apicommuncation } from '../../../../../shared/Api/apicommuncation';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
interface Customer {
  email: string;
  fullName: string;
  idProofNumber: string;
  phoneNumber: string;
  // add other fields returned by your API as needed
}

@Component({
  selector: 'app-customers',
  templateUrl: './customers.html',
  styleUrls: ['./customers.scss'],
  imports:[MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule]
})
export class Customers implements OnInit {
  customers: Customer[] = [];
  filtered: Customer[] = [];
  loading = false;
  error: string | null = null;
  search = '';

  constructor(private api: Apicommuncation) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.loading = true;
    this.error = null;
    this.api.getAllCustomer().subscribe({
      next: (value: any) => {
        // if API returns array directly
        this.customers = Array.isArray(value) ? value : (value?.data ?? []);
        this.filtered = [...this.customers];
        this.loading = false;
      },
      error: (err: any) => {
        this.error = 'Failed to load customers';
        console.error(err);
        this.loading = false;
      },
    });
  }

  onSearchChange(q: string): void {
    this.search = q.trim().toLowerCase();
    if (!this.search) {
      this.filtered = [...this.customers];
      return;
    }
    this.filtered = this.customers.filter(c =>
      (c.fullName || '').toLowerCase().includes(this.search) ||
      (c.email || '').toLowerCase().includes(this.search) ||
      (c.phoneNumber || '').toLowerCase().includes(this.search) ||
      (c.idProofNumber || '').toLowerCase().includes(this.search)
    );
  }
}
