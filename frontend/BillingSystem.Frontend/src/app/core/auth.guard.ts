import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router, ActivatedRouteSnapshot, RouterStateSnapshot,  } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(private authService: AuthService, private router: Router) {}

  private checkLogin(): boolean {
     const loggedIn = this.authService.isLoggedIn();
    console.log('AuthGuard.canActivate, loggedIn:', loggedIn);
    if (this.authService.isLoggedIn()) {
      return true;
    }
    this.router.navigate(['/login']);
    return false;
  }

  canActivate(): boolean {
    const loggedIn = this.authService.isLoggedIn();
  console.log('AuthGuard.canActivateChild, loggedIn:', loggedIn);
    return this.checkLogin();
  }

  canActivateChild(): boolean {
    const loggedIn = this.authService.isLoggedIn();
  console.log('AuthGuard.canActivateChild, loggedIn:', loggedIn);
    return this.checkLogin();
  }
}


