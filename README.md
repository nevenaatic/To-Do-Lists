# To-Do List

Is a web application for maintaining to-do lists (like [Google Keep](https://keep.google.com/)).
 The application consists of 3 segments:
* [API](#the-api)
* [Web Client](#web-client)
* [Security & Deployment](#security-&-deployment)

<p style="align-items: center">
<img align="left" alt="HTML" width="46px" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/html/html.png" />
<img align="left" alt="CSS"  width="46px" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/css/css.png" /> 
<img align="left" alt="TypeScript"  width="44px"src="https://imgs.search.brave.com/xRUA4UR_QkfMJQzWWENQ25CQyTSEUn7KjjnNILkmIPU/rs:fit:550:550:1/g:ce/aHR0cDovL3d3dy5z/b2Z0d2FyZS1hcmNo/aXRlY3RzLmNvbS9j/b250ZW50L2ltYWdl/cy9ibG9nLzIwMTYv/MTIvdHlwZXNjcmlw/dC1sb2dvLnBuZw"/>
<img align="left" alt="Angular" width="56px" height="56px" src="https://imgs.search.brave.com/niD9Ow-Pa2QlCDOjVda7f93oQ5ef85M0wyHGDfvTdiM/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly9jZG4t/aW1hZ2VzLTEubWVk/aXVtLmNvbS9tYXgv/MTIwMC8xKkdtTXRL/em56SjFkUzhzU3p4/elIzb3cucG5n" />
<img align="left"  width="56px" height="56px" src="https://img.favpng.com/23/10/7/c-programming-language-logo-microsoft-visual-studio-net-framework-png-favpng-WLLTMqZhSPAk9q3DTh993fZnh.jpg" />
<img align="left"  width="56px" height="56px" src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Microsoft_.NET_logo.svg/1200px-Microsoft_.NET_logo.svg.png" />
<img align="left"  width="56px" height="56px" src="https://www.media3.net/img/m3/mssql.jpg" />
</p>
<br /> .

## The API

An REST API solution which exposes CRUD & search endpoints. Reminder functionality 
that will run as separate service. 

The API should support the following:

* Preview of to-do-list.
* Update of to-do list, title & list items (including list/item reordering).
* Creation of to-do list containing list title & list items.
* Removal of to-do list.
* Searching the to-do lists by title (with partial match & case insensitive, e.g. if search criteria is "Dark", the result should contain item with name "darko").
* API swagger documentation.
*  Reminder functionality implying email sending for all of to-do-lists which remindMe date has expired.
*  Unit tests.

The API will include the following technologies/service providers:
* .Net Core 6 (Web API)
* Entity Framework Core 6
* [SendGrid](https://sendgrid.com/) email server provider

 
## Web Client

The API will be used by the client web application. The client application will 
provide user interface which should support the 
following:

* Dashboard page containing the list/grid of all available to-do lists with search input. "Reminded" to-do lists should be on top and with proper indicator.
* Page/popup for creating/editing of to-do list/items. To-do list/item should be created/update on focus lost event (like in Google Keep).
* Removal of to-do lists/items.
* To-do lists/items position change via drag-and-drop.
* "RemindMe" logic input fields (add/remove and validations).
 
The client will include the following technologies:
* Angular 14


## Security & Deployment

Web client & the API should support authentication and authorization. All three components, database, the api & web client should be hosted on Azure cloud.

* Enable web client/the api authentication via bearer token provided from Auth0 identity provider.
* Enable web client/the api authorization (e.g. only users with email domain *novalite.rs* have all rights, other can only access Dashboard page).
* Expanding the database model with user information and updating the business logic to include user information.
* Sharing of to-do-lists via link with expiration period (including background process to remove invalid validation links data).
* Deploying the solution to the Azure cloud system.

Security will include the following technologies/service provider
* OAuth 2.0 (via [Auth0](https://auth0.com/) identity provider)
