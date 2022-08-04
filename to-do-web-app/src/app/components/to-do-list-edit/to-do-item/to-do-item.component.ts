import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { MessageService } from 'src/app/core/message.service';
import { ToDoListService } from 'src/app/core/to-do-list.service';
import { ToDoItem } from 'src/app/models/to-do-item.model';

@Component({
    selector: 'app-to-do-item',
    templateUrl: './to-do-item.component.html',
    styleUrls: ['./to-do-item.component.css']
})
export class ToDoItemComponent implements OnDestroy {
    @Input() listItemForm = new FormGroup({
        id: new FormControl(''),
        text: new FormControl(''),
        isChecked: new FormControl(false),
        position: new FormControl(0)
    });
    @Input() listId: string | undefined;
    @Output() deletedItem = new EventEmitter();
    @Output() addedItem = new EventEmitter();
    subscription!: Subscription;
    constructor(private readonly listService: ToDoListService, private readonly snackBar: MatSnackBar, private messageService: MessageService) {
        this.subscription = messageService.$subject.subscribe(list => {
            this.listId = list.id;
            this.createItem();
        });
    }
    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }

    readonly updateItem = () => {
        if (!this.listItemForm.controls.text.value) {
            return;
        }
        if (this.id) {
            this.listService.updateListItem(this.listId!, this.getValues()).subscribe(_ =>
                this.snackBar.open('List item updated'));
            return;
        }
        if (!this.listId) {
            this.createListWithItem()
        }
        else {
            this.createItem();
        }
    };

    readonly createListWithItem = () => {
        return this.messageService.createListWithItem();
    }

    get id(): string | undefined {
        return this.listItemForm.controls.id.value as string | undefined;
    }

    private readonly createItem = () => {
        const item = new ToDoItem();
        item.text = this.listItemForm.controls.text.value as string;
        this.listService.addNewListItem(this.listId!, item).subscribe(res => this.addedItem.emit(res));
        this.listItemForm.controls.text.setValue('');
    };

    private readonly getValues = (): ToDoItem => {
        const item = new ToDoItem();
        item.id = this.listItemForm.get('id')?.value as string | undefined
        item.text = this.listItemForm.get('text')?.value!
        item.position = this.listItemForm.get('position')?.value!
        item.isChecked = this.listItemForm.get('isChecked')?.value!
        return item;
    }

    public readonly deleteItem = () => {
        this.listService.deleteListItem(this.listId!, this.listItemForm.get('id')?.value!).subscribe(_ => {
            this.deletedItem.emit(this.listItemForm.get('id')?.value)
        });
    }
}
