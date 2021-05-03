import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  public username: string;
  public emailAddress: string;
  public password: string;

  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public submit() {
    this.http.post('https://localhost:5001/auth/signup', {
      userName: this.username,
      emailAddress: this.emailAddress,
      password: this.password,
    }, { observe: 'response' })
    .toPromise()
    .then(result => {
      alert(JSON.stringify(result.body));
    }).catch(error => {
      alert(error.status);
    });
  }
}
