import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router'; // routerLink kullanmak için

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink], // RouterLink'i ekle
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    // Eğer sidebar'ın açma/kapama mekanizması jQuery ile yapılmışsa,
    // burada Angular'da yeniden implemente etmeniz veya direkt olarak CSS ile kontrol etmeniz gerekebilir.
    // Örneğin, bir event listener ekleyerek sınıf değiştirebilirsiniz.
  }
}