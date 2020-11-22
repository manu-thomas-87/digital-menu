import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashBoardComponent } from './Component/dash-board/dash-board.component';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  declarations: [DashBoardComponent],
  imports: [
    CommonModule,HttpClientModule
  ],
  exports: [DashBoardComponent]
})
export class DigitalMenuModule { }
