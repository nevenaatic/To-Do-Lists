import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ToDoList } from '../models/to-do-list.model';
import { Observable } from 'rxjs';
import { ToDoItem } from '../models/to-do-item.model';
import { SharedList } from '../models/shared-list';
import { PaginatorFilter } from '../models/paginator-filter.model';
import { ListsResponse } from '../models/lists-response.model';

@Injectable({
    providedIn: 'root'
})
export class ToDoListService {
    
    private readonly baseUri = `${environment.apiUrl}/to-do-lists`;
    constructor(private _http: HttpClient) { }

    getAllToDoLists(paginatorFilter : PaginatorFilter){
        return this._http.get<ListsResponse>(`${this.baseUri}/?pageNumber=${paginatorFilter.pageNumber}&pageSize=${paginatorFilter.pageSize}`);
    }
    deleteToDoList(id: string) {
        return this._http.delete<void>(this.baseUri + "/" + id);
    }
    getToDoList(id: string): Observable<ToDoList> {
        return this._http.get<ToDoList>(this.baseUri + "/" + id);
    }
    updateList(list: ToDoList) {
        return this._http.put<ToDoList>(this.baseUri + "/" + list.id, list);
    }
    updateListItem(listId: string, item: ToDoItem) {
        return this._http.put<ToDoItem>(this.baseUri + "/" + listId + "/to-do-items/" + item.id, item)
    }
    deleteListItem(listId: string, itemId: string) {
        return this._http.delete(this.baseUri + "/" + listId + "/to-do-items/" + itemId)
    }
    addNewListItem(id: string, item: ToDoItem): Observable<ToDoItem> {
        return this._http.post<ToDoItem>(this.baseUri + "/" + id + "/to-do-items", item)
    }
    createNewList(newList: ToDoList): Observable<ToDoList> {
        return this._http.post<ToDoList>(this.baseUri, newList)
    }
    changeItemPosition(listId: string | undefined, itemId: any[], position: number) {
        return this._http.put(this.baseUri + "/" + listId + "/to-do-items/" + itemId + "/" + position, null);
    }
    changeListPosition(listId: any, position: number) {
        return this._http.put(this.baseUri + "/" + listId + "/to-do-list/" + position, null)
    } 
    searchLists(searchText: string, filter: PaginatorFilter) {
        return this._http.post<ListsResponse>(`${this.baseUri}/search?searchText=${searchText}`, filter)
    }
    shareList(list: SharedList) {
        return this._http.post<string>(`${this.baseUri}/share`, list)
    }
     getSharedToDoList(generatedLink: string) {
      return this._http.get<ToDoList>(`${environment.apiUrl}/share/${generatedLink}`)
    }
   
}
