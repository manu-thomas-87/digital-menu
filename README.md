## Digital Menu

### Projects 
- ASP .NET Web API - REST API to handle the required CRUD operations of the digital menu end points. 
- Angular Web App - It will show the digital menu details which serverd from the Web API.

### Configuration 
#### Web API 
Host the rest API in IIS and note down the URL to configure it in the Angular web app. Web API project location ```digital-menu/DigitalMenu.Web.API/```

#### Angular web app
Location : digital-menu/ng/ng-digital-menu/

**Prerequisites**
Angular CLI must be installed in local machine. If angular CLI is not installed pleae install it using npm.
```
npm install -g @angular/cli
```

1. Configure the API base URL as 'base_url' in src/environments/environment.ts 
2. Navigate to the angular web app folder(please see the refered location) using CMD.
3. Install the required packages using node package manager
``` npm install ```.
4. Run ```ng serve``` for a dev server. Navigate to http://localhost:4200/. 

