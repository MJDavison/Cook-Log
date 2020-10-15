import { Component, OnInit } from '@angular/core';
import { MenuItemDto } from 'src/api/web-api-client';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  menuItem : MenuItemDto;
  constructor() { }

  ngOnInit(): void {
  }

}
