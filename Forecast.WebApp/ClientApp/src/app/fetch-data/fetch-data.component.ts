import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: Forecast[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    const apiUrl = 'http://localhost:5000/api/forecast/data';
    http.get<Forecast[]>(apiUrl).subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}
