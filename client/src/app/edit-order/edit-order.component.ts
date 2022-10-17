import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";

interface OrderItem {
  name: string,
  amount: number
}

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.scss']
})
export class EditOrderComponent implements OnInit {
  constructor() {
  }

  currentName:string = '';
  currentAmount:number = 0;
  items: OrderItem[] = [];
  columns: string[] = ['name', 'amount'];
  dataSource = new MatTableDataSource<OrderItem>()
  @ViewChild('sort') sort: MatSort | undefined;

  ngOnInit(): void {

  }

  addItem(){
    this.items.push({name:this.currentName, amount: this.currentAmount})
    this.dataSource = new MatTableDataSource<OrderItem>(this.items);
  }
}
