import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css'
})
export class ModalComponent {
  @Input() title: string = '';
  @Input() isOpen: boolean = false;
  @Input() size: string = 'md';
  @Output() closeModal = new EventEmitter<void>();

  sizeValue: string = '';

  ngOnInit() {
    switch (this.size) {
      case 'md':
        this.sizeValue = '500px';
        break;
      case 'lg':
        this.sizeValue = '800px';
        break;
      case 'xl':
        this.sizeValue = '1000px';
        break;
    }
  }

  close() {
    this.isOpen = false;
    this.closeModal.emit();
  }
}
