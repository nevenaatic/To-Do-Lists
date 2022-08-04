import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { ToDoListService } from "./to-do-list.service";

@NgModule({
    imports: [
        HttpClientModule
    ],
    providers: [
        ToDoListService
    ]
})
export class CoreModule { }