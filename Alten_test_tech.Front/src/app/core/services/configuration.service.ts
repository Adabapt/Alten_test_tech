import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { Configuration } from '../models/configuration';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {

  configuration: Configuration =  new Configuration();

  constructor(private httpClient : HttpClient) { }

  async getConfiguration() {
    this.configuration = <Configuration>await firstValueFrom(this.httpClient.get('../../assets/config/configBack.json'));
  }
}
