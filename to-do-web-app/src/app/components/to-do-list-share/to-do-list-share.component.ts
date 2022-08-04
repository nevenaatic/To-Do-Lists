import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MsalBroadcastService, MsalInterceptorAuthRequest, MsalService } from '@azure/msal-angular';
import { ToDoListService } from 'src/app/core/to-do-list.service';
import { ToDoList } from 'src/app/models/to-do-list.model';

@Component({
  selector: 'app-to-do-list-share',
  templateUrl: './to-do-list-share.component.html',
  styleUrls: ['./to-do-list-share.component.css']
})
export class ToDoListShareComponent {
  list = new ToDoList()
  message: string | undefined
  generatedLink: string | undefined
  constructor(private readonly listService: ToDoListService, private readonly route: ActivatedRoute, private msalInstance: MsalService) {
    this.generatedLink = this.route.snapshot.paramMap.get('id')?.toString();
    this.getSharedList();
  }
  
  readonly getSharedList = () =>
    this.listService.getSharedToDoList(this.generatedLink!).subscribe(
      (res: ToDoList) => { this.list = res; },
      err => {
        this.message = "The link has expired"
      })
}
