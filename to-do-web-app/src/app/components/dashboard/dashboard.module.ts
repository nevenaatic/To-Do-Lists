import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { DashboardComponent } from "./dashboard.component";
import { MatCardModule} from '@angular/material/card';
import { MatInputModule} from '@angular/material/input';
import { ToDoListPreviewComponent } from './to-do-list-preview/to-do-list-preview.component';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { MatIconModule} from '@angular/material/icon';
import { MatButtonModule} from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { ToDoEditModule } from "../to-do-list-edit/to-do-list-edit.module";
import { MatToolbarModule} from '@angular/material/toolbar';
import { DragDropModule} from '@angular/cdk/drag-drop';
import {  MatFormFieldModule } from "@angular/material/form-field";
import { ClipboardModule } from 'ngx-clipboard';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
    declarations: [
        DashboardComponent,
        ToDoListPreviewComponent,
         
    ],
    exports: [
        DashboardComponent
    ],
    imports:[
        BrowserModule,
        MatCardModule,
        MatInputModule,
        MatCheckboxModule,
        FormsModule,
        MatIconModule,
        MatButtonModule,
        ReactiveFormsModule,
        ToDoEditModule,
        MatToolbarModule,
        DragDropModule,
        MatFormFieldModule,
        ClipboardModule,
        MatPaginatorModule
    ]
})
export class DashboardModule { }