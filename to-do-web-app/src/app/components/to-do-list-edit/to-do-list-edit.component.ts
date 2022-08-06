import { Component, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToDoListService } from 'src/app/core/to-do-list.service';
import { ToDoList } from 'src/app/models/to-do-list.model';
import { FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { ToDoItem } from 'src/app/models/to-do-item.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { MessageService } from 'src/app/core/message.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
    selector: 'app-to-do-list-edit',
    templateUrl: './to-do-list-edit.component.html',
    styleUrls: ['./to-do-list-edit.component.css']
})
export class ToDoListEditComponent implements OnInit, OnDestroy {
    subscription: Subscription = new Subscription;
    listId: string | undefined;
    list = new ToDoList();
    minDate = new Date();
    form = new FormGroup({
        title: new FormControl('', [Validators.required]),
        reminderDate: new FormControl<Date | undefined>(undefined),
        position: new FormControl<number>(0),
        items: new FormArray<FormGroup<any>>([])
    })
    
    constructor(private readonly listService: ToDoListService, private readonly route: ActivatedRoute,
        private readonly router: Router, private readonly snackBar: MatSnackBar, private messageService: MessageService) {
        this.subscription = this.messageService.$subject.subscribe(list => {
            this.list = list;
        });
    }

    ngOnInit(): void {
        this.listId = this.route.snapshot.paramMap.get('id')?.toString();
        if (this.listId) {
            this.getToDoList();
        }
    }
    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }

    getToDoList() {
        this.listService.getToDoList(this.listId!).subscribe(res => {
            this.list = res;
            this.patchForm();
        })
    }
    dropItem(event: CdkDragDrop<ToDoItem[], string>) {
        this.listService.changeItemPosition(this.list.id, event.item.data, event.currentIndex + 1).subscribe(_ =>
            this.listService.getToDoList(this.list.id!)
                .subscribe(response => this.controls.items.controls = response.items.map(x => this.createFormGroup(x))));
    }
    get controls() {
        return this.form.controls;
    }

    get itemsControls(): FormGroup[] {
        return this.controls.items.controls as FormGroup[];
    }

    get checked() {
        return this.itemsControls.filter(x => x.controls['isChecked'].value);
    }

    get notChecked() {
        return this.itemsControls.filter(x => !x.controls['isChecked'].value);
    }

    private readonly patchForm = () => {
        this.controls.title.patchValue(this.list.title);
        this.controls.reminderDate.patchValue(this.list.reminderDate);
        this.controls.position.patchValue(this.list.position);
        this.list.items.forEach(item => this.controls.items.push(this.createFormGroup(item)));

    };

    private readonly createFormGroup = (item: ToDoItem): FormGroup =>
        new FormGroup({
            id: new FormControl(item.id),
            text: new FormControl(item.text),
            isChecked: new FormControl(item.isChecked),
            position: new FormControl(item.position)
        });

    readonly back = () =>
        this.router.navigate(['/']);

    readonly itemDeleted = ($event: string) =>
        this.controls.items.controls =
        this.itemsControls.filter(x => x.controls['id'].value !== $event);

    readonly itemAdded = (event: ToDoItem) =>
        this.itemsControls.push(this.createFormGroup(event))

    readonly updateList = () => {
        if (!this.list.id) {
            const newList = new ToDoList();
            newList.title = this.form.controls.title.value!
            this.listService.createNewList(newList).subscribe(res => {
                this.list = res;
                this.snackBar.open('List added');
            })
        } else {
            this.listService.updateList(this.getValues()).subscribe(_ =>
                this.snackBar.open('List updated'));
        }
    };

    private readonly getValues = (): ToDoList => {
        const list = new ToDoList();
        list.id = this.list.id;
        list.reminderDate = this.controls.reminderDate.value as Date | undefined;
        list.position = this.controls.position.value as number;
        list.title = this.controls.title.value as string;
        return list;
    };
}
