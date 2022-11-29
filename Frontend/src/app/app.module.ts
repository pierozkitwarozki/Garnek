import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { CreatePhraseComponent } from './create-phrase.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule}  from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { SnackbarService } from './snackbar.service';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { GarnekWebapiService } from './services/garnekwebapi.service';

@NgModule({
  declarations: [	
    CreatePhraseComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSnackBarModule,
    MatCardModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatProgressBarModule
  ],
  providers: [
    SnackbarService,
    GarnekWebapiService
  ],
  bootstrap: [CreatePhraseComponent]
})
export class AppModule { }
