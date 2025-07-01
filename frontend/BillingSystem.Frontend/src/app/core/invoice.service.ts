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
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private apiUrl = 'https://localhost:44326/api/Invoice'; // Backend API URL'niz
  constructor(private http: HttpClient,
    private authService: AuthService
  ) { }
  private token?: HttpHeaders | null = null;

  // READ (Query): Tüm faturaları getir
  getInvoices(): Observable<any> {
    const headers = this.authService.setTokentoHeader();
    const httpOptions = headers ? { headers: headers } : {};
    return this.http.get<InvoiceRequestResult<InvoiceListResponseDto[]>>(this.apiUrl + "/getivoices",
        { headers: httpOptions.headers });
  }

  getInvoicewithDareRange(startDate: string, endDate: string): Observable<any>{
    const headers = this.authService.setTokentoHeader();
    const httpOptions = headers ? {headers: headers, params: {startDate: startDate, endDate: endDate}} : {params: {startDate: startDate, endDate: endDate}};
    return this.http.get<InvoiceRequestResult<InvoiceListResponseDto[]>>(this.apiUrl+'/getlist', 
      { headers: httpOptions.headers , params: httpOptions.params });
  }

  // READ (Query): Belirli bir faturayı ID ile getir
  getInvoiceById(invoiceId: number): Observable<any> {
    const headers = this.authService.setTokentoHeader();
    const httpOptions = headers ? { headers: headers , params: { id: invoiceId.toString() }} : {params: { id: invoiceId.toString()}};
    return this.http.get<any>(`${this.apiUrl}`,{ headers: httpOptions.headers , params: httpOptions.params });
  }

  // CREATE (Command): Yeni fatura oluştur
  createInvoice(command: InvoiceCreateRequestDto): Observable<any> {
    const headers = this.authService.setTokentoHeader();
    const httpOptions = headers ? { headers: headers } : {};
    return this.http.post<any>(this.apiUrl, command, {headers: httpOptions.headers});
  }

  // UPDATE (Command): Fatura güncelle
  updateInvoice(command: InvoiceUpdateRequestDto): Observable<any> {
    const headers = this.authService.setTokentoHeader();
    const httpOptions = headers ? { headers: headers } : {};
    return this.http.put<any>(`${this.apiUrl}`, command, {headers: httpOptions.headers});
  }

  // DELETE (Command): Fatura sil
  deleteInvoice(request: InvoiceDeleteRequestDto): Observable<any> {
    const headers = this.authService.setTokentoHeader();
    const httpOptions = headers ? { headers: headers , body: request} : {body: request};
    return this.http.delete<InvoiceDeleteResponseDto>(`${this.apiUrl}`, httpOptions);
  }
}