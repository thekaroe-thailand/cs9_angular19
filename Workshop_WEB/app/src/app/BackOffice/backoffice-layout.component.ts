import { Component, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from './sidebar/sidebar.component';

@Component({
    selector: 'app-backoffice-layout',
    standalone: true,
    imports: [RouterModule, SidebarComponent],
    templateUrl: './backoffice-layout.component.html'
})
export class BackofficeLayoutComponent {
    @ViewChild(SidebarComponent) sidebarComponent!: SidebarComponent;

    refreshSidebar() {
        this.sidebarComponent.fetchData();
    }
}