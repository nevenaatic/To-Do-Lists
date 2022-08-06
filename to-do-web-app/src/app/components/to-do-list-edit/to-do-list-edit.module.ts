import { NgModule } from  "@angular/core";
import { ToDoListEditComponent } from "./to-do-list-edit.component";
import { ToDoItemComponent } from "./to-do-item/to-do-item.component";
import { MatCheckboxModule} from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from "@angular/platform-browser";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatNativeDateModule } from "@angular/material/core";
import { MatInputModule} from '@angular/material/input';
import { MatIconModule} from '@angular/material/icon';
import { MatToolbarModule} from '@angular/material/toolbar';
import { MatButtonModule} from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { NgxMatMomentModule } from "@angular-material-components/moment-adapter";
import { NgxMatDatetimePickerModule } from "@angular-material-components/datetime-picker";

@NgModule({
    declarations: [
       ToDoListEditComponent,
       ToDoItemComponent
    ],
    exports: [
       ToDoListEditComponent
    ],
    imports:[
      MatCheckboxModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserModule,
      MatDatepickerModule,
      MatFormFieldModule,
      MatDatepickerModule,
      MatNativeDateModule,
      MatInputModule,
      MatIconModule,
      MatToolbarModule,
      MatButtonModule,
      MatSnackBarModule,
      DragDropModule,
      NgxMatDatetimePickerModule,
      NgxMatMomentModule, 
    ],
    providers:[
      MatDatepickerModule,  
    ]
})
export class ToDoEditModule {}