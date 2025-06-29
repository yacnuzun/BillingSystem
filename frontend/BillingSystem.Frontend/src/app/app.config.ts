import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http'; // <-- YENİ EKLENTİ

import { routes } from './app.routes'; // Rotelar hala buradan gelecek
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes), // Rotelar burada kullanılmaya devam edecek
    provideClientHydration(withEventReplay()),
    provideHttpClient() // <-- HttpClient sağlayıcısını buraya ekliyoruz
  ]
};