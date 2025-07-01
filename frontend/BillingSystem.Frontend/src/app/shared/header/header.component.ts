import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/auth.service';
import { CustomerService } from '../../core/customer.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  userFullName: string = '';

  constructor(private authService: AuthService, private customerService: CustomerService) {
    // Kullanıcı bilgilerini yükle
    this.loadUser();
  }
  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(): void {
    const user = this.authService.getCurrentUser(); // Kullanıcı ID'sini al
    if (user) {
      const userDetail = this.customerService.getCustomerById(user); // Kullanıcı ID'sini al
      userDetail.subscribe({
        next: (data) => {
          const parsedUser = data; // Kullanıcı verisini al
          this.userFullName = parsedUser.title || 'Bilinmeyen Kullanıcı'; // veya customerTitle
          console.log('Kullanıcı bilgileri yüklendi:', this.userFullName);
        },
        error: (err) => {
          console.error('Kullanıcı bilgileri yüklenirken hata oluştu:', err);
          this.userFullName = 'Bilinmeyen Kullanıcı'; // Hata durumunda varsayılan isim
          console.log('Kullanıcı bilgileri yüklenemedi, varsayılan isim kullanıldı:', this.userFullName);
        }
      });
    } else {
      this.userFullName = 'Bilinmeyen Kullanıcı'; // veya customerTitle
    }
  }

  outUser():void{
    this.authService.logout();
    console.log('Kullanıcı çıkışı yapıldı.');
    window.location.reload(); // Sayfayı yenile
  }
}