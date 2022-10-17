import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {HttpClient} from "@angular/common/http";
import {ListItem} from "../order-list/order-list.component";

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
  constructor(private http: HttpClient) {
  }

  currentName: string = '';
  currentAmount: number = 0;
  items: OrderItem[] = [];
  columns: string[] = ['name', 'amount'];
  dataSource = new MatTableDataSource<OrderItem>()
  @ViewChild('sort') sort: MatSort | undefined;

  ngOnInit(): void {
    this.http.get('http://localhost:6010/api/v1/PurchaseOrder/get/').subscribe((response: any) => {
        this.items = response.data.items;
      },
      (error) => {
        console.log(error);
      });
  }

  addItem() {
    if (!this.currentName || !this.currentAmount || this.currentAmount == 0)
      return;
    this.items.push({name: this.currentName, amount: this.currentAmount})
    this.dataSource = new MatTableDataSource<OrderItem>(this.items);
  }

  submit() {
    // this.http.post()
  }

  save() {
    this.http.post('http://localhost:6010/api/v1/PurchaseOrder/create-or-update', {items: this.items}).subscribe((response: any) => {
        alert(response.message);
      },
      (error) => {
        console.log(error);
      });
  }
}
