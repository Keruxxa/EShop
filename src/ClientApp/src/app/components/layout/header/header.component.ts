import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CategoriesService } from '../../../shared/services/categories.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  imports: [RouterLink],
  providers: [CategoriesService],
})
export class HeaderComponent {
  constructor() {}
}
