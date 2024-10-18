import { Component } from '@angular/core';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { MainComponent } from './main/main.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss',
  imports: [HeaderComponent, MainComponent, FooterComponent],
})
export class LayoutComponent {}
