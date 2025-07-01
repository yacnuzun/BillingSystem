import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoiceService } from '../../../core/invoice.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms'; // Form işlemleri için gerekli
import { InvoiceListResponseDto, InvoiceDeleteRequestDto, InvoiceDeleteResponseDto } from '../../../core/dto/invoice-dtos'; // Tanımladığımız DTO'ları import et

let i: number = 0;

@Component({
  selector: 'app-invoice-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})

export class InvoicePageComponent implements OnInit {
  invoices: InvoiceListResponseDto[] = [];
  invoiceslenght: number = 0;
  loading = true;
  filterStartDate: string = '';
  filterEndDate: string = '';
  errorMessage: string | null = null;

  constructor(private invoiceService: InvoiceService,
    private router: Router) { }

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices(): void {
    this.loading = true;
    this.errorMessage = null;
    this.invoiceService.getInvoices().subscribe({
      next: (data) => {
        this.invoices = data.data.invoices;
        this.loading = false;
      },
      error: (err) => {
        console.error('Fatura listesi yüklenirken hata oluştu:', err);
        this.errorMessage = 'Fatura listesi yüklenemedi. Lütfen daha sonra tekrar deneyin.';
        this.loading = false;
      }
    });
  }

  filterInvoicesByDate(): void {
    if (!this.filterStartDate || !this.filterEndDate) {
      alert('Lütfen başlangıç ve bitiş tarihlerini girin.');
      return;
    }

    this.loading = true;
    this.errorMessage = null;

    this.invoiceService.getInvoicewithDareRange(this.filterStartDate, this.filterEndDate).subscribe({
      next: (data) => {
        this.invoices = data.data.invoices;
        this.loading = false;
      },
      error: (err) => {
        console.error('Tarihe göre fatura filtreleme hatası:', err);
        this.errorMessage = 'Faturalar filtrelenemedi. Lütfen daha sonra tekrar deneyin.';
        this.loading = false;
      }
    });
  }

  deleteInvoice(invoiceId: number): void {
    if (confirm('Bu faturayı silmek istediğinizden emin misiniz?')) {
      const deleteRequest: InvoiceDeleteRequestDto = { invoiceId: invoiceId };
      this.invoiceService.deleteInvoice(deleteRequest).subscribe({
        next: (response: InvoiceDeleteResponseDto) => {
          if (response.success) {
            alert(response.message || 'Fatura başarıyla silindi.');
            this.loadInvoices(); // Listeyi güncelle
          } else {
            alert(response.message || 'Fatura silinemedi.');
          }
        },
        error: (err) => {
          console.error('Fatura silinirken hata oluştu:', err);
          alert('Fatura silinirken bir sorun oluştu. Lütfen konsolu kontrol edin.');
        }
      });
    }
  }

  // Edit ve Create işlemleri için yönlendirmeler veya modal açma fonksiyonları buraya eklenebilir.
  // Örneğin:
  editInvoice(invoiceId: number): void {

    // Düzenleme sayfasına yönlendir veya bir modal aç
    console.log('Fatura düzenle:', invoiceId);
    this.router.navigate(['/invoice-edit', invoiceId]);
  }

  createNewInvoice(): void {
    // Yeni fatura oluşturma sayfasına yönlendir veya bir modal aç
    console.log('Yeni fatura oluştur');
    this.router.navigate(['/invoice-add']);
  }
}