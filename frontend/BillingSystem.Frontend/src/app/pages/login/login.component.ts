import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // FormsModule'ü import et
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth.service'; // AuthService'i import et

@Component({
  selector: 'app-login',
  standalone: true, // STANDALONE TRUE
  imports: [CommonModule, FormsModule], // Gerekli modülleri import et
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData = {
    userName: '',
    password: ''
  };
  rememberMe: boolean = false;

  constructor(private authService: AuthService, private router: Router) { }

  onLoginSubmit() {
  console.log('Login attempt with:', this.loginData);

  this.authService.login(this.loginData).subscribe({
    next: (response) => {
      console.log('Login successful', response);
      alert('Giriş başarılı!');
      // this.router.navigate(['/dashboard']);
    },
    error: (error) => {
      console.error('Login error', error);
      alert('Giriş başarısız! Lütfen bilgilerinizi kontrol edin.');
    }
  });
}
}