import { Component, Inject, OnInit } from '@angular/core';

import { BookStoreService } from './bookstore.service';
import { Book } from './models/book';

@Component({
  selector: 'app-bookstore',
  templateUrl: './bookstore.component.html'
})

export class BookStoreComponent implements OnInit {
  public books: Array<Book> = [];
  public book: Book = new Book();

  constructor(private bookStoreService: BookStoreService) {
  }

  ngOnInit(): void {
    this.refresh();
  }

  public remove = (book: Book) => {
    if (this.book.id == book.id) {
      this.reset();
    }
    let bookIndex = this.books.findIndex(x => x.id == book.id);

    this.bookStoreService.remove(book.id).subscribe(
      (data: any) => { this.books.splice(bookIndex, 1) },
      error => console.error(error)
    );
  };

  public edit = (book: Book) => {
    if (book)
      this.book = book;
    else
      this.book = new Book();
  };

  public refresh = (event?: any) => {
    this.bookStoreService.get().subscribe(
      (data: any) => { this.books = data },
      error => console.error(error)
    );
  };

  public reset = (event?: any) => {
    this.book = new Book();
  };

  public save = (book: Book) => {
    let bookIndex = this.books.findIndex(x => x.id && x.id == book.id);

    if (bookIndex >= 0) {
      this.bookStoreService.update(book).subscribe(
        (data: any) => { this.books.splice(bookIndex, 1, data) },
        error => console.error(error)
      );
    } else {
      this.bookStoreService.add(book).subscribe(
        (data: any) => this.books.push(data),
        error => console.error(error)
      );
    }

    this.reset();
  };

}
