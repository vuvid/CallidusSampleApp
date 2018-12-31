import { Author } from "./author";

export class Book {
  id: number;
  title: string;
  isbn: string;
  year: number;
  price: number;
  created: string;
  modified: string;
  authors: Array<Author>;
}
