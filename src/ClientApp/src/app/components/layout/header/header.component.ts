import { Component, OnDestroy } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { Subscription } from 'rxjs';
import { SelectListItem } from '../../../shared/models/select-list-item.model';
import { CategoriesService } from '../../../shared/services/categories.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  imports: [MenuModule, ButtonModule, RouterLink],
  providers: [CategoriesService],
})
export class HeaderComponent implements OnDestroy {
  public menuItems: MenuItem[] | undefined;
  public categories: SelectListItem<number>[] | undefined;
  public subscriptions!: Subscription;

  constructor(private readonly categoriesService: CategoriesService) {}

  ngOnDestroy(): void {
    this.subscriptions?.unsubscribe();
  }
}
