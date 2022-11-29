import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Subscription,  } from 'rxjs';
import { retry } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { SnackbarService } from './snackbar.service';
@Component({
  selector: 'app-root',
  templateUrl: './create-phrase.component.html',
  styleUrls: ['./create-phrase.component.scss']
})
export class CreatePhraseComponent implements OnInit, OnDestroy {
  private dataExistEndpoint = environment.baseUrl + 'Phrase/CanBeAdded/'
  private routeParamsSubscription: Subscription;
  public matcher = new MyErrorStateMatcher();
  public gameId: string;
  public userName: string;
  public message: string = '';
  public phrasesFormGroup: FormGroup;
  public categories: any[] = [];
  public lockButton: boolean = false;

  constructor(
    private router: ActivatedRoute, 
    private httpClient: HttpClient,
    private snackBarService: SnackbarService,
    private formBuilder: FormBuilder) {
  }
  ngOnInit(): void {
    this.buildForm();
    this.routeParamsSubscription = this.router.params.subscribe(params => {
      this.gameId = params['gameId'];
      this.userName = params['userName'];
      if (this.gameId && this.userName) {
        this.checkIfPhrasesCanBeAdded();
      }
    })
  }

  ngOnDestroy(): void {
    this.routeParamsSubscription.unsubscribe();
  }

  private getCategories(): void {
    this.httpClient.get(environment.baseUrl + 'Category/All')
      .pipe(retry(2))
      .subscribe((response: any[]) => {
        if (response) {
          this.categories = response.map(x => 
            { return { name: x.name, polishLabel: x.polishLabel} });
        }
      }, (error: HttpErrorResponse): void => {
        this.snackBarService.openSnackBar('Błąd przy pobieraniu kategorii, spróbuj ponownie za chwilę 😤');
      })
  }

  private checkIfPhrasesCanBeAdded(): void {
    const endpoint = 
      this.dataExistEndpoint + this.gameId + '/' + this.userName;
    this.httpClient.get(endpoint).pipe(
      retry(2))
      .subscribe((response: number): void => {
        this.getCategories();
      }, (error: HttpErrorResponse): void => {
      if (error.status === 404) {
        this.message = "Wprowadzono błędną nazwę gracza lub identyfikator gry. Sprawdź poprawność danych i spróbuj ponownie. 😤";
        return;
      }

      if (error.status === 422) {
        this.message = "Wprowadziłeś wszystkie hasła. Poczekaj na innych graczy i rozpocznij grę. 🥳";
        return;
      }

      this.message = "Wystąpił błąd z serwerem. 😤";
      this.snackBarService.openSnackBar(this.message);
    })
  }

  private buildForm(): void {
    this.phrasesFormGroup = this.formBuilder.group(
      {
        firstPhrase0c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
        secondPhrase0c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
        thirdPhrase0c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
        firstPhrase1c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
        secondPhrase1c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
        thirdPhrase1c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
        firstPhrase2c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
        secondPhrase2c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
        thirdPhrase2c: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
      }
    )
  }

  public sendPhrases(): void {
    this.lockButton = true;

    let body = {
      gameId: this.gameId,
      userName: this.userName,
      phrases: {}
    };

    body.phrases[`${this.categories[0].name}`] = [
      this.phrasesFormGroup.get('firstPhrase0c').value,
      this.phrasesFormGroup.get('secondPhrase0c').value,
      this.phrasesFormGroup.get('thirdPhrase0c').value
    ];
    body.phrases[`${this.categories[1].name}`] = [
      this.phrasesFormGroup.get('firstPhrase1c').value,
      this.phrasesFormGroup.get('secondPhrase1c').value,
      this.phrasesFormGroup.get('thirdPhrase1c').value
    ];
    body.phrases[`${this.categories[2].name}`] = [
      this.phrasesFormGroup.get('firstPhrase2c').value,
      this.phrasesFormGroup.get('secondPhrase2c').value,
      this.phrasesFormGroup.get('thirdPhrase2c').value
    ];
    
    this.httpClient
      .post(environment.baseUrl + 'Phrase', body, { headers: { 'Content-Type': 'application/json'}})
      .pipe(retry(2))
      .subscribe((): void => {
        this.snackBarService.openSnackBar('Hasła zostały dodane. Mozesz zamknac strone. 😍');
        this.message = 'Wprowadziłeś wszystkie hasła. Poczekaj na innych graczy i rozpocznij grę. 🥳';
      }, (error: HttpErrorResponse): void => {
        this.lockButton = false;
        this.snackBarService.openSnackBar('Oooops.. Coś poszło nie tak. Sprawdź poprawność haseł i spróbuj ponownie. 😤');
      })
  }
}

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
