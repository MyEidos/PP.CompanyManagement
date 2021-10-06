import { HttpEventType } from '@angular/common/http';
import {  ElementRef, EventEmitter, OnInit, Output } from '@angular/core';
import { FileSaverService } from 'ngx-filesaver';
import { EmployeeService } from '../services/employee.service';
import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, SortDirection } from '@angular/material/sort';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { IEmployeeViewModel } from '../models/rest/Portal/ViewModels/Employee/IEmployeeViewModel';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css']
})
export class EmployeesListComponent implements AfterViewInit {
  public progress: number = 0;
  public message?: string;

  @Output() public onUploadFinished = new EventEmitter();

  displayedColumns: string[] = ['id', 'first', 'personnelNumber', 'type'];
  dataSource: MatTableDataSource<IEmployeeViewModel>;
  employees: IEmployeeViewModel[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  constructor(private employeeService: EmployeeService, private fileSaverService: FileSaverService) {
    this.dataSource = new MatTableDataSource();

    this.loadData();
  }

  loadData() {
    this.employeeService.Get().subscribe(x => this.dataSource.data = x.list);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    //this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  public uploadFile = (files: FileList) => {
    
    if (files.length === 0) {
      return;
    }

    let fileToUpload = files[0];

    this.employeeService.importEmployeesFromFile(fileToUpload)
      .subscribe(event => {
        if (event  && event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        }
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);

          console.debug(event.body);

          this.fileSaverService.save(event.body, "employeeImportResult.json");

          this.loadData();
        }
      });
}
}
