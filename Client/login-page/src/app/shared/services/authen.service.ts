import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router } from "@angular/router";


@Injectable()
export class UserLogin  {
    loginName!:string;
}
const apiUrl = 'https://localhost:7257/api/Auth/'
const defaultPath = '';

export class AuthenService {
    
    user!:UserLogin;
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
    constructor(private router: Router, private httpClient : HttpClient){}
    logIn(username: string, password: string): boolean{
        const userLogin = {
          'username': username,
          'password' : password
        }
        var url = `${apiUrl}`+ '/login';
        this.httpClient.post<any>(url, userLogin).subscribe(data =>{
              this.user = {
             'loginName': data.data.loginName
            }
            this.router.navigate([this._lastAuthenticatedPath]);
            localStorage.setItem(this.JWT_TOKEN,data.data.token);
            this.currAcc = data.data.token;
            this.isLogin = true;
        }, (err:any) => {
          this.isLogin = false;
          if(err){ 
           
            
          }
           this.isLogin = false
        });
        return this.isLogin;
      }

      logOut()
      {
        
      }

}

@Injectable()
export class AuthGuardService implements CanActivate {
    constructor(private authService: AuthenService,private router: Router) { }
    canActivate(route: ActivatedRouteSnapshot): boolean {
        const isLoggedIn = this.authService.loggedIn;
        const isAuthForm = [
          'login-form'
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