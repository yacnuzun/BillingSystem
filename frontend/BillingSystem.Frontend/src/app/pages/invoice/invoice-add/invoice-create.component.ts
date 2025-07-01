import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // ngFor, ngIf gibi direktifler için gerekli
import { FormsModule } from '@angular/forms';   // ngModel gibi form direktifleri için gerekli
import { CustomerComponent } from '../../../shared/component/customer/customer.component'; // Müşteri seçimi için
import { InvoiceService } from '../../../core/invoice.service';
import { InvoiceCreateRequestDto } from '../../../core/dto/invoice-dtos';
import { AuthService } from '../../../core/auth.service';

@Component({
  selector: 'app-invoice-create',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    CustomerComponent
  ],
  templateUrl: './invoice-create.component.html',
  styleUrls: ['./invoice-create.component.css']
})
export class InvoiceCreateComponent {
  invoice: InvoiceCreateRequestDto = {
    customerId: 0,
    invoiceNumber: '',
    invoiceDate: new Date().toISOString().substring(0, 10),
    totalAmount: 0,
    userId: 0,
    invoiceLines: [
      { itemName: '', quantity: 1, price: 0 }
    ],
    customerName: '',
    customerAddress: '',
    issueDate: new Date().toISOString().substring(0, 10),
    dueDate: '',
    items: []
  };



  onCustomerSelected(customerId: number) {
    this.invoice.customerId = customerId;
  }

  constructor(private invoiceService: InvoiceService, private authService: AuthService) { }
  
  ngOnInit() {
    // Kullanıcı bilgilerini al ve faturaya ata
    this.getUser();
  }
  
  getUser() {
    const value = this.authService.getCurrentUser();
    if (value !== null) {
      const user: number = value// Kullanıcı ID'sini al ve faturaya ata
      this.invoice.userId = user; // Kullanıcı ID'sini faturaya ata
    } else {
      alert("Lütfen giriş yapın!");
    }
  }

  // Fatura satırı ekleme
  addLine() {
    this.invoice.invoiceLines.push({ itemName: '', quantity: 1, price: 0 });
  }

  // Fatura satırı silme
  removeLine(index: number) {
    this.invoice.invoiceLines.splice(index, 1);
  }

  // Toplam tutarı hesaplama
  calculateTotal() {
    this.invoice.totalAmount = this.invoice.invoiceLines.reduce((sum, line) => sum + (line.quantity * line.price), 0);
  }

  // Form gönderimi
  onSubmit() {
    // Göndermeden önce toplam tutarı tekrar hesapla
    this.calculateTotal();

    // issueDate ve dueDate'in de doğru formatta olduğundan emin olun
    // Örn: this.invoice.issueDate = new Date().toISOString().substring(0, 10);

    this.invoiceService.createInvoice(this.invoice).subscribe({
      next: () => alert('Fatura başarıyla oluşturuldu!'),
      error: (err) => {
        console.error('Fatura oluşturulurken hata oluştu:', err);
        alert('Fatura oluşturulamadı! Lütfen konsolu kontrol edin.');
      }
    });
  }
}