import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { DigitalMenuModule } from './../digital-menu/digital-menu.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,DigitalMenuModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
