import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ClipboardService } from 'ngx-clipboard';
import { ToDoListService } from 'src/app/core/to-do-list.service';
import { SharedList } from 'src/app/models/shared-list';
import { ToDoList } from 'src/app/models/to-do-list.model';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-to-do-list-preview',
    templateUrl: './to-do-list-preview.component.html',
    styleUrls: ['./to-do-list-preview.component.css']
})
export class ToDoListPreviewComponent {
    @Input() list = new ToDoList();
    @Output() deleted = new EventEmitter();
    private readonly webUri = `${environment.webUrl}/shared-list/`;

    constructor(private readonly listService: ToDoListService, private readonly router: Router, private clipboardApi: ClipboardService, private snackBar: MatSnackBar) { }
    readonly delete = () =>
        this.listService.deleteToDoList(this.list.id!).subscribe(res => {
            this.deleted.emit(this.list.id);
        });

    readonly edit = () =>
        this.router.navigate(['/to-do-list', this.list.id])

    readonly share = () => {
        var listForShare = new SharedList();
        listForShare.listId = this.list.id!
        this.listService.shareList(listForShare).subscribe(
            res => {
                this.clipboardApi.copyFromContent(this.webUri + res);
                this.snackBar.open('Copied to clipboard');
            },
            _ => this.snackBar.open("Copy failed. Try again")
        )
    }
}
