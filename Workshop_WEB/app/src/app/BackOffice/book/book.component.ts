import { Component } from '@angular/core';
import { ModalComponent } from '../../Components/modal/modal.component';

@Component({
  selector: 'app-book',
  standalone: true,
  imports: [ModalComponent],
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})
export class BookComponent {
  isModalOpen = false;

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }
}
