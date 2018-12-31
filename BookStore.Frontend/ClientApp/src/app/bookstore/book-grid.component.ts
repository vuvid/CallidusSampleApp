import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';

import { BookStoreService } from './bookstore.service';

@Component({
  selector: 'app-book-grid',
  templateUrl: './book-grid.component.html',
  styleUrls: ['./book-grid.component.css']
})

export class BookGridComponent implements OnInit {
  @Output() recordDeleted = new EventEmitter<any>();
  @Output() editClicked = new EventEmitter<any>();

  @Input() public books: Array<any>;

  constructor() {
  }

  public deleteRecord(event) {
    this.recordDeleted.emit(event);
  }

  public editRecord(event) {
    const clonedRecord = Object.assign({}, event);
    this.editClicked.emit(clonedRecord);
  }

  ngOnInit(): void {
  }
}
