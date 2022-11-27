import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

constructor(private snackBar: MatSnackBar) { }

public openSnackBar(message: string): void {
  this.snackBar.open(message, 'Ok', {
    duration: 3000,
    horizontalPosition: 'center',
    verticalPosition: 'bottom'
  })
}

}
