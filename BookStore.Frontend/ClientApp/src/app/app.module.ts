import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BookStoreComponent } from './bookstore/bookstore.component';
import { BookGridComponent } from './bookstore/book-grid.component';
import { BookFormComponent } from './bookstore/book-form.component';
import { AboutComponent } from './about/about.component';

import { BookStoreService } from './bookstore/bookstore.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    BookStoreComponent,
    AboutComponent,
    BookGridComponent,
    BookFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: BookStoreComponent, pathMatch: 'full' },
      { path: 'about', component: AboutComponent },
    ])
  ],
  providers: [
    BookStoreService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
