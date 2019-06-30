import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-actual-data',
  templateUrl: './actual-data.component.html',
  styleUrls: ['./actual-data.component.css']
})
export class ActualDataComponent {

  public forecast: Forecast;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    const apiUrl = 'http://localhost:5000/api/forecast/actual';
    http.get<Forecast>(apiUrl).subscribe(result => {
      this.forecast = result;
    }, error => console.error(error));
  }
}
