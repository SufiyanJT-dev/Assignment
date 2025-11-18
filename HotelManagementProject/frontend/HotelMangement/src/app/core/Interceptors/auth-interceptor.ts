import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Apicommuncation } from '../../shared/Api/apicommuncation';
import { throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const api = inject(Apicommuncation);
  const router = inject(Router);

  const token = localStorage.getItem('JwtAccessToken');
  let authReq = req;

  // Attach access token to ALL requests except refresh token request
  if (token && !req.url.includes('auth/refresh-token')) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      // Access token expired
      if (error.status === 401) {

        // Refresh token also expired -> logout
        if (req.url.includes('/auth/refresh-token')) {
          localStorage.removeItem('JwtAccessToken');
          router.navigate(['/login']);
          return throwError(() => error);
        }

        // Try to refresh token
        return api.getRefershToken().pipe(
          
          switchMap((response) => {
            const newToken = response.accessToken;

            // Save new token
            localStorage.setItem('JwtAccessToken', newToken);
            console.log("New Access Token:", newToken);

            // Retry original request with new token
            const retryReq = req.clone({
              setHeaders: { Authorization: `Bearer ${newToken}` }
            });

            return next(retryReq);
          }),

          // If refresh fails → logout
          catchError(err => {
            localStorage.removeItem('JwtAccessToken');
            router.navigate(['/login']);
            return throwError(() => err);
          })
        );
      }

      // Other errors
      return throwError(() => error);
    })
  );
};
