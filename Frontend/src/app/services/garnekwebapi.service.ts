import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { finalize, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GarnekWebapiService {

public requestCount$ = new BehaviorSubject<number>(0);

constructor(private httpClient: HttpClient) { }

public addPhrases(body: any): Observable<any> {
  return this.postRequest(environment.baseUrl + 'Phrase', body);
}

public getCategories(): Observable<any> {
  return this.getRequest(environment.baseUrl + 'Category/All');
}

public checkIfPhrasesCanBeAdded(gameId: string, userName: string): Observable<any> {
  const endpoint = 
    environment.baseUrl + 'Phrase/CanBeAdded/' + gameId + '/' + userName;

  return this.getRequest(endpoint);
}

private postRequest(endpoint: string, body: any): Observable<any> {
  this.requestCount$.next(this.requestCount$.value + 1);

  return this.httpClient.post(endpoint, body, { headers: { 'Content-Type': 'application/json'}})
    .pipe(retry(2), finalize((): void => {
      this.requestCount$.next(this.requestCount$.value - 1);
    }))
}

private getRequest(endpoint: string): Observable<any> {
  this.requestCount$.next(this.requestCount$.value + 1);

  return this.httpClient.get(endpoint)
    .pipe(retry(2), finalize((): void => {
      this.requestCount$.next(this.requestCount$.value - 1);
    }))
}
}
