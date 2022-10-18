import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {HttpClient} from "@angular/common/http";
import {ListItem} from "../order-list/order-list.component";
import {ActivatedRoute, Router} from "@angular/router";

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
  constructor(private http: HttpClient, private route: ActivatedRoute) {
  }

  currentName: string = '';
  currentAmount: number = 0;
  items: OrderItem[] = [];
  columns: string[] = ['name', 'amount', 'actions'];
  dataSource = new MatTableDataSource<OrderItem>()
  @ViewChild('sort') sort: MatSort | undefined;

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.http.get(`http://localhost:6010/api/v1/PurchaseOrder/get/${id}`).subscribe((response: any) => {
          this.items = response.data.items;
          this.dataSource = new MatTableDataSource<OrderItem>(this.items);
        },
        (error) => {
          console.log(error);
        });
    }
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
    let body: any = {items: this.items};
    let id = this.route.snapshot.paramMap.get('id');
    if (id) body.id = id;
    this.http.post('http://localhost:6010/api/v1/PurchaseOrder/create-or-update', body).subscribe((response: any) => {
        alert(response.message);
      },
      (error) => {
        console.log(error);
      });
  }

  remove(element: OrderItem) {
    let index = this.items.indexOf(element, 0);
    if (index > -1) {
      this.items.splice(index, 1);
      this.dataSource = new MatTableDataSource<OrderItem>(this.items);
    }
  }
}
