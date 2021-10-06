import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { IGetEmployeesResponse } from '../models/rest/Portal/ViewModels/Employee/IGetEmployeesResponse';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  //TODO: move to config. envs etc.
  private baseUrl = 'https://localhost:44352/employees';
  constructor(private http: HttpClient) { }

  public importEmployeesFromFile(fileToUpload: File) {

    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    const headerDict = {
      'Accept': 'application/json'
    }

    return this.http.post(this.baseUrl + "/import", formData, { responseType: "blob", headers: headerDict, reportProgress: true, observe: 'events' });
  }

  public Get() {
    return this.http.get<IGetEmployeesResponse>(this.baseUrl);
  }
}
