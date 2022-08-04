import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { ToDoList } from '../models/to-do-list.model';
import { ToDoListService } from './to-do-list.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private subject = new Subject<any>();
  public $subject = this.subject.asObservable();
  constructor(public readonly listService: ToDoListService) { }

  readonly createListWithItem = () => {
    const newList = new ToDoList();
    this.listService.createNewList(newList).subscribe(res =>
      this.subject.next(res)
    );
  }
}
