import { HttpClient } from "@angular/common/http";
import{ Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "./auth.service";
import { CustomerDto } from "./dto/customer-dto";

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = 'https://localhost:44391/api/Customer'; // Backend API URL'niz

  constructor(private http: HttpClient, private authService: AuthService) { }

  // READ (Query): Tüm müşterileri getir
  getCustomers(): Observable<any> {
    let headers = this.authService.setTokentoHeader();
    const userId = this.authService.getCurrentUser();
    if (!userId) {
      throw new Error('Kullanıcı ID bulunamadı. Lütfen giriş yapın.');
    }
    // Kullanıcı ID'sini header'a ekle
    if (headers) {
      headers = headers.set('userId', userId.toString());
    }
    const httpOptions = headers ? { headers: headers } : {};
    return this.http.get<any>(`${this.apiUrl}/getcustomers`,{headers: httpOptions.headers});
  }

  // READ (Query): Belirli bir müşteriyi ID ile getir
  getCustomerById(customerId: number): Observable<CustomerDto> {
    const headers = this.authService.setTokentoHeader();
    const httpOptions = headers ? { headers: headers } : {};
    return this.http.post<CustomerDto>(`${this.apiUrl}`, customerId , {
      headers: httpOptions.headers
    });
  }
}