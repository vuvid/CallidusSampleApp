import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class BookStoreService {
  private headers: HttpHeaders;
  private booksApiUrl: string = '/api/bookstore';

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  }

  public get() {
    return this.http.get(`${this.booksApiUrl}/books`, { headers: this.headers });
  }

  public getAuthors() {
    return this.http.get(`${this.booksApiUrl}/authors`, { headers: this.headers });
  }

  public add(book) {
    return this.http.post(`${this.booksApiUrl}/books`, book, { headers: this.headers });
  }

  public remove(id) {
    return this.http.delete(`${this.booksApiUrl}/books/${id}`, { headers: this.headers });
  }

  public update(book) {
    return this.http.put(`${this.booksApiUrl}/books`, book, { headers: this.headers });
  }
}
