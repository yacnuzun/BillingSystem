import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../../core/customer.service';
import { CustomerDto } from '../../../core/dto/customer-dto';



@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [CommonModule, FormsModule],
    templateUrl: './customer.component.html',
    styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  customers: CustomerDto[] = []; // Müşteri listesini tutar

  @Input() selectedCustomerId: number | null = null;
  @Output() customerSelected = new EventEmitter<number>();

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe({
      next: (res) => {
        if (res.success && res.data) {
          this.customers = res.data.customers;
          console.log('Müşteri listesi alındı:', this.customers);
        }
      },
      error: (err) => {
        console.error('Müşteri listesi alınamadı', err);
      }
    });
  }

  onSelectionChange(customerId: string): void {
    const selectedId = parseInt(customerId, 10);
    this.customerSelected.emit(selectedId);
  }
}