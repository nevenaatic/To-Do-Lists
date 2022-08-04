import { ToDoItem } from "./to-do-item.model";

export class ToDoList {
    id: string | undefined;
    reminderDate: Date|undefined
    title: string = "";
    position : number = 0;
    isReminded : boolean = false;
    items: ToDoItem[] = [];

   

}