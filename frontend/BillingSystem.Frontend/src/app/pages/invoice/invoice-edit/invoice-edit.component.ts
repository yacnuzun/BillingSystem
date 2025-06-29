import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router'; // Rota ve yönlendirme için import ettik

import { InvoiceService } from '../../../core/invoice.service';
import { InvoiceUpdateRequestDto, InvoiceDetailResponseDto, InvoiceLineDto } from '../../../core/dto/invoice-dtos';

@Component({
  selector: 'app-invoice-edit',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './invoice-edit.component.html', // Ortak HTML kullanıyoruz
  styleUrls: ['./invoice-edit.component.css']
})
export class InvoiceEditComponent implements OnInit {
  invoice: InvoiceUpdateRequestDto = { // Update DTO tipinde başlatıyoruz
    invoiceId: 0, // Bu ID rota parametresinden gelecek
    customerId: 0,
    invoiceNumber: '',
    invoiceDate: '',
    totalAmount: 0,
    userId: 0,
    invoiceLines: []
  };

  invoiceId: number | null = null; // Düzenlenecek faturanın ID'si

  constructor(
    private invoiceService: InvoiceService,
    private route: ActivatedRoute, // Rota parametrelerini almak için
    private router: Router // Yönlendirme yapmak için
  ) {}

  ngOnInit(): void {
    // Rota parametrelerinden fatura ID'sini al
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.invoiceId = +id; // String'i number'a çevir
        this.loadInvoice(this.invoiceId); // Faturayı yükle
      } else {
        // ID yoksa veya geçersizse hata verip yönlendirebiliriz
        alert('Düzenlenecek fatura ID\'si bulunamadı.');
        this.router.navigate(['/invoices/list']);
      }
    });
  }

  loadInvoice(id: number): void {
    this.invoiceService.getInvoiceById(id).subscribe({
      next: (data: InvoiceDetailResponseDto)  => {
        // Backend'den gelen InvoiceDetailResponseDto'yu,
        // formu doldurmak için InvoiceUpdateRequestDto'ya dönüştürüyoruz.
        this.invoice = {
          invoiceId: data.invoiceId,
          customerId: data.customerId,
          invoiceNumber: data.invoiceNumber,
          invoiceDate: this.formatDate(data.invoiceDate) ,
          totalAmount: data.totalAmount, // Backend'den gelen toplamı kullan
          userId: data.userId,
          invoiceLines: data.invoiceLines.map(line => ({ // Backend'deki InvoiceItemDto'yu InvoiceLineDto'ya dönüştür
            invoiceLineId: line.invoiceLineId, // Mevcut satırların ID'leri korunur
            itemName: line.itemName,
            quantity: line.quantity,
            price: line.price
          }))
        };
        console.log('Yüklenen fatura:', this.invoice);
        console.log('Yüklenen fatura satırları:', this.invoice.invoiceLines);
        this.calculateTotal(); // Veri yüklendikten sonra toplamı yeniden hesapla (doğrulama amaçlı)
      },
      error: (err) => {
        console.error('Fatura yüklenirken hata oluştu:', err);
        alert('Fatura yüklenemedi! Lütfen konsolu kontrol edin.');
        this.router.navigate(['/invoices/list']); // Hata durumunda listeye yönlendir
      }
    });
  }
formatDate(date: any): string {
  const d = new Date(date);
  const month = (d.getMonth() + 1).toString().padStart(2, '0');
  const day = d.getDate().toString().padStart(2, '0');
  const year = d.getFullYear();
  return `${year}-${month}-${day}`;
}
  addLine(): void {
    // Yeni eklenen satıra id: 0 veriyoruz. Backend bunu yeni kayıt olarak algılayacak.
    this.invoice.invoiceLines.push({ invoiceLineId: 0, itemName: '', quantity: 1, price: 0 });
    this.calculateTotal();
  }

  removeLine(index: number): void {
    // Eğer backend'iniz silinen satırları (InvoiceLineId'si olanları) ayrı bir şekilde işliyorsa
    // (örn. bir 'deletedInvoiceLineIds' listesi bekliyorsa), o mantığı buraya eklemeniz gerekebilir.
    // Şu an için sadece frontend'deki diziden siliyoruz.
    this.invoice.invoiceLines.splice(index, 1);
    this.calculateTotal();
  }

  calculateTotal(): void {
    this.invoice.totalAmount = this.invoice.invoiceLines.reduce((sum, line) => sum + (line.quantity * line.price), 0);
  }

  onSubmit(): void {
    if (this.invoiceId === null || this.invoice.invoiceId === 0) {
      alert('Hata: Düzenlenecek fatura ID\'si bulunamadı.');
      return;
    }
    this.calculateTotal(); // Göndermeden önce son kez toplamı hesapla

    this.invoiceService.updateInvoice(this.invoice).subscribe({ // Sadece command objesini gönderiyoruz
      next: (response) => {
        if (response.success) {
          alert('Fatura başarıyla güncellendi!');
          this.router.navigate(['/invoices/list']); // Başarılı güncelleme sonrası listeye yönlendirme
        } else {
          alert(`Fatura güncellenemedi: ${response.message}`);
        }
      },
      error: (err) => {
        console.error('Fatura güncellenirken hata oluştu:', err);
        alert('Fatura güncellenemedi! Lütfen konsolu kontrol edin.');
      }
    });
  }
}