import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit  {


  constructor(private http: HttpClient) {}; 

  ngOnInit() {
    const topUsers = this.http.get("http://localhost:5057/app/User/topTen")
    topUsers.subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
      complete: () => console.log("compleated")
  })
  }
}
