import { Injectable } from '@angular/core';
import {
  ActivatedRoute,
  CanActivate,
  CanActivateChild,
  Router
} from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate, CanActivateChild {

  constructor(private authService: AuthService,
    private router: Router,
  ) { }


  canActivate() {
    const token = this.authService.decodedToken();
    if (token) {
    
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }

  canActivateChild() {
    const token = this.authService.decodedToken();
    if (token) {
  
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}

