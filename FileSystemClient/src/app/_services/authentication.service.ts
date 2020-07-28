import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router'

import { environment } from 'src/environments/environment';
import { UserData } from '../_models/UserData';

import * as jwt_decode from 'jwt-decode';
 
@Injectable({ 
  providedIn: 'root' 
})
export class AuthenticationService {

    private currentUserSubject: BehaviorSubject<UserData>;

    constructor(
        private http: HttpClient,
        private router: Router
        ) { }

        getCurrentUser(): UserData {
            return JSON.parse(localStorage.getItem('currentUser'));
          }

    // register(data: any) {
    //     return this.http.post<any>(`${environment.apiUrl}account/register`, data).subscribe(
    //       data => {
    //         this.snackBar.open("Registered successfully", "OK", { duration: 2000 });
    //         this.router.navigateByUrl("/login");
    //       },
    //       err => {
    //         if (err.status == 400) {
    //           if (err.error.errors instanceof Array) {
    //             err.error.errors.forEach(element => {
    //               this.snackBar.open(element.description, "OK", { duration: 4000 });
    //             });
    //           } else {
    //             console.log(err);
    //           }
    //         }
    //       });
    //   }

    login(model: any){
        console.log(model);

        return this.http.post(`${environment.apiUrl}/api/Account/login`, model)
        .pipe(
            map((data: any) => {
                console.log("12"); 
                this.storeUserData(data['token']);
                this.router.navigateByUrl('/dash');  
            }
            )
        ).subscribe();
    }

    storeUserData(token: string) {
        const payload = jwt_decode(token);

        const user: UserData = {
          id: payload['nameid'],
          username: payload['sub'],
          token: token
        }
        
        localStorage.setItem('currentUser', JSON.stringify(user));
      }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.router.navigateByUrl('/login');
    }
}