import {DataSource} from '@angular/cdk/collections';
import {
  AfterContentInit,
  Component,
  ContentChildren,
  Input,
  AfterViewInit,
  QueryList,
  ViewChild,
  ContentChild,
} from '@angular/core';
import {MatSort} from '@angular/material/sort';
import {
  MatColumnDef,
  MatHeaderRowDef,
  MatRowDef,
  MatTable,
  MatTableDataSource,
} from '@angular/material/table';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";


export interface ListItem {
  name: string;
  creationTime: string;
  status: number;
}

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements AfterViewInit {
  constructor(private router: Router, private http: HttpClient) {
  }

  displayedColumns: string[] = ['name', 'creationTime', 'status'];
  dataSource = new MatTableDataSource<ListItem>([]);

  @ViewChild('sort') sort: MatSort | undefined;

  ngAfterViewInit() {
    if (this.sort)
      this.dataSource.sort = this.sort;

    this.http.get('http://localhost:6010/api/v1/PurchaseOrder/list').subscribe((response: any) => {
        this.dataSource = new MatTableDataSource<ListItem>(response.data);
      },
      (error) => {
        console.log(error);
      });
  }

  navigateToAddOrderPage() {
    this.router.navigateByUrl('/edit-order');
  }

  navigateToEditOrderPage(id: string) {
    this.router.navigateByUrl(`/edit-order/${id}`);
  }

}
