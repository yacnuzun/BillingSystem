import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // ngFor, ngIf gibi direktifler için gerekli
import { FormsModule } from '@angular/forms';   // ngModel gibi form direktifleri için gerekli

import { InvoiceService } from '../../../core/invoice.service';
import { InvoiceCreateRequestDto } from '../../../core/dto/invoice-dtos';

@Component({
  selector: 'app-invoice-create',
  standalone: true, // Zaten doğru şekilde belirlenmiş
  imports: [
    CommonModule, // HTML'deki *ngFor, *ngIf gibi yapısal direktifler için
    FormsModule   // Input'larda [(ngModel)] kullanıyorsanız bu gerekli
    // Eğer Reactive Forms kullanıyorsanız ReactiveFormsModule de eklemeniz gerekir.
    // ReactiveFormsModule // Eğer Reactive Forms kullanıyorsanız
  ],
  templateUrl: './invoice-create.component.html',
  styleUrls: ['./invoice-create.component.css']
})
export class InvoiceCreateComponent {
  invoice: InvoiceCreateRequestDto = {
    customerId: 0,
    invoiceNumber: '',
    invoiceDate: new Date().toISOString().substring(0, 10), // Varsayılan tarih ayarı
    totalAmount: 0,
    userId: 0, // Bu değerin doğru bir şekilde atanması gerekecektir (örn. kimlik doğrulama servisinden)
    invoiceLines: [
      { itemName: '', quantity: 1, price: 0 }
    ],
    customerName: '',
    customerAddress: '',
    issueDate: new Date().toISOString().substring(0, 10), // Muhtemelen invoiceDate ile aynı veya benzer
    dueDate: '', // Bu değerin de atanması gerekecektir
    items: [] // Bu alan muhtemelen invoiceLines ile çakışıyor, kontrol etmelisiniz.
  };

  constructor(private invoiceService: InvoiceService) {}

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