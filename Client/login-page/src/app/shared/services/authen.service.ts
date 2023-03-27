import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router } from "@angular/router";
import { Observable } from "rxjs/internal/Observable";
import { map } from "rxjs/internal/operators/map";

export class UserLogin  {
  loginName!:string;
}
const apiUrl = 'https://localhost:7257/api/Auth/'
const defaultPath = '';
@Injectable({
  providedIn:'root'
})
export class AuthenService {
    
    user!: UserLogin | null;
    currAcc!: string;
    private readonly JWT_TOKEN = 'JWT_TOKEN';
    isLogin: boolean = false;
    get loggedIn():boolean {
        return !!this.user;  //ép kiểu về boolean
    }

    private _lastAuthenticatedPath:string = defaultPath;
    set lastAuthenticatedPath(value:string){
        this._lastAuthenticatedPath = value;
    }
    constructor(private router: Router, private http: HttpClient){}
    async logIn(username: string, password: string): Promise<boolean>{
      localStorage.removeItem(this.JWT_TOKEN);
      const userLogin = {
          'loginName': username,
          'password' : password
        }
        var url = `${apiUrl}`+ 'login';
        try{
        var res =  await this.http.post<any>(url, userLogin).toPromise();
        if(res.token){
                this.user = {
                 'loginName': username
                }
                this.router.navigate([this._lastAuthenticatedPath]);
                localStorage.setItem(this.JWT_TOKEN,res.token);
                this.currAcc = res.token;
                this.isLogin = true;
        }else {
              this.isLogin = false;
        }
        return this.isLogin;
      }catch(error) {
        return this.isLogin;
      }
        

      }

      logOut()
      {
        this.user = null,
        localStorage.removeItem(this.JWT_TOKEN);
        this.router.navigate(['/home']);
      }

}

@Injectable()
export class AuthGuardService implements CanActivate {
    constructor(private authService: AuthenService,private router: Router) { }
    canActivate(route: ActivatedRouteSnapshot): boolean {
        const isLoggedIn = this.authService.loggedIn;
        const isAuthForm = [
          'home'
        ].includes(route.routeConfig?.path || defaultPath);
    
        if (isLoggedIn && isAuthForm) {
          this.authService.lastAuthenticatedPath = defaultPath;
          this.router.navigate([defaultPath]);
          return false;
        }
    
        if (!isLoggedIn && !isAuthForm) {
          this.router.navigate(['']);
        }
    
        if (isLoggedIn) {
          this.authService.lastAuthenticatedPath = route.routeConfig?.path || defaultPath;
        }
    
        return isLoggedIn || isAuthForm;
      }
}