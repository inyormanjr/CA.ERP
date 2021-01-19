import { Injectable } from '@angular/core';
  import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
  import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { UserView } from '../model/user.view';
import { UserService } from '../user.service';


@Injectable()
export class UserUpdateResolver implements Resolve<UserView>{
    constructor(private userService: UserService,
        private alertify: AlertifyService,
        private router: Router){}
        resolve(route: ActivatedRouteSnapshot) : Observable<UserView>{
            return this.userService.getById(route.params.id).pipe(
                catchError((error : any) =>{
                    this.router.navigate(['../list']);
                    this.alertify.error('Resolver cannot be loaded.');
                    return of(null);
                })
            )
        }
}