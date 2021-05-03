import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { StorageService } from '../services/storage/storage.service';

@Component({
  selector: 'app-check',
  templateUrl: './check.component.html',
  styleUrls: ['./check.component.scss'],
})
export class CheckComponent implements OnInit {
  resourceString: string;

  constructor(private http: HttpClient, private storage: StorageService) {}

  ngOnInit() {}

  public check_resource() {
    const header = new HttpHeaders()
      .set('Authorization', 'Bearer ' + this.storage.getString('token'));

    this.http
      .get('https://localhost:5001/weatherforecast', {
        responseType: 'json',
        headers: header,
        observe: 'response'
      })
      .toPromise()
      .then((result) => {
        this.resourceString = JSON.stringify(result.body);
      })
      .catch((error) => {
        alert(error.status);
      });
  }
}
