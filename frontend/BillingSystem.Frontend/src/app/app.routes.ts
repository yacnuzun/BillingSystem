import { Routes } from '@angular/router';

// Layout ve Sayfa Bileşenlerini import edin
import { AuthLayoutComponent } from './layout/auth-layout/auth-layout.component';
import { LoginComponent } from './pages/login/login.component';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { InvoicePageComponent } from './pages/invoice/invoice-list/invoice-list.component';
import { BlankPageComponent } from './pages/blank-page/blank-page.component';
import { InvoiceCreateComponent } from './pages/invoice/invoice-add/invoice-create.component';
import { InvoiceEditComponent } from './pages/invoice/invoice-edit/invoice-edit.component';
import { RenderMode } from '@angular/ssr';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent // Kendi login component'inizin yolu
  },
  // Ana layoutu kullanan rotalar
  {
    path: '', // Boş path, ana layout'u varsayılan yapar
    component: MainLayoutComponent,
    children: [
      { path: 'dashboard', component: BlankPageComponent }, // Örnek olarak dashboard da şimdilik blank page olabilir
      { path: 'invoices', component: InvoicePageComponent }, // Fatura sayfası
      { path: 'blank', component: BlankPageComponent }, // Blank sayfası
      { path: 'invoice-add', component: InvoiceCreateComponent },
      {
        path: 'invoice-edit/:id', component: InvoiceEditComponent,
        data: {
          renderMode: 'no-prerender' 
        }
      }, // Fatura düzenleme sayfası, ID parametresi alır
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' } // Varsayılan yönlendirme
    ]
  },
  // Tanımsız rotalar için 404 (isteğe bağlı)
  { path: '**', redirectTo: 'dashboard' } // Bilinmeyen tüm rotaları dashboard'a yönlendir
];