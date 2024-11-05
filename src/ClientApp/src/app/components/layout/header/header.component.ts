import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CategoriesService } from '../../../shared/services/categories.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  imports: [RouterLink, ButtonModule, InputTextModule, RouterModule],
  providers: [CategoriesService],
})
export class HeaderComponent {
  constructor() {}
}
