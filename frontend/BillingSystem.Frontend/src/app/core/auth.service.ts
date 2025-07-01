import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoginDto } from './dto/user'; // LoginDto arayüzünü içe aktar
import { json } from 'stream/consumers';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44335/api/Account';
  private tokenKey = 'auth_token';
  private currentUser = 'currentUser'; // Kullanıcı bilgilerini saklamak için
  private expiration = 'expiration_date';

  constructor(private http: HttpClient) { }

  login(credentials: { userName: string; password: string }): Observable<any> {

    return this.http.post<LoginDto>(`${this.apiUrl}`, credentials).pipe(
      tap(response => {
        if (response && response.token.token && response.userId) {
          localStorage.setItem(this.tokenKey, JSON.stringify(response.token.token));
          localStorage.setItem(this.currentUser, JSON.stringify(response.userId));
          localStorage.setItem(this.expiration, JSON.stringify(response.token.expiration))
        }
      })
    );
  }

  getCurrentUser(): number | null {
    const user = localStorage.getItem(this.currentUser);
    return user ? JSON.parse(user) : null;
  }

  setTokentoHeader(): HttpHeaders | null {
    let token = localStorage.getItem('auth_token'); // Veya token'ınızı sakladığınız herhangi bir yerden alın
    var headerProperty = new HttpHeaders();

    if (token) {
      token = token.trim();
      if (token.startsWith('"') && token.endsWith('"')) {
        token = token.substring(1, token.length - 1);
      }
      token = token.replace(/\//g, '');
      return headerProperty.set("Authorization", "Bearer " + token);
    } else {
      return null; // Token yoksa null döndür
    }
  }
  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.expiration);
    localStorage.removeItem(this.currentUser);
  }

  getToken(): string | null {
    if (typeof window !== 'undefined' && window.localStorage) {
      return localStorage.getItem(this.tokenKey);
    }
    return null;
  }

  controlExpirationDate(): boolean {
    if (typeof window !== 'undefined' && window.localStorage) {
      const expiration = localStorage.getItem(this.expiration)?.replace("\"", "").replace("\"", "");
      if (!expiration) return false;

      const cleanedExpiration = expiration.replace(/(\.\d{3})\d*(\+.*)/, '$1$2');

      const expirationTime = Date.parse(cleanedExpiration);
      if (isNaN(expirationTime)) {
        console.warn('Geçersiz expiration formatı:', expiration);
        return false;
      }

      return expirationTime > Date.now(); // expiration geçmediyse true
    }

    return false;
  }

  isLoggedIn(): boolean {
    const isToken = !!this.getToken();
    const isExpiration = this.controlExpirationDate();
    return isToken && isExpiration;
  }
}