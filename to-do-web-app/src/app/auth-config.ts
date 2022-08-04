 import { LogLevel, Configuration, BrowserCacheLocation } from '@azure/msal-browser';

 const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;
 
 export const msalConfig: Configuration = {
   auth: {
     clientId: '4e1ff54b-bf34-4f45-83ce-e50fc32967cd',
     authority: 'https://login.microsoftonline.com/common',
     redirectUri: 'http://localhost:4200', 
   },
   cache: {
     cacheLocation: BrowserCacheLocation.LocalStorage, 
     storeAuthStateInCookie: false, 
   }
 }
 
 export const protectedResources = {
   todoListApi: {
     endpoint: "https://localhost:7011/api/to-do-lists",
     scopes: ["api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.read", "api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.write"],
   },
 }

 export const loginRequest = {
   scopes: ["api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.read", "api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.write"]
 };