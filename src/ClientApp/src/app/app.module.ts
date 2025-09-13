import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterOutlet } from '@angular/router';
import { MessageService, SharedModule } from 'primeng/api';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './components/layout/layout.component';
import { CoreModule } from './core/core.module';
import { providePrimeNG } from 'primeng/config';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { defaultPreset } from './theming/theme-preset';
import { authInterceptor } from './core/interceptors/auth-interceptor';
import { Toast } from 'primeng/toast';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    CoreModule,
    SharedModule,
    AppRoutingModule,
    LayoutComponent,
    RouterOutlet,
    Toast,
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(withFetch(), withInterceptors([authInterceptor])),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: defaultPreset,
        options: {
          darkModeSelector: false || 'none',
        },
      },
      ripple: true,
    }),
    MessageService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor() {}
}
