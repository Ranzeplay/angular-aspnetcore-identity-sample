import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { StorageService } from 'src/app/services/storage/storage.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
})
export class SigninComponent implements OnInit {
  public emailAddress: string;
  public password: string;

  constructor(private http: HttpClient, public storage: StorageService) {}

  ngOnInit() {}

  public submit() {
    this.http.post('https://localhost:5001/auth/signin', {
      emailAddress: this.emailAddress,
      password: this.password,
    }, { observe: 'response' })
    .toPromise()
    .then(result => {
      const body: any = result.body;

      this.storage.update('token', body.content);
    }).catch(error => {
      alert(error.status);
    });
  }
}
