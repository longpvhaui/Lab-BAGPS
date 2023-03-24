
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenService } from '../shared/services/authen.service';
import { environment } from 'src/environments/environment';




@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
        let currentAcc = this.authenticationService.currAcc;
        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if (currentAcc && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentAcc}`
                }
            });
        }

        return next.handle(request);
    }
}