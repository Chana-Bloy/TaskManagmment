import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from 'src/app/services/task.service';
import { Task } from 'src/app/models/task';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import Swal from 'sweetalert2'


@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCheckboxModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})

export class TaskListComponent implements OnInit {

  tasks: Task[] = [];
  filteredTasks: Task[] = [];
  keyword = '';
  displayedColumns: string[] = ['title', 'description', 'isCompleted', 'actions'];

  constructor(private taskService: TaskService, private router: Router) { }

  ngOnInit(): void {

    this.taskService.tasksList.subscribe((tasks: Task[]) => {
      this.tasks = tasks;
      this.filteredTasks = tasks;
    });
  }

  setActiveTask(id: number): void {
    this.router.navigate(['/tasks', id]);
  }
  addTask(): void {
    this.router.navigate(['/add']);
  }

  editTask(e: Event, id: number): void {
    e.stopPropagation()
    this.router.navigate(['/tasks', id, 'edit']);

  }
  deleteTask(e: Event, id: number): void {
    e.stopPropagation()
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        this.taskService.deleteItem(id).then((statusCode) => {
          if (statusCode == 200) {
            Swal.fire({
              title: "Deleted!",
              text: "Your task has been deleted.",
              icon: "success",
              showConfirmButton: false,
              timer: 2000
            });
          }
          this.keyword=''
        });

      }
    });
  }
  searchTitle(): void {
    this.filteredTasks = this.tasks?.filter(x => x.description.toLowerCase().includes(this.keyword.toLocaleLowerCase()) || x.title.toLowerCase().includes(this.keyword.toLocaleLowerCase()))
  }

}
