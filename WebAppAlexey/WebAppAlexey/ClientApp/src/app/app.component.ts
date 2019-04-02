import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title: "app";
  users: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    //let token = localStorage.getItem("jwt");
    //this.http.get("http://localhost:50133/api/auth/login", {
    //  headers: new HttpHeaders({
    //    "Authorization": "Bearer " + token,
    //    "Content-Type": "application/json"
    //  })
    //}).subscribe(response => {
    //  this.users = response;
    //}, err => {
    //  console.log(err)
    //});
  }
}
