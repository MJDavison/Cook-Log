import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navTop',
  templateUrl: './navTop.component.html',
  styleUrls: ['./navTop.component.scss']
})
export class TopComponent implements OnInit {
  isExpanded: boolean;
  constructor() { }

  ngOnInit(): void {
  }

}
