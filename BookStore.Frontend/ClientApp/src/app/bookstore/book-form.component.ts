import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { Author } from './models/author';

import { BookStoreService } from './bookstore.service';
import { Book } from './models/book';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html'
})

export class BookFormComponent implements OnInit {
  public authors: Array<Author> = [];
  @Input() public book: Book;
  @Output() bookCreated = new EventEmitter<any>();
  @Output() resetClicked = new EventEmitter<any>();

  constructor(private bookStoreService: BookStoreService) {
  }

  ngOnInit(): void {
    this.bookStoreService.getAuthors().subscribe(
      (data: any) => { this.authors = data },
      error => console.error(error)
    );
  }

  public save = function (event) {
    this.bookCreated.emit(this.book);
  };

  public reset() {
    this.resetClicked.emit();
  }

  public compareAuthor = (a1: Author, a2: Author): boolean => {
    return a1.id == a2.id;
  }

}
