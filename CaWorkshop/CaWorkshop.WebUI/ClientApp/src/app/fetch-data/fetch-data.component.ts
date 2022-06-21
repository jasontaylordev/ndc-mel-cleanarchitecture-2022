import { Component } from '@angular/core';
import { TodoListsClient, WeatherForecast, WeatherForecastClient } from '../services/ca-workshop-api.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(client: WeatherForecastClient, todoClient: TodoListsClient) {
    client.getAbc().subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}
