import { Component } from '@angular/core';

@Component({
  selector: 'app-header-component',
  imports: [],
  templateUrl: './header-component.html',
  styleUrl: './header-component.scss',
})
export class HeaderComponent {
  studentName:string = "Neider Vélez"
  appName:string = "Aplicativo Unisersitario"
  loginStatus:boolean = false;
  loginCSS:string = "gray-text"

  login(): void {
    this.loginStatus = !this.loginStatus;
    if(this.loginStatus == true){
      this.loginCSS = "green-text";
    }else{
      this.loginCSS = "gray-text";
    }
  }
}
