import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToDoListService } from 'src/app/core/to-do-list.service';
import { ToDoList } from 'src/app/models/to-do-list.model';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { PageEvent } from '@angular/material/paginator';
import { PaginatorFilter } from 'src/app/models/paginator-filter.model';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    lists: ToDoList[] = []
    listSize: number | undefined
    searchText: string = ""
    paginatorFilter = new PaginatorFilter()
 
    constructor(private readonly listService: ToDoListService, private readonly router: Router) { }
    ngOnInit(): void {
        this.getAllLists();
    }

    getAllLists() {
        return this.listService.getAllToDoLists(this.paginatorFilter).subscribe(res => {
            this.lists = res.toDoLists;
            this.listSize = res.listSize;
        });
    }

    readonly listDeleted = ($event: string) =>
        this.lists = this.lists.filter(x => x.id !== $event);

    readonly addNewList = () =>
        this.router.navigate(['/to-do-list'])

    readonly dropList = (event: CdkDragDrop<ToDoList[], string>) =>
        this.listService.changeListPosition(event.item.data, event.currentIndex + 1).subscribe();

    readonly searchLists = () =>
        this.listService.searchLists(this.searchText, this.paginatorFilter).subscribe(res => { this.lists = res.toDoLists; this.listSize = res.listSize;})

    readonly OnPageChange = (event: PageEvent) => {
        this.paginatorFilter.pageNumber = event.pageIndex + 1;
        this.paginatorFilter.pageSize = event.pageSize;
        if(this.searchText != ""){
            this.searchLists();
        }else{
            this.getAllLists(); 
        }
    }
}
