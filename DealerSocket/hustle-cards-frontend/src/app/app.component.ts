import { Component, OnInit } from '@angular/core';
import { RequestOptions } from '@angular/http';
import { Http } from '@angular/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    constructor(private _httpService: Http) { }
    
    myContainer;
    message;
    apiValues: string[] = [];
    
    ngOnInit() {
        this.myContainer = document.getElementById("myHeader");
        
        
        this._httpService.get('/api/person').subscribe(values => {
         this.apiValues = values.json() as string[];
         
          this.message = this.apiValues[3];
            console.log(this.message);
            (this.myContainer as HTMLInputElement).innerHTML = this.message;
        });
    }
}
