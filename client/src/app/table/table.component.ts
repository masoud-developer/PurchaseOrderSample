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
  MatNoDataRow,
  MatRowDef,
  MatTable,
  MatTableDataSource,
} from '@angular/material/table';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent<T> implements AfterContentInit {
  @ContentChildren(MatHeaderRowDef) headerRowDefs: QueryList<MatHeaderRowDef> | undefined;
  @ContentChildren(MatRowDef) rowDefs: QueryList<MatRowDef<T>> | undefined;
  @ContentChildren(MatColumnDef) columnDefs: QueryList<MatColumnDef> | undefined;
  @ContentChild(MatNoDataRow) noDataRow: MatNoDataRow | undefined;

  @ViewChild(MatTable, {static: true}) table: MatTable<T> | undefined;

  @Input() columns: string[] | undefined;

  @Input()
  dataSource!: DataSource<T>;

  ngAfterContentInit() {
    this.columnDefs?.forEach(columnDef => this.table?.addColumnDef(columnDef));
    this.rowDefs?.forEach(rowDef => this.table?.addRowDef(rowDef));
    this.headerRowDefs?.forEach(headerRowDef => this.table?.addHeaderRowDef(headerRowDef));
    if(this.noDataRow)
       this.table?.setNoDataRow(this.noDataRow);
  }
}
