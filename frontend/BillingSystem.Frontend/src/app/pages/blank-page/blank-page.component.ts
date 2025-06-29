import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blank-page',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h1 class="h3 mb-4 text-gray-800">Blank Page</h1>
    <p>Bu sizin boş sayfanızın içeriğidir. Ana layout'unuzun içinde görünecektir.</p>
    <div class="card shadow mb-4">
      <div class="card-header py-3">
        <h6 class="m-0 font-weight-bold text-primary">Örnek Kart</h6>
      </div>
      <div class="card-body">
        Sayfa içeriği burada yer alacak.
      </div>
    </div>
  `,
  styleUrls: ['./blank-page.component.css']
})
export class BlankPageComponent { }