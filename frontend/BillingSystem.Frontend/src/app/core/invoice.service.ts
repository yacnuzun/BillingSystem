import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  InvoiceDeleteRequestDto,
  InvoiceDeleteResponseDto,
  InvoiceListResponseDto,
  InvoiceDetailResponseDto,
  InvoiceCreateRequestDto,
  InvoiceUpdateRequestDto,
  InvoiceRequestResult
} from './dto/invoice-dtos'; // Tanımladığımız DTO'ları import et

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private apiUrl = 'https://localhost:44326/api/Invoice'; // Backend API URL'niz

  constructor(private http: HttpClient) { }

  // READ (Query): Tüm faturaları getir
  getInvoices(): Observable<any> {
    return this.http.get<InvoiceRequestResult<InvoiceListResponseDto[]>>(this.apiUrl+"/getivoices");
  }

  // READ (Query): Belirli bir faturayı ID ile getir
  getInvoiceById(invoiceId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`,{
  params: { id: invoiceId.toString() }
});
  }

  // CREATE (Command): Yeni fatura oluştur
  createInvoice(command: InvoiceCreateRequestDto): Observable<any> {
    // Backend'den fatura ID'si veya başarı mesajı dönebilir
    return this.http.post<any>(this.apiUrl, command);
  }

  // UPDATE (Command): Fatura güncelle
  updateInvoice(command: InvoiceUpdateRequestDto): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}`, command);
  }

  // DELETE (Command): Fatura sil
  deleteInvoice(request: InvoiceDeleteRequestDto): Observable<any> {
    // Delete isteği body almaz, ancak siz request object olarak göndermek istiyorsanız
    // .NET tarafında [FromBody] ile almanız gerekebilir, bu HTTP DELETE standartlarına tam uymaz.
    // En iyi yöntem URL parametresi veya Query String kullanmaktır.
    // Örnek: DELETE api/invoices/1
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      body: request // İşte buraya request object'ini doğrudan atıyorsunuz
    };
    return this.http.delete<InvoiceDeleteResponseDto>(`${this.apiUrl}`, httpOptions);
  }
}