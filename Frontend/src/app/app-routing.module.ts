import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreatePhraseComponent } from './create-phrase.component';


const routes: Routes = [
  { path: ':gameId/:userName', component: CreatePhraseComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash : true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
