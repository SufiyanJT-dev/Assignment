import { HttpInterceptorFn } from '@angular/common/http';

export const headerInterceptor: HttpInterceptorFn = (req, next) => {
const token=localStorage.getItem('JwtAccessToken')


const modified = req.clone({
  setHeaders: {
    
    'X-App-Version': '1.0.0',
    Authorization: token ? `Bearer ${token}` : ''
  }
});

      return next(modified);
 };


   