import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router'; // Sayfa içeriğini yüklemek için
import { SidebarComponent } from '../../shared/sidebar/sidebar.component'; // Sidebar component'i import et
import { HeaderComponent } from '../../shared/header/header.component';   // Header component'i import et
import { FooterComponent } from '../../shared/footer/footer.component';   // Footer component'i import et

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    SidebarComponent, // Bileşenleri burada kullanmak için ekle
    HeaderComponent,
    FooterComponent
  ],
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent { }