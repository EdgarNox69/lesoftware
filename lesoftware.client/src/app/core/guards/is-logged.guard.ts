import { inject } from '@angular/core';
import { CanMatchFn, Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { map } from 'rxjs';

export const isLoggedGuard: CanMatchFn = (route, segments) => {
  const loginService = inject(LoginService)
  const router = inject(Router)

  return loginService.currentUserLoginOn.pipe(
    map((currentUserLoginOn)=>currentUserLoginOn || router.createUrlTree(['/login']))
  );
};
