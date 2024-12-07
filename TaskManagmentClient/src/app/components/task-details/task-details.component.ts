import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Task } from 'src/app/models/task';
import { TaskService } from 'src/app/services/task.service';
import { ActivatedRoute, Router } from '@angular/router';
import {RouterModule} from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'app-task-details',
  standalone: true,
  imports: [CommonModule,RouterModule,MatButtonModule,MatIconModule],
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.css']
})
export class TaskDetailsComponent implements OnInit{

  currentTask: Task = { } as Task ;

  constructor(
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router:Router
    ) {}

  ngOnInit(): void {
      this.getTask(this.route.snapshot.params['id']);
    
  }

  getTask(id: string): void {

    if (id) {
      this.taskService.getItem(id).subscribe({
        next: (data) => {
            this.currentTask= data as Task;
        },
        error: (e) => console.error(e)
    }); 
    }
    else {
      this.currentTask = {} as Task
    }

  }
  goToTaskList():void{
    this.router.navigate(['/tasks']);
  }

}
