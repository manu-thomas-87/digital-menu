import { Component, OnInit } from '@angular/core';
import { MenuService } from '../../Service/menu.service';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.css']
})
export class DashBoardComponent implements OnInit {

  constructor(private menuService: MenuService) { }
  public menu: any;
  public IsDataLoaded: boolean

  ngOnInit(): void {
    const sub = this.menuService.GetMenu().subscribe(res => {
      this.menu = res;
      this.IsDataLoaded = true;
    });
  }
}
